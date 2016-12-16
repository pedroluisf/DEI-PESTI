using FluentValidation;

namespace AppTour.Model.Models.Enterprise
{
    public class EnterpriseValidator : AbstractValidator<EnterpriseModel>
    {
        public EnterpriseValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3,30).WithName("Name");
            RuleFor(x => x.Description).Length(3, 250).WithName("Description");
        }
    }

}
