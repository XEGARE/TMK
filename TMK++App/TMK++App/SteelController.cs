using Npgsql;

namespace TMK__App
{
    internal class SteelController
    {
        private readonly NpgsqlConnection _connection;

        public SteelController(NpgsqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        }

        public void ShowAll()
        {
            Console.Clear();
            Helpers.ExecuteQuery(_connection, "SELECT * FROM steel_grades");
        }

        public void Create()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Введите название стали: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Название не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "INSERT INTO steel_grades (name) VALUES (@name)";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("name", name);
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

                Console.Write("Введите старое название стали: ");
                string oldName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(oldName))
                {
                    Console.WriteLine("Старое название не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите новое название стали: ");
                string newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("Новое название не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "UPDATE steel_grades SET name = @newName WHERE name = @oldName";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("newName", newName);
                    command.Parameters.AddWithValue("oldName", oldName);
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

                Console.Write("Введите название стали: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Название не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "DELETE FROM steel_grades WHERE name = @name";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("name", name);
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

                Console.Write("Введите название стали: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Название не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "SELECT * FROM steel_grades WHERE name = @name";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("name", name);
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
