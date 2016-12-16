// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Tools.Testes.GoogleLocalsAPITest.Detail.JsonTypes
{

    public class Result
    {

        private JObject __jobject;
        public Result(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private AddressComponent[] _addressComponents;
        public AddressComponent[] AddressComponents
        {
            get
            {
                if(_addressComponents == null)
                    _addressComponents = (AddressComponent[])JsonClassHelper.ReadArray<AddressComponent>(JsonClassHelper.GetJToken<JArray>(__jobject, "address_components"), JsonClassHelper.ReadStronglyTypedObject<AddressComponent>, typeof(AddressComponent[]));
                return _addressComponents;
            }
        }

        public string FormattedAddress
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "formatted_address"));
            }
        }

        public string FormattedPhoneNumber
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "formatted_phone_number"));
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Geometry _geometry;
        public Geometry Geometry
        {
            get
            {
                if(_geometry == null)
                    _geometry = JsonClassHelper.ReadStronglyTypedObject<Geometry>(JsonClassHelper.GetJToken<JObject>(__jobject, "geometry"));
                return _geometry;
            }
        }

        public string Icon
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "icon"));
            }
        }

        public string Id
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "id"));
            }
        }

        public string InternationalPhoneNumber
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "international_phone_number"));
            }
        }

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
            }
        }

        public double Rating
        {
            get
            {
                return JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "rating"));
            }
        }

        public string Reference
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "reference"));
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string[] _types;
        public string[] Types
        {
            get
            {
                if(_types == null)
                    _types = (string[])JsonClassHelper.ReadArray<string>(JsonClassHelper.GetJToken<JArray>(__jobject, "types"), JsonClassHelper.ReadString, typeof(string[]));
                return _types;
            }
        }

        public string Url
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "url"));
            }
        }

        public string Vicinity
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "vicinity"));
            }
        }

        public string Website
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "website"));
            }
        }

    }
}
