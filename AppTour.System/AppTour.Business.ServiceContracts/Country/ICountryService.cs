using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Country;

namespace AppTour.Business.ServiceContracts.Country
{
    [ServiceContract]
    public interface ICountryService
    {
        [OperationContract]
        IList<CountryModel> GetCountries();
        [OperationContract]
        CountryModel GetCountry(Guid id);
        [OperationContract]
        void UpdateCountry(CountryModel country);
        [OperationContract]
        Guid InsertCountry(CountryModel country);
        [OperationContract]
        void DeleteCountry(CountryModel country);
    }
}
