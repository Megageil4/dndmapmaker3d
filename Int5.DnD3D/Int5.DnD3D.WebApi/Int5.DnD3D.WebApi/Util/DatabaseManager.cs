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
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    INSERT INTO User VALUES (0,$username);
";
                command.Parameters.AddWithValue("$username", username);
                return command.ExecuteNonQuery() == 1;
            }
           
        }
        public List<string> GetAllUsernames()
        {
            List<string> usernames = new();
            using (var connection = new SqliteConnection(DataSource))
            {
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
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT * FROM User WHERE username =  $username;
";
                command.Parameters.AddWithValue("$username", username);
                return command.ExecuteNonQuery() == 1;
            }
            return false;
        }

        public DatabaseManager(string? dataSource)
        {
            DataSource = dataSource;
        }
    }
}
