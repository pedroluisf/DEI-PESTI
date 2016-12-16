using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Author;
using AppTour.Model.Models.Enterprise;

namespace AppTour.DataAccess.Repository.Author
{
    public sealed class AuthorRepository
    {
        #region IQueryable GetAuthors
        private IQueryable<AuthorModel> GetAuthors(AppTourEntities data)
        {
            var query = from c in data.AUTHOR
                        orderby c.NAME
                        select new AuthorModel
                        {
                            Id = c.ID,
                            Enterprise = new EnterpriseModel
                            {
                                Id = c.ENTERPRISE.ID,
                                Name = c.ENTERPRISE.NAME,
                                CreationDate = c.ENTERPRISE.CREATION_DATE,
                                UpdateDate = c.ENTERPRISE.UPDATE_DATE
                            },
                            Name = c.NAME,
                            RealName = c.REALNAME,
                            Email = c.EMAIL,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region Public GetAuthors
        public IList<AuthorModel> GetAuthors()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetAuthors(data).ToList();
            }
        }
        #endregion

        #region  GetAuthor(Guid id)
        public AuthorModel GetAuthor(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetAuthors(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateAuthor(AuthorModel Author)
        public void UpdateAuthor(AuthorModel Author)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                AUTHOR current = data.AUTHOR.Where(x => Author.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.ENTERPRISE = data.ENTERPRISE.Where(f => f.ID == Author.Id).FirstOrDefault();
                        current.NAME = Author.Name;
                        current.REALNAME = Author.RealName;
                        current.EMAIL = Author.Email;
                        current.CREATION_DATE = Author.CreationDate;
                        current.UPDATE_DATE = DateTime.Now;

                        data.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region InsertAuthor(AuthorModel Author)
        public Guid InsertAuthor(AuthorModel Author)
        {
            Guid id = Guid.NewGuid();
            if (Author == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {
                AUTHOR _new = new AUTHOR
                {
                    ID = id,
                    ENTERPRISE = data.ENTERPRISE.Where(f => f.ID == Author.Enterprise.Id).FirstOrDefault(),
                    NAME = Author.Name,
                    REALNAME = Author.RealName,
                    EMAIL = Author.Email,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.AUTHOR.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteAuthor(AuthorModel Author)
        public void DeleteAuthor(AuthorModel Author)
        {
            if (Author == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                AUTHOR current = data.AUTHOR.Where(p => p.ID == Author.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
                        data.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }

        }
        #endregion
    }
}
