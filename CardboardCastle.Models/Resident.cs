namespace CardboardCastle.Models
{
    public interface IResident : IIntegrityModel
    {
        string Nickname { get; set; }
        long ResidentId { get; set; }
        long? UserId { get; set; }
    }

    /// <summary>
    /// Represents a user who lives in 1 or more dwellings
    /// </summary>
    public class Resident : IntegrityModel, IResident
    {
        /// <summary>
        /// The ID of the resident
        /// </summary>
        public long ResidentId { get; set; }
        /// <summary>
        /// The option user's ID
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// The nickname used to identify this user
        /// </summary>
        public string Nickname { get; set; }
    }
}
