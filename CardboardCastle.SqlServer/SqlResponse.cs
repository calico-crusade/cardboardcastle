namespace CardboardCastle.SqlServer
{
    using Models.Types;

    public class SqlResponse
    {
        public int Id { get; set; }
        public QueryResponseCode Code { get; set; }
    }
}
