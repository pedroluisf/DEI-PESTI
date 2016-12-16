using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Comment;
using AppTour.Model.Models.User;

namespace AppTour.DataAccess.Repository.Comment
{
    public sealed class CommentRepository
    {

        #region - GetCommentsForPoint(AppTourEntities data, Guid PointId)
        private IQueryable<CommentModel> GetCommentsForPoint(AppTourEntities data, Guid PointId)
        {
            var query = from c in data.COMMENT
                        where c.POINT.ID.Equals(PointId) && c.IS_ACTIVE
                        orderby c.CREATION_DATE
                        select new CommentModel
                        {
                            Id = c.ID,
                            Comment = c.COMMENT1,
                            CreationDate = c.CREATION_DATE,
                            IsActive = c.IS_ACTIVE,
                            IsReported = c.IS_REPORTED,
                            User = (from u in data.USER
                                    where u.ID.Equals(c.USER.ID)
                                    select new UserModel
                                    {
                                        Id = u.ID,
                                        RealName = u.REALNAME,
                                        IsActive = u.IS_ACTIVE,
                                        CreationDate = u.CREATION_DATE,
                                        UpdateDate = u.UPDATE_DATE

                                    }).FirstOrDefault()
                        };
            return query;
        }
        #endregion

        #region - GetCommentsForPoint(AppTourEntities data, Guid PointId)
        private IQueryable<CommentModel> GetCommentsForEvent(AppTourEntities data, Guid EventId)
        {
            var query = from c in data.COMMENT
                        where c.EVENT.ID.Equals(EventId) && c.IS_ACTIVE
                        orderby c.CREATION_DATE
                        select new CommentModel
                        {
                            Id = c.ID,
                            Comment = c.COMMENT1,
                            CreationDate = c.CREATION_DATE,
                            IsActive = c.IS_ACTIVE,
                            IsReported = c.IS_REPORTED,
                            User = (from u in data.USER
                                    where u.ID.Equals(c.USER.ID)
                                    select new UserModel
                                    {
                                        Id = u.ID,
                                        RealName = u.REALNAME,
                                        IsActive = u.IS_ACTIVE,
                                        CreationDate = u.CREATION_DATE,
                                        UpdateDate = u.UPDATE_DATE
                                    }).FirstOrDefault()
                        };
            return query;
        }
        #endregion

        #region + IList<CommentModel> GetCommentsForPoint(Guid PointId)
        public IList<CommentModel> GetCommentsForPoint(Guid PointId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCommentsForPoint(data, PointId).ToList();
            }
        }
        #endregion

        #region + IList<CommentModel> GetCommentsForEvent(Guid EventId)
        public IList<CommentModel> GetCommentsForEvent(Guid EventId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                IList<CommentModel> comments = this.GetCommentsForEvent(data, EventId).ToList();

                return comments;
            }
        }
        #endregion

        #region + Guid InsertComment(CommentModel Comment)
        public Guid InsertComment(CommentModel Comment)
        {

            if (Comment == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {
                COMMENT _new = new COMMENT
                {
                    ID = (Comment.Id == null || Comment.Id == Guid.Empty ? Guid.NewGuid() : Comment.Id),
                    EVENT = (Comment.Event != null ? data.EVENT.SingleOrDefault(x => x.ID.Equals(Comment.Event.Id)) : null),
                    POINT = (Comment.Point != null ? data.POINT.SingleOrDefault(x => x.ID.Equals(Comment.Point.Id)) : null),
                    IS_REPORTED = Comment.IsReported,
                    IS_ACTIVE = Comment.IsActive,
                    USER = data.USER.SingleOrDefault(x => x.ID.Equals(Comment.User.Id)),
                    COMMENT1 = Comment.Comment,
                    CREATION_DATE = DateTime.Now
                };

                data.COMMENT.AddObject(_new);
                data.SaveChanges();

                return _new.ID;
            }

        }
        #endregion
    }
}
