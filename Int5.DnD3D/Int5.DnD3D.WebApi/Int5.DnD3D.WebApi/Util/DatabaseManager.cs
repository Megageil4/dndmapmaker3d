using Microsoft.Data.Sqlite;

namespace Int5.DnD3D.WebApi.Util
{
    /// <summary>
    /// Manages the Sqlite Database for a given DataSource
    /// </summary>
    public class DatabaseManager
    {
        public string? DataSource { get; set; }
        /// <summary>
        /// Inserts a new User to the Database
        /// </summary>
        /// <param name="username">Username of the newly added User</param>
        /// <returns>True if user was succesfully added, false if not</returns>
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
        /// <summary>
        /// Selects all Users in the sqlite database
        /// </summary>
        /// <returns>A list of all Users</returns>
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
        /// <summary>
        /// Tests if a User exists in the sqlite databse
        /// </summary>
        /// <param name="username">Username to be searched</param>
        /// <returns>True if user exists, false if not</returns>
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
        /// <summary>
        /// Creates a new instance of the DatabaseManager
        /// </summary>
        /// <param name="dataSource">path to the .db file</param>
        public DatabaseManager(string? dataSource)
        {
            DataSource = dataSource;
        }
    }
}
