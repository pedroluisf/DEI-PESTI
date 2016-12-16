using FluentValidation;

namespace AppTour.Model.Models.User
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(ViewRes.ErrorMessages.EmailAddress);
            RuleFor(x => x.UserName).NotNull().NotEmpty().Length(3, 15).WithMessage(ViewRes.ErrorMessages.Length);
            RuleFor(x => x.RealName).NotNull().NotEmpty().Length(3, 100).WithMessage(ViewRes.ErrorMessages.Length);
        }
    }
}
