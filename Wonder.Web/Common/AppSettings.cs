using Wonder.Web.Database;

namespace Wonder.Web.Common
{
    public class AppSettings
    {
        public string Hostname { get; private set; }

        public int Port { get; private set; }

        public bool IsHttps { get; private set; }

        public DbSettings DatabaseSettings { get; private set; }

        public AppSettings(bool https = false)
        {
            DatabaseSettings = DbSettings.Default;

            IsHttps = https;

            Port = 8080;

            if (https)
            {
                Hostname = $"https://localhost:{Port}";
            }
            else Hostname = $"http://localhost:{Port}";
        }
    }
}
