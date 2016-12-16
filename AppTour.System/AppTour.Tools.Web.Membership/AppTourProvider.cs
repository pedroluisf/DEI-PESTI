using System;
using System.Web.Security;
using System.Web.Configuration;
using AppTour.Model.Models.User;
using AppTour.Business.Services.User;

namespace AppTour.Tools.Web.Membership
{
    public sealed class AppTourProvider : MembershipProvider
    {
        #region Class Variables
        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;
        private string remoteProviderName;
        private MachineKeySection machineKey; //Used when determining encryption key values.

        #endregion

        #region Enums

        private enum FailureType
        {
            Password = 1,
            PasswordAnswer = 2
        }

        #endregion

        #region Properties
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return enablePasswordRetrieval;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return requiresUniqueEmail;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return passwordFormat;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return minRequiredPasswordLength;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return passwordStrengthRegularExpression;
            }
        }
        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {

            // Initialize the abstract base class.


            this.applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(this.applicationName))
            {
                this.applicationName = "/";
            }

            this.enablePasswordRetrieval = ProviderUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            this.enablePasswordReset = ProviderUtility.GetBooleanValue(config, "enablePasswordReset", true);
            this.requiresQuestionAndAnswer = ProviderUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            this.requiresUniqueEmail = ProviderUtility.GetBooleanValue(config, "requiresUniqueEmail", true);
            this.maxInvalidPasswordAttempts = ProviderUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            this.passwordAttemptWindow = ProviderUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            this.minRequiredPasswordLength = ProviderUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 0x80);
            this.minRequiredNonAlphanumericCharacters = ProviderUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 0x80);
            this.passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];

            if (config["passwordFormat"] != null)
            {
                this.passwordFormat = (MembershipPasswordFormat)Enum.Parse(typeof(MembershipPasswordFormat), config["passwordFormat"]);
            }
            else
            {
                this.passwordFormat = MembershipPasswordFormat.Hashed;
            }

            this.remoteProviderName = config["remoteProviderName"];
            base.Initialize(name, config);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return GetProvider(remoteProviderName, applicationName).ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return GetProvider(remoteProviderName, applicationName).ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status, string Realname)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }
                MembershipUser user = new MembershipUser(remoteProviderName, username, providerUserKey, email, passwordQuestion, null, isApproved, false, createDate, createDate, createDate, createDate, createDate);
                
                Roles.AddUserToRole(username, "Utilizador");

                UserService userService = new UserService();
                Guid Id = userService.InsertUser(new UserModel
                {
                    Id = (Guid)providerUserKey,
                    RealName = Realname,
                    CreationDate = createDate,
                    UpdateDate = createDate
                });

                if (Id != Guid.Empty)
                {
                    status = MembershipCreateStatus.Success;
                    return user;
                }
                else
                {
                    status = MembershipCreateStatus.ProviderError;
                    return null;
                }
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status, "No Real Name");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            UserService service = new UserService();
            MembershipUser _user = this.GetUser(username, false);

            UserModel usrToDelete = service.GetUser((Guid)_user.ProviderUserKey);
            service.DeleteUser(usrToDelete);

            return GetProvider(remoteProviderName, applicationName).DeleteUser(username, deleteAllRelatedData);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProvider(remoteProviderName, applicationName).FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProvider(remoteProviderName, applicationName).FindUsersByName(usernameToMatch, pageIndex, pageIndex, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProvider(remoteProviderName, applicationName).GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline()
        {
            return GetProvider(remoteProviderName, applicationName).GetNumberOfUsersOnline();
        }

        public override string GetPassword(string username, string answer)
        {
            return GetProvider(remoteProviderName, applicationName).GetPassword(username, answer);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return GetProvider(remoteProviderName, applicationName).GetUser(username, userIsOnline);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetProvider(remoteProviderName, applicationName).GetUser(providerUserKey, userIsOnline);
        }

        public override string GetUserNameByEmail(string email)
        {
            string str = GetProvider(remoteProviderName, applicationName).GetUserNameByEmail(email);
            return str;
        }

        public override string ResetPassword(string username, string answer)
        {
            return GetProvider(remoteProviderName, applicationName).ResetPassword(username, answer);
        }

        public override bool UnlockUser(string userName)
        {
            return GetProvider(remoteProviderName, applicationName).UnlockUser(userName);
        }

        public override void UpdateUser(MembershipUser user)
        {
            GetProvider(remoteProviderName, applicationName).UpdateUser(user);
        }

        public void UpdateUser(MembershipUser user, UserModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException();

            this.UpdateUser(user);
            new UserService().UpdateUser(userModel);
        }

        public override bool ValidateUser(string username, string password)
        {
            return GetProvider(remoteProviderName, applicationName).ValidateUser(username, password);
        }

        private System.Web.Security.MembershipProvider GetProvider(string providerName, string applicationName)
        {
            System.Web.Security.MembershipProvider provider;
            if ((providerName != null) && (System.Web.Security.Membership.Providers[providerName] != null))
            {
                provider = System.Web.Security.Membership.Providers[providerName];
            }
            else
            {
                provider = System.Web.Security.Membership.Provider;
            }

            if (applicationName != null)
            {
                provider.ApplicationName = applicationName;
            }

            return provider;
        }

        private System.Web.Security.MembershipUserCollection BuildUserCollection(MembershipUser[] list)
        {
            if (list == null) return null;
            System.Web.Security.MembershipUserCollection collection = new System.Web.Security.MembershipUserCollection();
            foreach (MembershipUser user in list)
            {
                collection.Add(user);
            }
            return collection;
        }
    }
}
