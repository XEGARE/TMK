# Инструкция разработчика 

## Стек технологий 
 
- **Язык программирования** : C#
 
- **Платформа** : .NET 8.0
 
- **Библиотека для работы с базой данных** : [Npgsql](https://www.npgsql.org/)  (для взаимодействия с PostgreSQL)

## Архитектура приложения 

Приложение состоит из следующих компонентов:
### 1. Helpers.cs
Содержит вспомогательные методы для работы с базой данных и отображением информации:
 
- **Методы** : 
  - `ExecuteQuery(NpgsqlConnection, string) : void` — Выполняет SQL-запрос.
 
  - `OutputResultsInJsonFormat(NpgsqlCommand) : void` — Выводит результаты запроса в формате JSON.
 
  - `PauseMessage() : void` — Пауза с выводом сообщения.

### 2. OrderController.cs
Обрабатывает операции, связанные с заказами:
 
- **Поля** : 
  - `_connection : NpgsqlConnection` — Объект для работы с подключением к базе данных.
 
- **Методы** : 
  - `OrderController(NpgsqlConnection)` — Конструктор.
 
  - `ShowAll() : void` — Отображение всех заказов.
 
  - `Create() : void` — Создание нового заказа.
 
  - `Change() : void` — Изменение заказа.
 
  - `Delete() : void` — Удаление заказа.

  - `Search() : void` — Поиск заказа.

### 3. PositionController.cs 
Обрабатывает операции, связанные с позициями:
 
- **Поля** : 
  - `_connection : NpgsqlConnection` — Объект для работы с подключением к базе данных.
 
- **Методы** : 
  - `PositionController(NpgsqlConnection)` — Конструктор.
 
  - `ShowAll() : void` — Отображение всех позиций.
 
  - `Create() : void` — Создание новой позиции.
 
  - `Change() : void` — Изменение позиции.
 
  - `Delete() : void` — Удаление позиции.
 
  - `Search() : void` — Поиск позиции.
 
  - `InitializeDatabaseConnection() : bool` — Инициализация подключения к базе данных.

### 4. SteelController.cs 
Обрабатывает операции, связанные с управлением сталью:
 
- **Поля** : 
  - `_connection : NpgsqlConnection` — Объект для работы с подключением к базе данных.
 
- **Методы** : 
  - `SteelController(NpgsqlConnection)` — Конструктор.
 
  - `ShowAll() : void` — Отображение всех записей.
 
  - `Create() : void` — Создание новой записи.
 
  - `Change() : void` — Изменение записи.
 
  - `Delete() : void` — Удаление записи.
 
  - `Search() : void` — Поиск записи.

### 5. Program.cs 
Точка входа приложения:
 
- **Поля** : 
  - `connectionString : string` — Строка подключения к базе данных.
 
  - `_connection : NpgsqlConnection` — Экземпляр подключения к базе данных.
 
- **Методы** : 
  - `Main(string[]) : Task` — Главный метод приложения.
 
  - `InitializeDatabaseConnection() : bool` — Метод для инициализации подключения к базе данных.