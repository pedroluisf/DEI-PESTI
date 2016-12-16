﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using JsonCSharpClassGenerator;
using Newtonsoft.Json.Linq;

namespace AppTour.Agents.GoogleLocals.JsonTypes
{

    public class Viewport
    {

        private JObject __jobject;
        public Viewport(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Northeast _northeast;
        public Northeast Northeast
        {
            get
            {
                if(_northeast == null)
                    _northeast = JsonClassHelper.ReadStronglyTypedObject<Northeast>(JsonClassHelper.GetJToken<JObject>(__jobject, "northeast"));
                return _northeast;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Southwest _southwest;
        public Southwest Southwest
        {
            get
            {
                if(_southwest == null)
                    _southwest = JsonClassHelper.ReadStronglyTypedObject<Southwest>(JsonClassHelper.GetJToken<JObject>(__jobject, "southwest"));
                return _southwest;
            }
        }

    }
}
