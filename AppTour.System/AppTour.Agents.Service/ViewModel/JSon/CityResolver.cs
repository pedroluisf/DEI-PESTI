// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.Service.ViewModel.JSon
{

    public class CityResolver
    {

        public CityResolver(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public CityResolver(JObject obj)
        {
            this.__jobject = obj;
        }

        public string Arteria
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "arteria"));
            }
        }

        public string Localidade
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "localidade"));
            }
        }

        public string Troco
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "troco"));
            }
        }

        public string LocalOuZona
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "local ou zona"));
            }
        }

        public string Cp7
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "cp7"));
            }
        }

    }
}
