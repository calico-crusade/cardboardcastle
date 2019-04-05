namespace CardboardCastle.SqlServer.ModelImpl
{
    using Models;

    /// <summary>
    /// Represents an apartment or house where utilities are shared
    /// </summary>
    public class Dwelling : IntegrityModel, IDwelling
    {
        /// <summary>
        /// The ID of the dwelling
        /// </summary>
        public long DwellingId { get; set; }
        /// <summary>
        /// The friendly name of the dwelling
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Address Street line 1
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// Address Street line 2
        /// </summary>
        public string Street2 { get; set; }
        /// <summary>
        /// Address City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Address State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Address postal code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// A friendly name to be used by the application
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
