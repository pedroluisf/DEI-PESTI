// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using AppTour.Agents.GoogleLocals.Detail.JsonTypes;
using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.GoogleLocals.Detail
{

    public class GoogleLocalsDetailAPI
    {

        public GoogleLocalsDetailAPI(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public GoogleLocalsDetailAPI(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private object[] _htmlAttributions;
        public object[] HtmlAttributions
        {
            get
            {
                if(_htmlAttributions == null)
                    _htmlAttributions = (object[])JsonClassHelper.ReadArray<object>(JsonClassHelper.GetJToken<JArray>(__jobject, "html_attributions"), JsonClassHelper.ReadObject, typeof(object[]));
                return _htmlAttributions;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Result _result;
        public Result Result
        {
            get
            {
                if(_result == null)
                    _result = JsonClassHelper.ReadStronglyTypedObject<Result>(JsonClassHelper.GetJToken<JObject>(__jobject, "result"));
                return _result;
            }
        }

        public string Status
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "status"));
            }
        }

    }
}
