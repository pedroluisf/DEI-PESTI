// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.GoogleLocals.CoordenateResolver
{

    public class ResolveCoordenate
    {

        public ResolveCoordenate(string json)
            : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public ResolveCoordenate(JObject obj)
        {
            this.__jobject = obj;
        }

        public string Latitude
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "latitude"));
            }
        }

        public string Cp4
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "cp4"));
            }
        }

        public string Longitude
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "longitude"));
            }
        }

    }
}
