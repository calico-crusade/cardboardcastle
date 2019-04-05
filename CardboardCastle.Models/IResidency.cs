namespace CardboardCastle.Models
{
    public interface IResidency : IIntegrityModel
    {
        long DwellingId { get; set; }
        long ResidencyId { get; set; }
        long ResidentId { get; set; }
    }
}
