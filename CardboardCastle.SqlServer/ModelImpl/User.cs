namespace CardboardCastle.SqlServer.ModelImpl
{
    using Models;

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
    }
}
