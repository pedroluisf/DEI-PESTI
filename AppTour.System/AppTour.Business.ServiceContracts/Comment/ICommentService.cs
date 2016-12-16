using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Comment;

namespace AppTour.Business.ServiceContracts.Comment
{
    [ServiceContract]
    public interface ICommentService
    {
        [OperationContract]
        IList<CommentModel> GetCommentsForPoint(Guid PointId);

        [OperationContract]
        IList<CommentModel> GetCommentsForEvent(Guid EventId);

        [OperationContract]
        Guid AddComment(CommentModel Comment);

        [OperationContract]
        IList<CommentModel> Report(CommentModel Comment);

        [OperationContract]
        IList<CommentModel> UpdateComment(CommentModel Comment);
    }
}
