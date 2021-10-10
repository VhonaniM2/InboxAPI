using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class InboxMessageModel
    {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string Message { get; set; }
        public string Label { get; set; }

    }
}