using FluentValidation;

namespace AppTour.Model.Models.Topic
{
    public class TopicValidator : AbstractValidator<TopicModel>
    {
        public TopicValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}
