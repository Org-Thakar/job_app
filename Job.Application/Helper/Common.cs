using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Job.Application.Helper
{
    public class Common
    {
        private readonly IConfiguration _configuration;

        public Common(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAccountManager()
        {
            string strAccountManagers = _configuration.GetSection("Account_Managers").Value;

            string AccMngs = strAccountManagers;

            if (AccMngs.IndexOf(',') == 0)
            {
                return string.IsNullOrEmpty(AccMngs) ? "Mr. Deepak Chettiar" : AccMngs;
            }
            else
            {
                string[] ArrayAccount = AccMngs.Split(',');
                int count = ArrayAccount.Count();
                Random rnd = new Random();

                int AccNum = rnd.Next(count);

                return AccMngs.Split(',')[AccNum];
            }

        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return "";

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public string Right(string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }

        public string SplitTwoChar(string str)
        {
            string newstr = string.Empty;
            if (str.Length > 0)
            {
                string[] split = (from Match m in Regex.Matches(str, "..") select m.Value).ToArray();

                //string[] split = new string[str.Length / 2 + (str.Length % 2 == 0 ? 0 : 1)];
                if (split.Length > 0)
                {
                    for (int i = 0; i < split.Length; i++)

                    {
                        if (i == 0)
                        {
                            newstr = split[i];
                        }
                        else
                        {
                            newstr = newstr + "-" + split[i];
                        }

                    }
                    return newstr;

                }
                else
                {
                    return str;
                }

            }
            else
            {
                return str;
            }

        }
    }
}
