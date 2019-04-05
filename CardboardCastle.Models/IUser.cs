namespace CardboardCastle.Models
{
    public interface IUser : IIntegrityModel
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        long UserId { get; set; }
    }
}
