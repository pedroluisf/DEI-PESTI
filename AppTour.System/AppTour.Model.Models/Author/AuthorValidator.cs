using FluentValidation;

namespace AppTour.Model.Models.Author
{
    public class AuthorValidator : AbstractValidator<AuthorModel>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
