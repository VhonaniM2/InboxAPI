using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class Error
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}