using FluentValidation;

namespace AppTour.Model.Models.Point
{
    public class PointValidator : AbstractValidator<PointModel>
    {
        public PointValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.Topics.Count).GreaterThanOrEqualTo(1).WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}


