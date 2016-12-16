using AppTour.Business.Services.User;
using AppTour.Model.Models.User;
using System;

namespace AppTour.WebServices.AppTourWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Login" in code, svc and config file together.
    public class Login : ILogin
    {
        public UserModel Authentication(string username, string password)
        {
            try
            {
                return new UserService().Auth(username, password, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
