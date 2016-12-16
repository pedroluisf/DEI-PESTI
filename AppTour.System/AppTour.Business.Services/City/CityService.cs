using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.City;
using AppTour.DataAccess.Repository.City;
using AppTour.Model.Models.City;

namespace AppTour.Business.Services.City
{
    public class CityService : ICityService
    {
        public IList<CityModel> GetCities(string SearchTerm)
        {
            return new CityRepository().GetCities(SearchTerm);
        }

        public IList<CityModel> GetCities()
        {
            return new CityRepository().GetCities();
        }

        public CityModel GetCity(Guid id)
        {
            return new CityRepository().GetCity(id);
        }

        public void UpdateCity(CityModel City)
        {
            new CityRepository().UpdateCity(City);
        }

        public Guid InsertCity(CityModel City)
        {
            return new CityRepository().InsertCity(City);
        }

        public void DeleteCity(CityModel City)
        {
            new CityRepository().DeleteCity(City);
        }

    }
}
