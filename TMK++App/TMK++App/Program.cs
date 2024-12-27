using Npgsql;

using TMK__App;

class Program
{
    private static string connectionString = "Host=localhost;Username=myuser;Password=mypassword;Database=mydatabase";
    private static NpgsqlConnection connection;

    public static async Task Main(string[] args)
    {
        if (InitializeDatabaseConnection())
        {
            connection.Close();

            SteelController steelController = new SteelController(connection);
            OrderController orderController = new OrderController(connection);
            PositionController positionController = new PositionController(connection);

            bool LaunchCycle = true;

            int menu = 0;

            while (LaunchCycle)
            {
                Console.Clear();

                if (menu == 0)
                {
                    Console.WriteLine("Меню:");
                    Console.WriteLine("1. Стали");
                    Console.WriteLine("2. Заказы");
                    Console.WriteLine("3. Позиции");
                    Console.WriteLine("0. Выход");
                }
                else
                {
                    if (menu == 1)
                        Console.WriteLine("Стали:");
                    else if (menu == 2)
                        Console.WriteLine("Заказы:");
                    else if (menu == 3)
                        Console.WriteLine("Позиции:");

                    Console.WriteLine("1. Посмотреть");
                    Console.WriteLine("2. Создать");
                    Console.WriteLine("3. Изменить");
                    Console.WriteLine("4. Удалить");
                    Console.WriteLine("5. Поиск");
                    Console.WriteLine("0. Назад");
                }

                Console.Write("Выберите пункт меню: ");
                var choice = Console.ReadLine();

                if (menu == 0)
                {
                    if (choice == "1")
                        menu = 1;
                    else if (choice == "2")
                        menu = 2;
                    else if (choice == "3")
                        menu = 3;
                    else if (choice == "0")
                        LaunchCycle = false;
                }
                else
                {
                    if (choice == "1")
                    {
                        if (menu == 1)
                            steelController.ShowAll();
                        else if (menu == 2)
                            orderController.ShowAll();
                        else if (menu == 3)
                            positionController.ShowAll();
                        Helpers.PauseMessage();
                    }
                    else if (choice == "2")
                    {
                        if (menu == 1)
                            steelController.Create();
                        else if (menu == 2)
                            orderController.Create();
                        else if (menu == 3)
                            positionController.Create();
                    }
                    else if (choice == "3")
                    {
                        if (menu == 1)
                            steelController.Change();
                        else if (menu == 2)
                            orderController.Change();
                        else if (menu == 3)
                            positionController.Change();
                    }
                    else if (choice == "4")
                    {
                        if (menu == 1)
                            steelController.Delete();
                        else if (menu == 2)
                            orderController.Delete();
                        else if (menu == 3)
                            positionController.Delete();
                    }
                    else if (choice == "5")
                    {
                        if (menu == 1)
                            steelController.Search();
                        else if (menu == 2)
                            orderController.Search();
                        else if (menu == 3)
                            positionController.Search();
                    }
                    else if (choice == "0")
                        menu = 0;
                }
            }
        }
        else
        {
            Console.WriteLine("Не удалось установить подключение к базе данных. Программа завершена.");
        }
    }

    static bool InitializeDatabaseConnection()
    {
        try
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Подключение не установлено. Статус: " + connection.State);
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при подключении к базе данных: {ex.Message}");
            return false;
        }
    }
}
