using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class InboxMessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Deleted { get; set; }
        public string Label { get; set; }
        public System.DateTime DateReceived { get; set; }
        public Nullable<System.DateTime> DateDeleted { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    }
}