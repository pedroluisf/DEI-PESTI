using System.Collections.Generic;

namespace AppTour.Agents.Service.ViewModel
{
    public class PointViewModel
    {

        public string Reference { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public CityViewModel City { get; set; }

        public string Coordenate { get; set; }

        public string PhoneNumber { get; set; }

        public string URL { get; set; }

        public string SourceURL { get; set; }

        public bool IsActive { get; set; }

        public IList<string> Topics { get; set; }

        public IList<string> Pictures { get; set; }

    }
}
