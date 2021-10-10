using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class OperationOutcome
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static OperationOutcome Success
        {
            get
            {
                return new OperationOutcome
                {
                    IsSuccessful = true
                };
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public string Error { get; set; }

    }
}