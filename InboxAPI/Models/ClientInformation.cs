using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class ClientInformation
    {
        public string ClientID { get; set; }
        public string SecretKey { get; set; }
        public string ClientName { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}