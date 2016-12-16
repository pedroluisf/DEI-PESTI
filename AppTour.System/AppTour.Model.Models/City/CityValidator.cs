using FluentValidation;

namespace AppTour.Model.Models.City
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
        }

    }
}
