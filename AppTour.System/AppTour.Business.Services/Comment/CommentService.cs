using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Comment;
using AppTour.DataAccess.Repository.Comment;
using AppTour.Model.Models.Comment;

namespace AppTour.Business.Services.Comment
{
    public class CommentService : ICommentService
    {
        public IList<CommentModel> GetCommentsForPoint(Guid PointId)
        {
            throw new NotImplementedException();
        }

        public IList<CommentModel> GetCommentsForEvent(Guid EventId)
        {
            throw new NotImplementedException();
        }

        public Guid AddComment(CommentModel Comment)
        {
            return new CommentRepository().InsertComment(Comment);
        }

        public IList<CommentModel> Report(CommentModel Comment)
        {
            throw new NotImplementedException();
        }

        public IList<CommentModel> UpdateComment(CommentModel Comment)
        {
            throw new NotImplementedException();
        }
    }
}
