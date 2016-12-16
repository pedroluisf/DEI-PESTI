// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.GoogleLocals.JsonTypes
{

    public class Geometry
    {

        private JObject __jobject;
        public Geometry(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Location _location;
        public Location Location
        {
            get
            {
                if(_location == null)
                    _location = JsonClassHelper.ReadStronglyTypedObject<Location>(JsonClassHelper.GetJToken<JObject>(__jobject, "location"));
                return _location;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Viewport _viewport;
        public Viewport Viewport
        {
            get
            {
                if(_viewport == null)
                    _viewport = JsonClassHelper.ReadStronglyTypedObject<Viewport>(JsonClassHelper.GetJToken<JObject>(__jobject, "viewport"));
                return _viewport;
            }
        }

    }
}
