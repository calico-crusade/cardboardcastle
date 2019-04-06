namespace CardboardCastle.SqlServer
{
    using Models.Types;

    public class SqlResponse
    {
        public int Id { get; set; }
        public QueryResponseCode Code { get; set; }
    }

    public class SqlResponse<T>
    {
        public T[] Data { get; set; }
        public QueryResponseCode Code { get; set; }
    }
}
