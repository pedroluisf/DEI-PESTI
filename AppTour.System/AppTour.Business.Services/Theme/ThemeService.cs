using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Theme;
using AppTour.DataAccess.Repository.Theme;
using AppTour.Model.Models.Theme;

namespace AppTour.Business.Services.Theme
{
    public class ThemeService : IThemeService
    {
        public IList<ThemeModel> GetThemes()
        {
            return new ThemeRepository().GetThemes();
        }

        public IList<ThemeModel> GetActiveThemes()
        {
            return new ThemeRepository().GetActiveThemes();
        }

        public ThemeModel GetTheme(Guid id)
        {
            return new ThemeRepository().GetTheme(id);
        }

        public void UpdateTheme(ThemeModel theme)
        {
            new ThemeRepository().UpdateTheme(theme);
        }

        public Guid InsertTheme(ThemeModel theme)
        {
            return new ThemeRepository().InsertTheme(theme);
        }

        public void DeleteTheme(ThemeModel theme)
        {
            new ThemeRepository().DeleteTheme(theme);
        }
    }
}
