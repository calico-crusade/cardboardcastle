namespace CardboardCastle.SqlServer
{
    public interface IDatabaseConfig
    {
        string Connection { get; set; }
        int Timeout { get; set; }
        string Catalog { get; set; }
    }

    public class DatabaseConfig : IDatabaseConfig
    {
        public string Connection { get; set; }
        public int Timeout { get; set; }
        public string Catalog { get; set; }
    }
}
