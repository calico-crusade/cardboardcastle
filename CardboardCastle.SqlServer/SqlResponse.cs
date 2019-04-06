namespace CardboardCastle.SqlServer
{
    using Models.Types;

    public class SqlResponse
    {
        public int Id { get; set; }
        public QueryResponseCode Code { get; set; }

        public SqlResponse() { }

        public SqlResponse(int id, QueryResponseCode code)
        {
            Id = id;
            Code = code;
        }

        public SqlResponse(int id) : this(id, QueryResponseCode.Ok) { }

        public SqlResponse(QueryResponseCode code) : this(0, code) { }

        public static SqlResponse NotFound => new SqlResponse(QueryResponseCode.NotFound);
        public static SqlResponse Error => new SqlResponse(QueryResponseCode.Error);
        public static SqlResponse Unauthorized => new SqlResponse(QueryResponseCode.Unauthroized);
    }

    public class SqlResponse<T>
    {
        public T[] Data { get; set; }
        public QueryResponseCode Code { get; set; }
    }
}
