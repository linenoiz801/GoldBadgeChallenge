using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Barbecue_Repository
{
    public static class StrUtils
    {
        public static string PadLeft(string str, int len)
        {
            while (str.Length < len)
                str += " ";
            return str;
        }
        public static string PadRight(string str, int len)
        {
            while (str.Length < len)
                str = " " + str;
            return str;
        }
        public static string StringOfChar(char ch, int len)
        {
            string result = "";
            while (result.Length < len)
                result += ch;
            return result;
        }


    }
}
