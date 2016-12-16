using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace AppTour.Business.Services.Helpers
{
    public static class ExtensionMethods
    {
        public static T ToContainerObject<T>(this string json)
        {
            T ret = default(T);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!string.IsNullOrEmpty(json))
            {
                ret = serializer.Deserialize<T>(json);
            }

            return ret;
        }

        public static string ToJson<T>(this IEnumerable<T> fields)
        {
            string ret = "{}";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (fields != null)
            {
                ret = serializer.Serialize(fields);
            }

            return ret;
        }

        public static string ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return (obj != null) ? serializer.Serialize(obj) : string.Empty;
        }

    }
}
