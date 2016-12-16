using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Author;

namespace AppTour.Business.ServiceContracts.Author
{
    [ServiceContract]
    public interface IAuthorService
    {
        [OperationContract]
        IList<AuthorModel> GetAuthors();
        [OperationContract]
        AuthorModel GetAuthor(Guid id);
        [OperationContract]
        void UpdateAuthor(AuthorModel author);
        [OperationContract]
        Guid InsertAuthor(AuthorModel author);
        [OperationContract]
        void DeleteAuthor(AuthorModel author);
    }
}
