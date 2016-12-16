using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Country;
using AppTour.DataAccess.Repository.Country;
using AppTour.Model.Models.Country;

namespace AppTour.Business.Services.Country
{
    public class CountryService : ICountryService
    {
        public IList<CountryModel> GetCountries()
        {
            return new CountryRepository().GetCountries();
        }

        public CountryModel GetCountry(Guid id)
        {
            return new CountryRepository().GetCountry(id);
        }

        public void UpdateCountry(CountryModel country)
        {
            new CountryRepository().UpdateCountry(country);
        }

        public Guid InsertCountry(CountryModel country)
        {
            return new CountryRepository().InsertCountry(country);
        }

        public void DeleteCountry(CountryModel country)
        {
            new CountryRepository().DeleteCountry(country);
        }

        public CountryModel GetCountry(string ISO)
        {
            return new CountryRepository().GetCountry(ISO);
        }
    }
}
