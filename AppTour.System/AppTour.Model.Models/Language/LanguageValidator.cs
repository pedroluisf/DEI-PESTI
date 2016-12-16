using FluentValidation;

namespace AppTour.Model.Models.Language
{
    public class LanguageValidator : AbstractValidator<LanguageModel>
    {
        public LanguageValidator()
        {
            RuleFor(x => x.ISO2).NotNull().NotEmpty().Length(2).WithMessage(ViewRes.ErrorMessages.ExactLength);
        }
    }
}
