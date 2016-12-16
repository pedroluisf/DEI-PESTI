using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.User;

namespace AppTour.DataAccess.Repository.User
{
    public sealed class UserRepository
    {
        #region - IQueryable<UserModel> GetUsers(AppTourEntities data)
        private IQueryable<UserModel> GetUsers(AppTourEntities data)
        {

            var query = from c in data.USER
                        select new UserModel
                        {
                            Id = c.ID,
                            RealName = c.REALNAME,
                            IsActive = c.IS_ACTIVE,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region + IList<UserModel> GetUsers()
        public IList<UserModel> GetUsers()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetUsers(data).ToList();
            }
        }
        public IList<UserModel> GetActiveUsers()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetUsers(data).Where(x => x.IsActive == true).ToList();
            }
        }
        #endregion

        #region + UserModel GetUser(Guid id)
        public UserModel GetUser(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetUsers(data).SingleOrDefault(x => x.Id == id);
            }
        }

        public UserModel GetActiveUser(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetUsers(data).Where(x => x.Id == id && x.IsActive == true).First();
            }
        }
        #endregion

        #region + Guid InsertUser(UserModel user)
        public Guid InsertUser(UserModel user)
        {
            if (user == null)
                throw new NullReferenceException("user");

            using (AppTourEntities data = new AppTourEntities())
            {
                USER _new = new USER
                {
                    ID = (user.Id == Guid.Empty ? Guid.NewGuid() : user.Id),
                    REALNAME = user.RealName,
                    IS_ACTIVE = user.IsActive,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.USER.AddObject(_new);
                data.SaveChanges();

                return _new.ID;
            }
        }
        #endregion

        #region UpdateUser(UserModel theme)
        public void UpdateUser(UserModel user)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                USER current = data.USER.Where(x => user.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.REALNAME = user.RealName;
                        current.IS_ACTIVE = user.IsActive;
                        current.CREATION_DATE = user.CreationDate;
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

        #region DeleteUser(UserModel theme)
        public void DeleteUser(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                USER current = data.USER.Where(p => p.ID == user.Id).SingleOrDefault();
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

        #region Deactivate User
        public bool Deactivate(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            using (AppTourEntities data = new AppTourEntities())
            {
                USER current = data.USER.Where(p => p.ID == user.Id).SingleOrDefault();
                if (current != null)
                {

                    try
                    {
                        current.IS_ACTIVE = false;
                        current.UPDATE_DATE = DateTime.Now;

                        data.SaveChanges();

                        return true;
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
                return false;
            }

        }
        #endregion
    }
}
