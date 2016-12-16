// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Tools.Testes.GoogleLocalsAPITest.Detail.JsonTypes
{

    public class Location
    {

        private JObject __jobject;
        public Location(JObject obj)
        {
            this.__jobject = obj;
        }

        public double Lat
        {
            get
            {
                return JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "lat"));
            }
        }

        public double Lng
        {
            get
            {
                return JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "lng"));
            }
        }

    }
}
