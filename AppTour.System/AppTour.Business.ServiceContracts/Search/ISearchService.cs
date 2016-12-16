using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Search;

namespace AppTour.Business.ServiceContracts.Search
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        IList<PointModel> Search(SearchModel searchModel);

    }
}
