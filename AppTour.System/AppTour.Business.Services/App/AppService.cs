using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.App;
using AppTour.DataAccess.Repository.App;
using AppTour.Model.Models.App;

namespace AppTour.Business.Services.App
{
    public class AppService : IAppService
    {
        public IList<AppModel> GetApps()
        {
            return new AppRepository().GetApps();
        }

        public AppModel GetApp(Guid id)
        {
            return new AppRepository().GetApp(id);
        }

        public void UpdateApp(AppModel App)
        {
            new AppRepository().UpdateApp(App);
        }

        public Guid InsertApp(AppModel App)
        {
            return new AppRepository().InsertApp(App);
        }

        public void DeleteApp(AppModel App)
        {
            new AppRepository().DeleteApp(App);
        }
    }
}
