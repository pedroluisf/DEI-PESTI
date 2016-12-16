using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Search;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Search;
using AppTour.DataAccess.Repository.Point;

namespace AppTour.Business.Services.Service
{
    public class SearchService : ISearchService
    {
        public IList<PointModel> Search(SearchModel searchModel)
        {
            return new PointRepository().GetFromSearch(searchModel);
        }

    }
}
