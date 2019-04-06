using System;

namespace CardboardCastle.Core.ApiModels
{
    using Models;

    public class DetailedUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }

        public static implicit operator DetailedUser(User user)
        {
            return new DetailedUser
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedOn = user.CreatedOn,
                Email = user.EmailAddress
            };
        }
    }
}
