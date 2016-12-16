using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace AppTour.Model.Models.Search
{
    public class SearchValidator : AbstractValidator<SearchModel>
    {
        public SearchValidator()
        {
            RuleFor(x => x.Terms).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
            RuleFor(x => x.Coordenate).NotEmpty().WithMessage(ViewRes.ErrorMessages.Required);
        }
    }
}
