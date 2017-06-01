using System;
using System.IO;
using System.Linq;

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
                if (words.Length > 2)
                {
                    s = words[2] + "-" + words[0] + "-" + contentTypeName + ".pdf";
                }
                else if (words.Length == 2)
                {
                    s = words[1] + "-" + words[0] + "-" + contentTypeName + ".pdf";
                }
                else if (words.Length == 1)
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

        public static SSN ParseSSN(string data)
        {
            SSN ssn = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    string ssnFiltered = new String(data.Where(x => Char.IsDigit(x)).ToArray());
                    if (ssnFiltered.Length == 9)
                    {
                        ssn = new SSN();
                        ssn.LeftPart = ssnFiltered.Substring(0, 3);
                        ssn.MiddlePart = ssnFiltered.Substring(3, 2);
                        ssn.RightPart = ssnFiltered.Substring(5, 4);
                    }
                }
            }
            catch
            {
            }
            return ssn;
        }

        public static string OnlyDigits(string data)
        {
            string justNumbers = null;
            if (!string.IsNullOrEmpty(data))
            {
                justNumbers = new String(data.Where(Char.IsDigit).ToArray());
            }
            return justNumbers;
        }

        public static NameSet ParseFullname(string fullname)
        {
            NameSet o = new NameSet();
            if (!string.IsNullOrEmpty(fullname))
            {
                string[] words = fullname.Split();
                if (words.Length > 2)
                {
                    o.FirstName = words[0].Trim();
                    o.MiddleInitial = words[1].Substring(0, 1).Trim();
                    o.LastName = words[2].Trim();
                }
                else if (words.Length > 1)
                {
                    o.FirstName = words[0].Trim();
                    o.LastName = words[1].Trim();
                }
                else
                {
                    o.FirstName = words[0].Trim();
                }
            }
            return o;
        }

    }

    public class SSN
    {
        public string LeftPart { get; set; }
        public string MiddlePart { get; set; }
        public string RightPart { get; set; }

        public override string ToString()
        {
            string data = string.Empty;

            if (!string.IsNullOrEmpty(LeftPart))
            {
                data += LeftPart;
            }
            if (!string.IsNullOrEmpty(MiddlePart))
            {
                data += "-" + MiddlePart;
            }
            if (!string.IsNullOrEmpty(RightPart))
            {
                data += "-" + RightPart;
            }

            return data;
        }
    }

    public class NameSet
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
    }
}