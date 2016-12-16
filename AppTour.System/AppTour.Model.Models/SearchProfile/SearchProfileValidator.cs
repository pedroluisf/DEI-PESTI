using AppTour.Model.Models.Search;
using FluentValidation;

namespace AppTour.Model.Models.SearchProfile
{
    public class SearchProfileValidator : AbstractValidator<SearchProfileModel>
    {
        public SearchProfileValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}

