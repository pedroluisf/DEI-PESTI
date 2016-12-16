using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppTour.Model.Models.App_Start.RegisterClientValidationExtensions), "Start")]

namespace AppTour.Model.Models.App_Start
{
    public static class RegisterClientValidationExtensions
    {
        public static void Start()
        {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
        }
    }
}