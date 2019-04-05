namespace CardboardCastle.Core.ApiModels
{
    using Models;

    public class RegisterUser
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public static implicit operator User(RegisterUser user)
        {
            return new User
            {
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            };
        }
    }
}
