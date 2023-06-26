using Microsoft.Data.Sqlite;

namespace Int5.DnD3D.WebApi.Util
{
    public class DatabaseManager
    {
        public string? DataSource { get; set; }
        public bool AddUser(string username)
        {
            using (var connection = new SqliteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    INSERT INTO User (username) VALUES ($username);
";
                command.Parameters.AddWithValue("$username", username);
                Console.WriteLine("POST: " + username+" added");
                GetAllUsernames();
                return command.ExecuteNonQuery() == 1;
            }
           
        }
        public List<string> GetAllUsernames()
        {
            List<string> usernames = new();
            using (var connection = new SqliteConnection(DataSource))
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
                        Console.WriteLine(result);
                        usernames.Add(result);
                    }
                }
            }
            return usernames;

        }

        public bool UserExists(string username)
        {

            using (var connection = new SqliteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT * FROM User WHERE username =  $username;
";
                command.Parameters.AddWithValue("$username", username);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader.GetString(1);
                        return result == username;
                    }
                }
            }
            return false;
        }

        public DatabaseManager(string? dataSource)
        {
            DataSource = dataSource;
        }
    }
}
