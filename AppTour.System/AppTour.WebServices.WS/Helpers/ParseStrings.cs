using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections.Specialized;

namespace AppTour.WebServices.WS.Helpers
{
    public static class ParseStrings
    {
        #region + NameValueCollection ParseFromStream(string SearchTerm)
        public static NameValueCollection ParseFromStream(string SearchTerm)
        {
            NameValueCollection queryParameters = new NameValueCollection();

            string[] querySegments = SearchTerm.Split('&');
            foreach (string segment in querySegments)
            {
                string[] parts = segment.Split('=');
                if (parts.Length > 0)
                {
                    string key = parts[0].Trim(new char[] { '?', ' ' });
                    string val = parts[1].Trim();

                    queryParameters.Add(key, val);
                }
            }
            return queryParameters;

        }
        #endregion
    }
}