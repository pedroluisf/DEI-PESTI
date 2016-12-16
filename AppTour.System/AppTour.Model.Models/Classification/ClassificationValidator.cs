using FluentValidation;

namespace AppTour.Model.Models.Classification
{
    public class ClassificationValidator : AbstractValidator<ClassificationModel>
    {
        public ClassificationValidator()
        {
            RuleFor(x => x.Classification).NotEmpty().InclusiveBetween(1, 5).WithMessage(string.Format(ViewRes.ErrorMessages.ValuesBetween, 1, 5));
        }
    }
}
