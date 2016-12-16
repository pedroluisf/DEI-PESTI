using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AppTour.Business.Services.User;
using AppTour.Model.Models.User;

namespace AppTour.WebServices.WS_AppTour
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AppTourWS" in both code and config file together.
    public class AppTourWS : IAppTourWS
    {
        public void DoWork()
        {
        }


        public UserModel Authentication(string username, string password)
        {
            try
            {
                return new UserService().Auth(username, password, false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
