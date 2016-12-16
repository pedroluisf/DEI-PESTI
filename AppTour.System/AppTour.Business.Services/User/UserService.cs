using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AppTour.Business.ServiceContracts.User;
using AppTour.DataAccess.Repository.User;
using AppTour.Model.Models.User;
using AppTour.Model.Models.SearchProfile;
using AppTour.Business.Services.Topic;
using AppTour.Business.Services.SearchProfile;

namespace AppTour.Business.Services.User
{
    public class UserService : IUserService
    {
        #region + IList<UserModel> GetUsers()
        public IList<UserModel> GetUsers()
        {
            return new UserRepository().GetUsers();
        }
        #endregion

        public UserModel GetUser(Guid id)
        {
            MembershipUser membershipUser = Membership.GetUser(id);

            if (membershipUser != null)
            {
                Guid UserGUID = (Guid)membershipUser.ProviderUserKey;

                UserModel myUser = new UserRepository().GetActiveUser(UserGUID);

                myUser.UserName = membershipUser.UserName;
                myUser.Email = membershipUser.Email;
                myUser.Role = Roles.GetRolesForUser(membershipUser.UserName).FirstOrDefault().ToString();

                return myUser;
            }
            return null;
        }

        public void UpdateUser(UserModel user)
        {
            MembershipUser membershipUser = Membership.GetUser(user.UserName, true);

            membershipUser.Email = user.Email;

            if (user.NewPassword != null && !user.NewPassword.Equals(""))
                membershipUser.ChangePassword(user.Password, user.NewPassword);

            Membership.UpdateUser(membershipUser);

            new UserRepository().UpdateUser(user);
        }

        public Guid InsertUser(UserModel user)
        {
            Guid userId = new UserRepository().InsertUser(user);

            SearchProfileModel myDefaultSearchProfile = new SearchProfileModel
            {
                Id = Guid.NewGuid(),
                Name = "Default",
                EventsSearchDays = 10,
                IsActive = true,
                PointsRangeDistance = 5,
                SearchCriteria = string.Empty,
                Utilizador = GetUser(userId),
                SearchProfileTopics = new TopicService().GetActiveTopics()
            };

            Guid id = new SearchProfileService().InsertSearchProfile(myDefaultSearchProfile);
            return userId;
        }

        public void DeleteUser(UserModel user)
        {
            new UserRepository().DeleteUser(user);
        }

        public UserModel Auth(string username, string password, bool remember)
        {
            if (Membership.ValidateUser(username, password))
            {
                MembershipUser membershipUser = Membership.GetUser(username);

                if (membershipUser != null)
                {
                    Guid UserGUID = (Guid)membershipUser.ProviderUserKey;

                    UserModel myUser = new UserRepository().GetActiveUser(UserGUID);

                    myUser.Email = membershipUser.Email;
                    myUser.Role = Roles.GetRolesForUser(username).First().ToString();
                    myUser.UserName = membershipUser.UserName;

                    return myUser;
                }
            }
            return null;
        }

        public bool UsernameValidation(string username)
        {
            MembershipUser membershipUser = Membership.GetUser(username);
            if (membershipUser != null)
                return false;
            else
                return true;
        }
        public bool EmailValidation(string email)
        {
            int c = Membership.FindUsersByEmail(email).Count;
            if (c == 0)
                return true;
            else
                return false;
        }

        public UserModel Register(string username, string password, string realname, string email, bool isApproved)
        {
            // Membership Stuff
            MembershipCreateStatus createStatus;
            Membership.CreateUser(username, password, email, null, null, true, null, out createStatus);
            Roles.AddUserToRole(username, "Utilizador");

            if (createStatus == MembershipCreateStatus.Success)
            {
                MembershipUser _user = Membership.GetUser(username);
                Guid IdUser = (Guid)_user.ProviderUserKey;
                UserModel newUser = new UserModel
                {
                    Id = IdUser,
                    RealName = realname,
                    CreationDate = _user.CreationDate,
                    UpdateDate = _user.CreationDate,
                    Role = Roles.GetRolesForUser(username).First().ToString(),
                    Email = email,
                    IsActive = isApproved,
                    UserName = username
                };
                Guid inserted = InsertUser(newUser);

                return newUser;
            }
            return null;
        }

        public UserModel GetUser(string username)
        {
            MembershipUser membershipUser = Membership.GetUser(username);
            if (membershipUser != null)
            {
                Guid UserGUID = (Guid)membershipUser.ProviderUserKey;

                UserModel myUser = new UserRepository().GetActiveUser(UserGUID);

                myUser.UserName = username;
                myUser.Email = membershipUser.Email;
                myUser.Role = Roles.GetRolesForUser(username).First().ToString();
                myUser.UserName = membershipUser.UserName;

                return myUser;
            }
            return null;
        }

        public bool DeleteAccount(string username)
        {
            UserModel myUser = GetUser(username);

            if (myUser == null)
                return false;

            return new UserRepository().Deactivate(myUser);
        }
    }
}
