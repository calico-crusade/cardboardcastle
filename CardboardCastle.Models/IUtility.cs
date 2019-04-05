using System;

namespace CardboardCastle.Models
{
    using Types;

    public interface IUtility : IIntegrityModel
    {
        decimal Amount { get; set; }
        DateTime? PaymentDate { get; set; }
        long DwellingId { get; set; }
        string Name { get; set; }
        string Pattern { get; set; }
        UtilityPaymentType Type { get; set; }
        long UtilityId { get; set; }
    }
}
