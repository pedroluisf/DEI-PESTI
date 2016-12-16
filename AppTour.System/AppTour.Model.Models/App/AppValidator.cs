using FluentValidation;

namespace AppTour.Model.Models.App
{
    public class AppValidator : AbstractValidator<AppModel>
    {
        public AppValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
            RuleFor(x => x.Description).Length(3, 250).WithName("Description");
        }
    }
}
