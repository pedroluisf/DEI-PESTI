// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using AppTour.Tools.Testes.GoogleLocalsAPITest.JsonTypes;
using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Tools.Testes.GoogleLocalsAPITest
{

    public class GoogleLocalsJSon
    {

        public GoogleLocalsJSon(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public GoogleLocalsJSon(JObject obj)
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
        private Result[] _results;
        public Result[] Results
        {
            get
            {
                if(_results == null)
                    _results = (Result[])JsonClassHelper.ReadArray<Result>(JsonClassHelper.GetJToken<JArray>(__jobject, "results"), JsonClassHelper.ReadStronglyTypedObject<Result>, typeof(Result[]));
                return _results;
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
