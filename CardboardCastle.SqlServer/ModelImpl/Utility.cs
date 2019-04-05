using System;

namespace CardboardCastle.SqlServer.ModelImpl
{
    using Models;
    using Models.Types;

    /// <summary>
    /// Represents something that needs to be paid within a dwelling
    /// </summary>
    public class Utility : IntegrityModel, IUtility
    {
        /// <summary>
        /// The ID of the utility
        /// </summary>
        public long UtilityId { get; set; }
        /// <summary>
        /// The ID of the dwelling this utility belongs to
        /// </summary>
        public long DwellingId { get; set; }
        /// <summary>
        /// The name of the utility
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The amount of the utility
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The type of arrangement the utility has for payments
        /// </summary>
        public UtilityPaymentType Type { get; set; }
        /// <summary>
        /// The recurrence pattern (if any) for this utility
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// The date the first payment is due
        /// </summary>
        public DateTime? PaymentDate { get; set; }
    }
}
