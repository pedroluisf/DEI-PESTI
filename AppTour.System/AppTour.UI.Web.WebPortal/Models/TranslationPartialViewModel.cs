using System;

namespace AppTour.UI.Web.WebPortal.Models
{
    public class TranslationPartialViewModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Controller { get; set; }
        public string ParticialView { get; set; }
    }
}