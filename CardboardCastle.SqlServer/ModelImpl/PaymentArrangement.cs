﻿namespace CardboardCastle.SqlServer.ModelImpl
{
    using Models;
    using Models.Types;

    /// <summary>
    /// Represents an arrangement for how to split a utility
    /// </summary>
    public class PaymentArrangement : IntegrityModel, IPaymentArrangement
    {
        /// <summary>
        /// The ID to represent the payment arrangement
        /// </summary>
        public long PaymentArrangementId { get; set; }
        /// <summary>
        /// The ID of the utility this payment arrangement belongs to
        /// </summary>
        public long UtilityId { get; set; }
        /// <summary>
        /// The ID of the person who will be paying
        /// </summary>
        public long ResidencyId { get; set; }
        /// <summary>
        /// The amount the payment is for
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// How the payment and amount is used
        /// </summary>
        public PaymentType Type { get; set; }
    }
}
