using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Author;
using AppTour.DataAccess.Repository.Author;
using AppTour.Model.Models.Author;

namespace AppTour.Business.Services.Author
{
    public class AuthorService : IAuthorService
    {

        public IList<AuthorModel> GetAuthors()
        {
            return new AuthorRepository().GetAuthors();
        }

        public AuthorModel GetAuthor(Guid id)
        {
            return new AuthorRepository().GetAuthor(id);
        }

        public void UpdateAuthor(AuthorModel author)
        {
            new AuthorRepository().UpdateAuthor(author);
        }

        public Guid InsertAuthor(AuthorModel author)
        {
            return new AuthorRepository().InsertAuthor(author);
        }

        public void DeleteAuthor(AuthorModel author)
        {
            new AuthorRepository().DeleteAuthor(author);
        }
    }
}
