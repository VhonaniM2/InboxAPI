using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class UserIdentityModel
    {
        public string FullNames { get; set; }
        public string EmailAddress { get; set; }
        public Guid UserId { get; set; }
    }
}