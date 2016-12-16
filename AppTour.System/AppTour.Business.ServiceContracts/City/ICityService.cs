using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.City;

namespace AppTour.Business.ServiceContracts.City
{
    [ServiceContract]
    public interface ICityService
    {
        [OperationContract]
        IList<CityModel> GetCities();
        
        [OperationContract]
        CityModel GetCity(Guid id);
        
        [OperationContract]
        void UpdateCity(CityModel city);
        
        [OperationContract]
        Guid InsertCity(CityModel city);

        [OperationContract]
        void DeleteCity(CityModel city);
    }
}
