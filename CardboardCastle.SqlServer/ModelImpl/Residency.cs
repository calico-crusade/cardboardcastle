namespace CardboardCastle.SqlServer.ModelImpl
{
    using Models;

    /// <summary>
    /// Represents the relationship between a user and a dwelling
    /// </summary>
    public class Residency : IntegrityModel, IResidency
    {
        /// <summary>
        /// The Id to represent this object
        /// </summary>
        public long ResidencyId { get; set; }
        /// <summary>
        /// The ID of the resident object for this user
        /// </summary>
        public long ResidentId { get; set; }
        /// <summary>
        /// The ID of the dwelling for this object
        /// </summary>
        public long DwellingId { get; set; }
    }
}
