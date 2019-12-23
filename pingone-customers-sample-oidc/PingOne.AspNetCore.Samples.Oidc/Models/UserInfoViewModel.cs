using System;

namespace PingOne.AspNetCore.Samples.Oidc.Models
{
    public class UserInfoViewModel
    {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string PreferredUsername { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public Guid? Sub { get; set; }
    }
}
