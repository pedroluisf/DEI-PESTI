
using FluentValidation;
namespace AppTour.Model.Models.Translation
{
    public class TranslationValidator : AbstractValidator<TranslationModel>
    {
        public TranslationValidator()
        {
            RuleFor(x => x.Value).NotNull().NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.Language).NotNull().NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.FieldName).NotNull().NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.TableName).NotNull().NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}
