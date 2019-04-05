namespace CardboardCastle.Core
{
    public interface ICoreConfig
    {
        string KeyPath { get; }
    }

    public class CoreConfig : ICoreConfig
    {
        public string KeyPath { get; set; }
    }
}
