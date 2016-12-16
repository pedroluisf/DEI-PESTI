using System;
using System.Web.Mvc;
using System.Web.Security;
using AppTour.Business.Services.User;
using AppTour.Model.Models.User;
using AppTour.UI.Web.WebPortal.Models;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class AccountController : Controller
    {

        #region + LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                UserModel utilizador = new UserService().Auth(model.UserName, model.Password, model.RememberMe);

                if (utilizador != null)
                {
                    this.Session["USERNAME"] = utilizador.UserName;
                    this.Session["REALNAME"] = utilizador.RealName;
                    this.Session["USERNAME_ROLE"] = utilizador.Role;
                    this.Session["USERNAME_KEY"] = utilizador.Id.ToString();

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", ViewRes.Login.BadLogin);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region + Login
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login(string username, string password, string remember)
        {
            bool remeberThis = (remember != null ? true : false);
            UserModel utilizador = new UserService().Auth(username, password, remeberThis);

            if (utilizador != null)
            {
                this.Session["USERNAME"] = utilizador.UserName;
                this.Session["REALNAME"] = utilizador.RealName;
                this.Session["USERNAME_ROLE"] = utilizador.Role;
                this.Session["USERNAME_KEY"] = utilizador.Id.ToString();

                FormsAuthentication.SetAuthCookie(username, remeberThis);

                return Content("Sucesso");
            }
            else
            {
                return Content(ViewRes.Login.BadLogin);
            }
        }
        #endregion

        #region + LogOff
        public ActionResult LogOff()
        {
            Session.Clear();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region + Profile
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Profile(string id)
        {
            if (id == null || id.Equals(""))
                return RedirectToAction("Index", "Home");

            Guid IdUser = new Guid(Session["USERNAME_KEY"].ToString());

            if (IdUser == null || IdUser == Guid.Empty)
                return RedirectToAction("Index", "Home");

            UserModel wantedUser = new UserService().GetUser(id);

            if (wantedUser != null && wantedUser.Id == IdUser)
                return View(wantedUser);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region + DeleteAccount
        [Authorize]
        public ActionResult DeleteAccount(string id)
        {

            return Content("Internal Error");
        }
        #endregion

        #region + ActionResult Register()
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;

                //((AppTourProvider)Membership.Provider).CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus, model.RealName);

                //Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                UserModel utilizador = new UserService().Register(model.UserName, model.Password, model.RealName, model.Email, true);


                if (utilizador != null)
                    createStatus = MembershipCreateStatus.Success;
                else
                    createStatus = MembershipCreateStatus.InvalidUserName;


                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);

                    this.Session["USERNAME"] = utilizador.UserName;
                    this.Session["REALNAME"] = utilizador.RealName;
                    this.Session["USERNAME_ROLE"] = utilizador.Role;
                    this.Session["USERNAME_KEY"] = utilizador.Id.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region + Update Profile
        public ActionResult UpdateProfile(string email, string name, string oldpassword, string password, string repeatpassword, string username)
        {
            if (email == null || email.Equals(""))
                return Content("Valores Vazios!");

            Guid IdUser = new Guid(Session["USERNAME_KEY"].ToString());

            if (IdUser == null || IdUser == Guid.Empty)
                return Content("Erro de Segurança!");

            bool isValid = Membership.ValidateUser(username, oldpassword);
            if (!isValid)
                return Content("Utilizador/Password errados!");

            // Altera a password se requisitado
            if (!password.Equals(""))
            {
                if (!password.Equals(repeatpassword))
                    return Content("Novas Passwords não coincidem!");
            }

            UserModel wantedUser = new UserService().GetUser(username);

            if (wantedUser != null && wantedUser.Id == IdUser)
            {
                wantedUser.Email = email;
                wantedUser.RealName = name;
                wantedUser.Password = oldpassword;
                wantedUser.NewPassword = (password.Equals("") ? null : password);

                this.Session["USERNAME"] = wantedUser.UserName;
                this.Session["REALNAME"] = wantedUser.RealName;

                new UserService().UpdateUser(wantedUser);

                return Content("Sucesso");
            }
            return Content("Internal Error");
        }
        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
