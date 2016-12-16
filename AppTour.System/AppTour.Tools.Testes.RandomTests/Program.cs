using System;
using System.Text;

namespace AppTour.Tools.Testes.RandomTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string value = "39.822986\r\n";

            Console.WriteLine("{0} => {1}<-", RemoveSpecialCharacters(value), RemoveSpecialCharacters(value));
            Console.ReadKey();
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') | c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
