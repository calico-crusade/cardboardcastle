namespace CardboardCastle.Models
{
    using Types;

    public interface IPaymentArrangement : IIntegrityModel
    {
        decimal Amount { get; set; }
        long PaymentArrangementId { get; set; }
        long ResidencyId { get; set; }
        PaymentType Type { get; set; }
        long UtilityId { get; set; }
    }
}
