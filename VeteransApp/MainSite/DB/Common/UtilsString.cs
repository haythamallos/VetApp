using System.IO;

namespace Vetapp.Engine.Common
{
    public class UtilsString
    {
        public static string createFilename(string data, string contentTypeName)
        {
            string s = null;
            if (!string.IsNullOrEmpty(data))
            {
                string[] words = data.Split();
                if (words.Length >= 2)
                {
                    s = words[1] + "-" + words[0] + "-" + contentTypeName + ".pdf";
                }
                else if (words.Length >= 1)
                {
                    s = words[0] + "-" + contentTypeName + ".pdf";
                }
                if (!IsLegalFilename(s))
                {
                    s = null;
                }
            }
            return s;
        }

        public static bool IsLegalFilename(string name)
        {
            try
            {
                var fileInfo = new FileInfo(name);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}