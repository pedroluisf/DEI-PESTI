using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Theme;

namespace AppTour.Business.ServiceContracts.Theme
{
    public interface IThemeService
    {
        [OperationContract]
        IList<ThemeModel> GetThemes();

        [OperationContract]
        IList<ThemeModel> GetActiveThemes();

        [OperationContract]
        ThemeModel GetTheme(Guid id);

        [OperationContract]
        void UpdateTheme(ThemeModel theme);

        [OperationContract]
        Guid InsertTheme(ThemeModel theme);

        [OperationContract]
        void DeleteTheme(ThemeModel theme);
    }
}
