﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class UserDetails
    {
        public string FullNames { get; set; }
        public string  Role { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}