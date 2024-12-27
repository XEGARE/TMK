using Npgsql;

namespace TMK__App
{
    internal class PositionController
    {
        private readonly NpgsqlConnection _connection;

        public PositionController(NpgsqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        }

        public void ShowAll()
        {
            Console.Clear();
            Helpers.ExecuteQuery(_connection, "SELECT * FROM positions");
        }

        public void Create()
        {
            try
            {
                _connection.Open();

                Console.Clear();

                Console.Write("Введите ID заказа: ");
                string orderID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderID))
                {
                    Console.WriteLine("ID заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(orderID, out int numericOrderID))
                {
                    Console.WriteLine("ID заказа должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите номер позиции: ");
                string positionNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(positionNumber))
                {
                    Console.WriteLine("Номер позиции не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(positionNumber, out int numericPositionNumber))
                {
                    Console.WriteLine("Номер позиции должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите ID стали: ");
                string steelID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(steelID))
                {
                    Console.WriteLine("ID стали не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(steelID, out int numericSteelID))
                {
                    Console.WriteLine("ID стали должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите диаметр: ");
                string diameter = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(diameter))
                {
                    Console.WriteLine("Диаметр не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!float.TryParse(diameter, out float numericDiameter))
                {
                    Console.WriteLine("Диаметр должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите толщину стенки: ");
                string wallThickness = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(wallThickness))
                {
                    Console.WriteLine("Диаметр не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!float.TryParse(wallThickness, out float numericWallThickness))
                {
                    Console.WriteLine("Диаметр должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите объем позиции заказа: ");
                string volume = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(volume))
                {
                    Console.WriteLine("Объем позиции заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!float.TryParse(volume, out float numericVolume))
                {
                    Console.WriteLine("Объем позиции заказа должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите единица измерения: ");
                string unit = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(unit))
                {
                    Console.WriteLine("Единица измерения не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }

                string status = "новая";

                while (true)
                {
                    Console.WriteLine("Выберите статус заказа:");
                    Console.WriteLine("1. новая");
                    Console.WriteLine("2. в работе");
                    Console.WriteLine("3. выполнена");
                    Console.Write("Выберите пункт меню: ");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        status = "новая";
                        break;
                    }
                    else if (choice == "2")
                    {
                        status = "в работе";
                        break;
                    }
                    else if (choice == "3")
                    {
                        status = "выполнена";
                        break;
                    }
                }

                string query = "INSERT INTO positions (order_id, position_number, steel_grade_id, diameter, wall_thickness, volume, unit, status) VALUES (@orderID, @positionNumber, @steelID, @diameter, @wallThickness, @volume, @unit, @status)";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("orderID", numericOrderID);
                    command.Parameters.AddWithValue("positionNumber", numericPositionNumber);
                    command.Parameters.AddWithValue("steelID", numericSteelID);
                    command.Parameters.AddWithValue("diameter", numericDiameter);
                    command.Parameters.AddWithValue("wallThickness", numericWallThickness);
                    command.Parameters.AddWithValue("volume", numericVolume);
                    command.Parameters.AddWithValue("unit", unit);
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

                Console.Write("Введите ID заказа: ");
                string orderID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderID))
                {
                    Console.WriteLine("ID заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(orderID, out int numericOrderID))
                {
                    Console.WriteLine("ID заказа должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите номер позиции: ");
                string positionNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(positionNumber))
                {
                    Console.WriteLine("Номер позиции не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(positionNumber, out int numericPositionNumber))
                {
                    Console.WriteLine("Номер позиции должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                List<string> updates = new List<string>();

                Console.Write("Введите ID стали: ");
                string steelID = Console.ReadLine();
                int numericSteelID = 0;
                if (!string.IsNullOrWhiteSpace(steelID))
                {
                    if (!int.TryParse(steelID, out numericSteelID))
                    {
                        Console.WriteLine("ID стали должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    updates.Add("steel_grade_id = @steelID");
                }

                Console.Write("Введите диаметр: ");
                string diameter = Console.ReadLine();
                float numericDiameter = 0.0f;
                if (!string.IsNullOrWhiteSpace(diameter))
                {
                    if (!float.TryParse(diameter, out numericDiameter))
                    {
                        Console.WriteLine("Диаметр должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    updates.Add("diameter = @diameter");
                }

                Console.Write("Введите толщину стенки: ");
                string wallThickness = Console.ReadLine();
                float numericWallThickness = 0.0f;
                if (!string.IsNullOrWhiteSpace(wallThickness))
                {
                    if (!float.TryParse(wallThickness, out numericWallThickness))
                    {
                        Console.WriteLine("Диаметр должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    updates.Add("wall_thickness = @wallThickness");
                }

                Console.Write("Введите объем позиции заказа: ");
                string volume = Console.ReadLine();
                float numericVolume = 0.0f;
                if (!string.IsNullOrWhiteSpace(volume))
                {
                    if (!float.TryParse(volume, out numericVolume))
                    {
                        Console.WriteLine("Объем позиции заказа должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    updates.Add("volume = @volume");
                }

                Console.Write("Введите единица измерения: ");
                string unit = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(unit))
                    updates.Add("unit =@unit");

                string status = "";

                Console.WriteLine("Выберите статус заказа:");
                Console.WriteLine("1. новая");
                Console.WriteLine("2. в работе");
                Console.WriteLine("3. выполнена");
                Console.Write("Выберите пункт меню: ");
                string choice = Console.ReadLine();
                if (choice == "1")
                    status = "новая";
                else if (choice == "2")
                    status = "в работе";
                else if (choice == "3")
                    status = "выполнена";

                if (!string.IsNullOrWhiteSpace(status))
                    updates.Add("status = @status");

                if (updates.Count == 0)
                {
                    Console.WriteLine("Вы не ввели ни одного параметра!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "UPDATE positions SET " + string.Join(", ", updates) + " WHERE order_id = @orderID AND position_number = @positionNumber";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    if (!string.IsNullOrWhiteSpace(steelID)) command.Parameters.AddWithValue("steelID", numericSteelID);
                    if (!string.IsNullOrWhiteSpace(diameter)) command.Parameters.AddWithValue("diameter", numericDiameter);
                    if (!string.IsNullOrWhiteSpace(wallThickness)) command.Parameters.AddWithValue("wallThickness", numericWallThickness);
                    if (!string.IsNullOrWhiteSpace(volume)) command.Parameters.AddWithValue("volume", numericVolume);
                    if (!string.IsNullOrWhiteSpace(unit)) command.Parameters.AddWithValue("unit", unit);
                    if (!string.IsNullOrWhiteSpace(status)) command.Parameters.AddWithValue("status", status);
                    command.Parameters.AddWithValue("orderID", numericOrderID);
                    command.Parameters.AddWithValue("positionNumber", numericPositionNumber);
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

                Console.Write("Введите ID заказа: ");
                string orderID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(orderID))
                {
                    Console.WriteLine("ID заказа не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(orderID, out int numericOrderID))
                {
                    Console.WriteLine("ID заказа должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                Console.Write("Введите номер позиции: ");
                string positionNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(positionNumber))
                {
                    Console.WriteLine("Номер позиции не может быть пустым!");
                    Helpers.PauseMessage();
                    return;
                }
                if (!int.TryParse(positionNumber, out int numericPositionNumber))
                {
                    Console.WriteLine("Номер позиции должно быть числовым значением!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "DELETE FROM positions WHERE order_id = @orderID AND position_number = @positionNumber";

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("orderID", numericOrderID);
                    command.Parameters.AddWithValue("positionNumber", numericPositionNumber);
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


                List<string> conditions = new List<string>();

                Console.Write("Укажите цех-производитель: ");
                string manufacturer = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(manufacturer))
                    conditions.Add("o.manufacturer = @manufacturer");

                Console.Write("Введите ID стали: ");
                string steelID = Console.ReadLine();
                int numericSteelID = 0;
                if (!string.IsNullOrWhiteSpace(steelID))
                {
                    if (!int.TryParse(steelID, out numericSteelID))
                    {
                        Console.WriteLine("ID стали должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    conditions.Add("p.steel_grade_id = @steelID");
                }

                Console.Write("Введите диаметр: ");
                string diameter = Console.ReadLine();
                float numericDiameter = 0.0f;
                if (!string.IsNullOrWhiteSpace(diameter))
                {
                    if (!float.TryParse(diameter, out numericDiameter))
                    {
                        Console.WriteLine("Диаметр должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    conditions.Add("p.diameter = @diameter");
                }

                Console.Write("Введите толщину стенки: ");
                string wallThickness = Console.ReadLine();
                float numericWallThickness = 0.0f;
                if (!string.IsNullOrWhiteSpace(wallThickness))
                {
                    if (!float.TryParse(wallThickness, out numericWallThickness))
                    {
                        Console.WriteLine("Диаметр должно быть числовым значением!");
                        Helpers.PauseMessage();
                        return;
                    }
                    conditions.Add("p.wall_thickness = @wallThickness");
                }

                if (conditions.Count == 0)
                {
                    Console.WriteLine("Вы не ввели ни одного параметра!");
                    Helpers.PauseMessage();
                    return;
                }

                string query = "SELECT o.order_number, o.manufacturer, o.start_date, o.end_date, o.status as order_status, p.* FROM positions p JOIN orders o ON p.order_id = o.id WHERE " + string.Join(" AND ", conditions);

                using (var command = new NpgsqlCommand(query, _connection))
                {
                    if (!string.IsNullOrWhiteSpace(manufacturer)) command.Parameters.AddWithValue("manufacturer", manufacturer);
                    if (!string.IsNullOrWhiteSpace(steelID)) command.Parameters.AddWithValue("steelID", numericSteelID);
                    if (!string.IsNullOrWhiteSpace(diameter)) command.Parameters.AddWithValue("diameter", numericDiameter);
                    if (!string.IsNullOrWhiteSpace(wallThickness)) command.Parameters.AddWithValue("wallThickness", numericWallThickness);
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
