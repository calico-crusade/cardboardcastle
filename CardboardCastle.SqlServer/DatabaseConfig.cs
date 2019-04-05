namespace CardboardCastle.SqlServer
{
    public interface IDatabaseConfig
    {
        string Connection { get; set; }
    }

    public class DatabaseConfig : IDatabaseConfig
    {
        public string Connection { get; set; }
    }
}
