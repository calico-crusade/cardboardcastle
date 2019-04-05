namespace CardboardCastle.Models
{
    public interface IResident : IIntegrityModel
    {
        string Nickname { get; set; }
        long ResidentId { get; set; }
        long? UserId { get; set; }
    }
}
