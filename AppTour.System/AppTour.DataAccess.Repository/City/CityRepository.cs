using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.City;
using AppTour.Model.Models.Country;

namespace AppTour.DataAccess.Repository.City
{
    public sealed class CityRepository
    {
        #region - IQueryable GetCities(AppTourEntities data)
        private IQueryable<CityModel> GetCities(AppTourEntities data)
        {
            var query = from c in data.CITY
                        orderby c.NAME
                        select new CityModel
                        {
                            Id = c.ID,
                            Country = (from p in data.COUNTRY
                                       where p.ID == c.COUNTRY.ID
                                       select new CountryModel
                                       {
                                           CountryCode = p.COUNTRY_CODE,
                                           Id = p.ID,
                                           CountryName = p.COUNTRY_NAME,
                                           ISO = p.ISO,
                                           ISO3 = p.ISO3,
                                           Name = p.NAME
                                       }).FirstOrDefault(),
                            Name = c.NAME
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetCitiesWithTranslations(AppTourEntities data, string ISO2)
        private IQueryable<CityModel> GetCitiesWithTranslations(AppTourEntities data, string ISO2)
        {
            var query = from c in data.CITY
                        orderby c.NAME
                        select new CityModel
                        {
                            Id = c.ID,
                            Country = (from p in data.COUNTRY
                                       where p.ID == c.COUNTRY.ID
                                       select new CountryModel
                                       {
                                           CountryCode = p.COUNTRY_CODE,
                                           Id = p.ID,
                                           CountryName = p.COUNTRY_NAME,
                                           ISO = p.ISO,
                                           ISO3 = p.ISO3,
                                           Name = p.NAME
                                       }).FirstOrDefault(),
                            Name = c.NAME,
                            NameTranslated = (from l in data.TRANSLATIONS
                                              where l.FIELD_NAME == "NAME"
                                                     && l.TABLE_NAME == "CITY"
                                                     && l.FOREIGN_ID.Value == c.ID
                                                     && l.LANGUAGE.ISO2 == ISO2
                                              select l.VALUE).ToString()
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetCities(AppTourEntities data, string SearchTerm)
        private IQueryable<CityModel> GetCities(AppTourEntities data, string SearchTerm)
        {
            var query = from c in data.CITY
                        where c.NAME.Contains(SearchTerm)
                        orderby c.NAME
                        select new CityModel
                        {
                            Id = c.ID,
                            Country = (from p in data.COUNTRY
                                       where p.ID == c.COUNTRY.ID
                                       select new CountryModel
                                       {
                                           CountryCode = p.COUNTRY_CODE,
                                           Id = p.ID,
                                           CountryName = p.COUNTRY_NAME,
                                           ISO = p.ISO,
                                           ISO3 = p.ISO3,
                                           Name = p.NAME
                                       }).FirstOrDefault(),
                            Name = c.NAME
                        };
            return query;
        }
        #endregion

        #region + GetCities
        public IList<CityModel> GetCities()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCities(data).ToList();
            }
        }
        #endregion

        #region + GetCity(Guid id)
        public CityModel GetCity(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCities(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region + UpdateCity(CityModel City)
        public void UpdateCity(CityModel City)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                CITY current = data.CITY.Where(x => City.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.COUNTRY = data.COUNTRY.Where(x => x.ID == City.Country.Id).SingleOrDefault();
                        current.NAME = City.Name;

                        data.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region + InsertCity(CityModel City)
        public Guid InsertCity(CityModel City)
        {
            if (City == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {
                CITY _new = new CITY
                {
                    ID = (City.Id == null || City.Id == Guid.Empty ? Guid.NewGuid() : City.Id),
                    COUNTRY = data.COUNTRY.Where(x => x.ID == City.Country.Id).SingleOrDefault(),
                    NAME = City.Name,
                };

                data.CITY.AddObject(_new);
                data.SaveChanges();

                return _new.ID;
            }
        }
        #endregion

        #region + DeleteCity(CityModel City)
        public void DeleteCity(CityModel City)
        {
            if (City == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {

                CITY current = data.CITY.Where(x => x.ID == City.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
                        data.SaveChanges();
                    }
                    catch (UpdateException upx)
                    {
                        throw new UpdateException(upx.InnerException.Message);
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }

        }
        #endregion

        #region + IList<CityModel> GetCities(string SearchTerm)
        public IList<CityModel> GetCities(string SearchTerm)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCities(data, SearchTerm).ToList();
            }
        }
        #endregion
    }
}
