# Инструкция для администратора

## Шаг 1: Запуск базы данных 
 
1. Перейдите в директорию, где находится файл `docker-compose.yml`:


```bash
cd database
```

2. Запустите базу данных с помощью команды:


```bash
docker-compose up -d
```

3. Проверьте, что контейнер успешно запущен:


```bash
docker ps
```
Убедитесь, что контейнер с именем `postgres_db` находится в списке работающих контейнеров.

---


## Шаг 2: Импорт данных в базу 
 
1. Убедитесь, что файл `database.sql` находится в доступной директории.
 
2. Выполните команду для импорта данных в базу данных:


```bash
docker exec -i postgres_db psql -U myuser -d mydatabase < database.sql
```

3. Проверьте импорт данных:


```bash
docker exec -it postgres_db psql -U myuser -d mydatabase -c "\dt"
```

Если команда отображает список таблиц, значит импорт прошёл успешно.


---


## Шаг 3: Настройка строки подключения в приложении 
 
1. Откройте файл `Program.cs` в вашем редакторе кода.
 
2. Найдите строку подключения:


```csharp
private static string connectionString = "Host=localhost;Username=myuser;Password=mypassword;Database=mydatabase";
```
 
3. При необходимости замените значения: 
  - `Host` — укажите хост базы данных (например, `localhost` или IP-адрес сервера).
 
  - `Username`, `Password`, `Database` — используйте соответствующие значения из `docker-compose.yml`.


---


## Шаг 4: Сборка приложения 

1. Соберите приложение с помощью команды:


```bash
dotnet build
```

2. Проверьте запуск приложения локально:


```bash
dotnet run
```


---


## Шаг 5: Распространение приложения 
 
1. Подготовьте исполняемые файлы или архив приложения для передачи пользователям.
 
2. Распространите приложение, выбрав удобный способ передачи (например, файловый сервер или облачный сервис).
 
3. Предоставьте пользователям инструкции по установке и запуску.