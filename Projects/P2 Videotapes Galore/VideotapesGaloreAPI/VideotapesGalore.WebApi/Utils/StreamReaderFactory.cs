using System.IO;

namespace VideotapesGalore.WebApi.Utils
{
    public static class StreamReaderFactory
    {
        /// <summary>
        /// Returns steam reader to read in JSON specific to file to read
        /// </summary>
        /// <param name="path">path to file to read</param>
        /// <returns></returns>
        public static StreamReader GetStreamReader(string path)
        {
            if (File.Exists(path)) {
                return new StreamReader(path);
            }
            return null;
        }
    }
}