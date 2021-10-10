using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InboxAPI.Models
{
    public class DataOperationOutcome
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static DataOperationOutcome Success
        {
            get
            {
                var outcome = new DataOperationOutcome { IsSuccessful = true };
                return outcome;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [was successful].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was successful]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Creates the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The error outcome.</returns>
        public static DataOperationOutcome CreateError(string message)
        {
            return new DataOperationOutcome
            {
                IsSuccessful = false,
                Message = message
            };
        }
    }
}