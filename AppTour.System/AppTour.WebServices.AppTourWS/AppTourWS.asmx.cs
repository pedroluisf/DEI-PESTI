using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AppTour.Business.ServiceContracts.User;
using AppTour.Business.Services.User;
using AppTour.Model.Models.User;

namespace AppTour.WebServices.AppTourWS
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AppTourWS : System.Web.Services.WebService
    {

        [WebMethod]
        public UserModel Authentication(String username, String password)
        {
            try
            {
                return new UserService().Auth(username, password, false);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}