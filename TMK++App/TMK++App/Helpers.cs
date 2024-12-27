using Npgsql;
using System.Text.Json;

namespace TMK__App
{
    internal class Helpers
    {
        public static void ExecuteQuery(NpgsqlConnection connection, string query)
        {
            try
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    OutputResultsInJsonFormat(cmd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public static void OutputResultsInJsonFormat(NpgsqlCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var results = new List<Dictionary<string, object>>();

                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var columnValue = reader.GetValue(i);
                        row[columnName] = columnValue;
                    }

                    results.Add(row);
                }

                string json = JsonSerializer.Serialize(results, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                Console.WriteLine("Результат:");
                if (results.Count == 0)
                    Console.WriteLine("Ничего не найдено");
                else
                    Console.WriteLine(json);
            }
        }

        public static void PauseMessage()
        {
            Console.Write("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

    }
}
