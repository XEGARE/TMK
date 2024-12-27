using Npgsql;

namespace TMK__App
{
    internal class OrderController
    {
        private readonly NpgsqlConnection _connection;

        public OrderController(NpgsqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        }

        public void ShowAll()
        {
            Console.Clear();
            Helpers.ExecuteQuery(_connection, "SELECT * FROM orders");
        }

        public void Create()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Введите номер заказа: ");
                string orderNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderNumber))
                {
                    Console.WriteLine("Номер заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Укажите цех-производитель: ");
                string manufacturer = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(manufacturer))
                {
                    Console.WriteLine("Цех-производитель не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите дату начала(в формате ДД.ММ.ГГГГ): ");
                string startDate = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(startDate))
                {
                    Console.WriteLine("Дата начала не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите дату окончания(в формате ДД.ММ.ГГГГ): ");
                string endDate = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(endDate))
                {
                    Console.WriteLine("Дата окончания не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string status = "новый";

                while (true)
                {
                    Console.WriteLine("Выберите статус заказа:");
                    Console.WriteLine("1. новый");
                    Console.WriteLine("2. в работе");
                    Console.WriteLine("3. выполнен");
                    Console.Write("Выберите пункт меню: ");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        status = "новый";
                        break;
                    }
                    else if (choice == "2")
                    {
                        status = "в работе";
                        break;
                    }
                    else if (choice == "3")
                    {
                        status = "выполнен";
                        break;
                    }
                }

                string query = "INSERT INTO orders (order_number, manufacturer, start_date, end_date, status) VALUES (@orderNumber, @manufacturer, TO_DATE(@startDate, 'DD.MM.YYYY'), TO_DATE(@endDate, 'DD.MM.YYYY'), @status)";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("orderNumber", orderNumber);
                    command.Parameters.AddWithValue("manufacturer", manufacturer);
                    command.Parameters.AddWithValue("startDate", startDate);
                    command.Parameters.AddWithValue("endDate", endDate);
                    command.Parameters.AddWithValue("status", status);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Запись успешно добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            Helpers.PauseMessage();
        }

        public void Change()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Введите номер заказа: ");
                string orderNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderNumber))
                {
                    Console.WriteLine("Номер заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                List<string> updates = new List<string>();

                Console.Write("Укажите цех-производитель: ");
                string manufacturer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(manufacturer))
                    updates.Add("manufacturer = @manufacturer");

                Console.Write("Введите дату начала(в формате ДД.ММ.ГГГГ): ");
                string startDate = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(startDate))
                    updates.Add("start_date = TO_DATE(@startDate, 'DD.MM.YYYY')");

                Console.Write("Введите дату окончания(в формате ДД.ММ.ГГГГ): ");
                string endDate = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(endDate))
                    updates.Add("end_date = TO_DATE(@endDate, 'DD.MM.YYYY')");

                string status="";

                Console.WriteLine("Выберите статус заказа:");
                Console.WriteLine("1. новый");
                Console.WriteLine("2. в работе");
                Console.WriteLine("3. выполнен");
                Console.Write("Выберите пункт меню: ");
                string choice = Console.ReadLine();
                if (choice == "1")
                    status = "новый";
                else if (choice == "2")
                    status = "в работе";
                else if (choice == "3")
                    status = "выполнен";

                if (!string.IsNullOrWhiteSpace(status))
                    updates.Add("status = @status");

                if (updates.Count == 0)
                {
                    Console.WriteLine("Вы не ввели ни одного параметра!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "UPDATE orders SET " + string.Join(", ", updates) + " WHERE order_number = @orderNumber";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    if (!string.IsNullOrWhiteSpace(manufacturer)) command.Parameters.AddWithValue("manufacturer", manufacturer);
                    if (!string.IsNullOrWhiteSpace(startDate)) command.Parameters.AddWithValue("startDate", startDate);
                    if (!string.IsNullOrWhiteSpace(endDate)) command.Parameters.AddWithValue("endDate", endDate);
                    if (!string.IsNullOrWhiteSpace(status)) command.Parameters.AddWithValue("status", status);
                    command.Parameters.AddWithValue("orderNumber", orderNumber);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Запись успешно обновлена!");
                    else
                        Console.WriteLine("Ошибка при выполненинии запроса: запись не найдена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            Helpers.PauseMessage();
        }

        public void Delete()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Введите номер заказа: ");
                string orderNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderNumber))
                {
                    Console.WriteLine("Номер заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "DELETE FROM orders WHERE order_number = @orderNumber";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("orderNumber", orderNumber);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Запись успешно удалена!");
                    else
                        Console.WriteLine("Ошибка при выполненинии запроса: запись не найдена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            Helpers.PauseMessage();
        }

        public void Search()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Укажите цех-производитель: ");
                string manufacturer = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(manufacturer))
                {
                    Console.WriteLine("Цех-производитель не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "SELECT * FROM orders WHERE manufacturer = @manufacturer";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("manufacturer", manufacturer);
                    Helpers.OutputResultsInJsonFormat(command);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            Helpers.PauseMessage();
        }
    }
}
