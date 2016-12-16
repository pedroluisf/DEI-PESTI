using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace AppTour.Tools.Web.Membership
{
    sealed internal class ProviderUtility
    {
        /// <summary>
        /// Get a default application name
        /// </summary>
        /// <returns></returns>
        internal static string GetDefaultAppName()
        {
            string defPath;
            try
            {
                string vPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
                if (string.IsNullOrEmpty(vPath))
                {
                    vPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
                    int num1 = vPath.IndexOf('.');
                    if (num1 != -1)
                    {
                        vPath = vPath.Remove(num1);
                    }
                }
                if (string.IsNullOrEmpty(vPath))
                {
                    return "/";
                }
                defPath = vPath;
            }
            catch
            {
                defPath = "/";
            }
            return defPath;
        }

        /// <summary>
        /// return a value from a collection of a default value if not in that collection
        /// </summary>
        /// <param name="config">the collection</param>
        /// <param name="valueName">the value to look up</param>
        /// <param name="defaultValue">the default value</param>
        /// <returns>a value from the collection or the default value</returns>
        internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            bool result;
            string valueToParse = config[valueName];
            if (valueToParse == null)
            {
                return defaultValue;
            }
            if (bool.TryParse(valueToParse, out result))
            {
                return result;
            }
            throw new Exception("Value must be boolean");
        }

        /// <summary>
        /// return a value from a collection of a default value if not in that collection
        /// </summary>
        /// <param name="config">a collection</param>
        /// <param name="valueName">the name of the value in the collection</param>
        /// <param name="defaultValue">a default value</param>
        /// <param name="zeroAllowed">is zero allowed</param>
        /// <param name="maxValueAllowed">what is the largest number that will be accepted</param>
        /// <returns>a value</returns>
        internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            int result;
            string valueToParse = config[valueName];
            if (valueToParse == null)
            {
                return defaultValue;
            }
            if (!int.TryParse(valueToParse, out result))
            {
                if (zeroAllowed)
                {
                    throw new Exception("Value must be non negative integer");
                }
                throw new Exception("Value must be positive integer");
            }
            if (zeroAllowed && (result < 0))
            {
                throw new Exception("Value must be non negative integer");
            }
            if (!zeroAllowed && (result <= 0))
            {
                throw new Exception("Value must be positive integer");
            }
            if ((maxValueAllowed > 0) && (result > maxValueAllowed))
            {
                throw new Exception("Value too big");
            }
            return result;
        }
    }
}
