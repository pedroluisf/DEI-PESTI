using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AppTour.UI.Web.WebPortal.Helpers
{
    public static class EncodingHelper
    {
        #region - Atributos
        private static string Password = "ldjf8erhqr9814-1438";
        private static HashSet<char> ValidChars;
        #endregion

        #region - init()
        private static void init()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.";
            ValidChars = new HashSet<char>(chars.ToCharArray());
        }
        #endregion

        #region + static string EncodeString(this string value)
        public static string EncodeString(this string value)
        {
            if (value == null)
                return null;

            if (ValidChars == null)
                init();

            var resultBuilder = new StringBuilder();
            foreach (char currentChar in value.ToCharArray())
                if (ValidChars.Contains(currentChar))
                    resultBuilder.Append(currentChar);
                else
                {
                    byte[] bytes = System.Text.UnicodeEncoding.UTF8.GetBytes(currentChar.ToString());
                    foreach (byte currentByte in bytes)
                        resultBuilder.AppendFormat("${0:x2}", currentByte);
                }
            string result = resultBuilder.ToString();
            //Special case, use + for spaces as it is shorter and spaces are common
            return result.Replace("$20", "+");
        }
        #endregion

        #region + static string DecodeString(this string value)
        public static string DecodeString(this string value)
        {
            if (value == null)
                return value;

            if (ValidChars == null)
                init();

            //Special case, change + back to a space
            value = value.Replace("+", " ");
            var regex = new Regex(@"\$[0-9a-fA-F]{2}");
            value = regex.Replace(value,
              match =>
              {
                  string hexCode = match.Value.Substring(1, 2);
                  byte byteValue = byte.Parse(hexCode, NumberStyles.AllowHexSpecifier);
                  string decodedChar = System.Text.UnicodeEncoding.UTF32.GetString(new byte[] { byteValue });
                  return decodedChar;
              });
            return value;
        }
        #endregion
    }
}