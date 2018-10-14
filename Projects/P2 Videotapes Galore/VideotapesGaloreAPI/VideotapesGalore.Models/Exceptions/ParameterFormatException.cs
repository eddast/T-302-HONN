using System;

namespace VideotapesGalore.Models.Exceptions
{
    /// <summary>
    /// Exception thrown on 412 - precondition failed errors
    /// When user inputted model is not correctly formatted
    /// </summary>
    public class ParameterFormatException : Exception
    {
        /// <summary>
        /// Default message for exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public ParameterFormatException() : base("Paramter improperly formatted.") { }

        /// <summary>
        /// Sets paramter name for exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public ParameterFormatException(string parameter) : base($"{parameter} improperly formatted.") { }

        /// <summary>
        /// Sets message and exception thrown when model is badly-formatted
        /// </summary>
        /// <returns>Exception indicating model was badly-formatted</returns>
        public ParameterFormatException(string message, Exception inner) : base(message, inner) { } 
    }
}