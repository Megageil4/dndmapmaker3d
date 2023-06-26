// See https://aka.ms/new-console-template for more information
using Int5.DnD.EFCUVIDND.MySql;
using Int5.DnD3D.EFCFUVIDND.DbSetup;
using Microsoft.Data.Sqlite;
if (!File.Exists(@"user.db"))
{
    using (var connection = new SqliteConnection(@"Data Source=user.db"))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE User (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL
        );
        INSERT INTO User
        VALUES (0, 'default');
        ";
        command.ExecuteNonQuery();
        Console.WriteLine("Datenbank neu erstellt");


        command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT *
        FROM User
    ";
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var result = reader.GetString(1);
                Console.WriteLine($"{result}");
            }
        }
    }
}
else
{
    Console.WriteLine("Datenbank bereits vorhanden");
    using (var connection = new SqliteConnection(@"Data Source=user.db"))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT *
        FROM User
    ";
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var result = reader.GetString(1);
                Console.WriteLine($"{result}");
            }
        }
    }
}