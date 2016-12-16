using FluentValidation;

namespace AppTour.Model.Models.Country
{
    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
            RuleFor(x => x.ISO).NotNull().NotEmpty().Length(2).WithMessage(ViewRes.ErrorMessages.ExactLength);
            RuleFor(x => x.ISO3).NotNull().NotEmpty().Length(3).WithMessage(ViewRes.ErrorMessages.ExactLength);
        }
    }
}
