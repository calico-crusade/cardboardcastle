namespace CardboardCastle.Models
{
    public interface IUser : IIntegrityModel
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string ResetToken { get; set; }
        string Salt { get; set; }
        long UserId { get; set; }
    }

    /// <summary>
    /// Represents a user's account
    /// </summary>
    public class User : IntegrityModel, IUser
    {
        /// <summary>
        /// The ID of the user
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// User's password hash
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User's Password salt
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// User's Reset Token
        /// </summary>
        public string ResetToken { get; set; }
    }
}
