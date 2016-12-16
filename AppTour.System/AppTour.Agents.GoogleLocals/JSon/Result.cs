// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.GoogleLocals.JsonTypes
{

    public class Result
    {

        private JObject __jobject;
        public Result(JObject obj)
        {
            this.__jobject = obj;
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

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
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

        public string Vicinity
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "vicinity"));
            }
        }

        public double? Rating
        {
            get
            {
                return JsonClassHelper.ReadNullableFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "rating"));
            }
        }

    }
}
