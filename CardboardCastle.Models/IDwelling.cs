namespace CardboardCastle.Models
{
    public interface IDwelling : IIntegrityModel
    {
        string City { get; set; }
        long DwellingId { get; set; }
        string FriendlyName { get; set; }
        string Name { get; set; }
        string PostalCode { get; set; }
        string State { get; set; }
        string Street1 { get; set; }
        string Street2 { get; set; }
    }
}
