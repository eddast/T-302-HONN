namespace VideotapesGalore.Services.Interfaces
{
    /// <summary>
    /// Defines error logging action in system for global error handling
    /// </summary>
    public interface ITapeService
    {
        /// <summary>
        /// Logs error message of exception to logfile
        /// </summary>
        /// <param name="message">error message to log to logfile</param>
         void LogToFile(string message);
    }
}