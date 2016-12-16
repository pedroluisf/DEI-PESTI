using FluentValidation;

namespace AppTour.Model.Models.Theme
{
    public class ThemeValidator : AbstractValidator<ThemeModel>
    {
        public ThemeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
        }
    }
}
