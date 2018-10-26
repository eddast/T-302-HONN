using System.IO;

namespace VideotapesGalore.WebApi.Utils
{
    public static class StreamReaderFactory
    {
        public static StreamReader GetStreamReader(string path)
        {
            if (File.Exists(path)) {
                return new StreamReader(path);
            }
            return null;
        }
    }
}