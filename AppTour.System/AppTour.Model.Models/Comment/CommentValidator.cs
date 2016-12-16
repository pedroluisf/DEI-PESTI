using FluentValidation;

namespace AppTour.Model.Models.Comment
{
    public class CommentValidator : AbstractValidator<CommentModel>
    {
        public CommentValidator()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}
