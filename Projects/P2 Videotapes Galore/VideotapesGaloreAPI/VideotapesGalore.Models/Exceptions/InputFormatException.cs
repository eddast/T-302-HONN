using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideotapesGalore.Models.Exceptions
{
    /// <summary>
    /// Exception thrown on 412 - precondition failed errors
    /// When user inputted model is not correctly formatted
    /// </summary>
    public class InputFormatException : Exception
    {
        /// <summary>
        /// Default message for exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public InputFormatException() : base("Model is not properly formatted.") { }

        /// <summary>
        /// Sets message for exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public InputFormatException(string message) : base(message) { }

        /// <summary>
        /// Sets message and exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public InputFormatException(string message, Exception inner) : base(message, inner) { } 

        /// <summary>
        /// Sets message for exception thrown when model is badly-formatted
        /// Adds list of informative errors
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public InputFormatException(string message, IEnumerable<string> errorList) : base(FormatErrorList(message, errorList)) { }

        /// <summary>
        /// Adds list of errors to error message
        /// </summary>
        /// <returns>String with error message and individual errors in context</returns>
        private static string FormatErrorList(string message, IEnumerable<string> errorList)
        {
            string allErrors = "";
            foreach (var error in errorList) allErrors += error + "\n";
            return message + "\n" + allErrors;
        }
    }
}