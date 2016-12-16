using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.User;

namespace AppTour.Business.ServiceContracts.User
{
    public interface IUserService
    {
        [OperationContract]
        IList<UserModel> GetUsers();

        [OperationContract]
        UserModel GetUser(Guid id);

        [OperationContract]
        UserModel GetUser(string username);

        [OperationContract]
        void UpdateUser(UserModel user);

        [OperationContract]
        Guid InsertUser(UserModel user);

        [OperationContract]
        void DeleteUser(UserModel user);

        [OperationContract]
        UserModel Auth(string username, string password, bool remember);

        [OperationContract]
        UserModel Register(string username, string password, string realname, string email, bool isApproved);

        [OperationContract]
        bool DeleteAccount(string username);

    }
}
