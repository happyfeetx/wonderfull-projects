namespace Wonder.Web.Database
{
    public class DbSettings
    {
        public string Hostname { get; private set; }
        public string DatabaseName { get; private set; }
        public string LoginName { get; private set; }
        public string Password { get; private set; }
        public int Port { get; private set; }

        public static DbSettings Default => new DbSettings()
        {
            Hostname = "localhost",
            DatabaseName = "WonderDb",
            LoginName = "Database",
            Password = "",
            Port = 5442
        };
    }

    public enum DatabaseProvider
    {
        SQLServer = 0,
        SQLite = 1
    }
}
