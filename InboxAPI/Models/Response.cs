using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace InboxAPI.Models
{
    public class Response<T> : OperationOutcome
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}