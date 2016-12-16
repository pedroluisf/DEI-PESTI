using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Country;

namespace AppTour.DataAccess.Repository.Country
{
    public sealed class CountryRepository
    {
        #region - IQueryable GetCountries
        private IQueryable<CountryModel> GetCountries(AppTourEntities data)
        {
            var query = from c in data.COUNTRY
                        orderby c.NAME
                        select new CountryModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            CountryCode = c.COUNTRY_CODE,
                            CountryName = c.COUNTRY_NAME,
                            ISO = c.ISO,
                            ISO3 = c.ISO3

                        };
            return query;
        }
        #endregion

        #region - CountryModel GetCountries(AppTourEntities data, string ISO)
        private CountryModel GetCountries(AppTourEntities data, string ISO)
        {
            var query = from c in data.COUNTRY
                        where c.ISO == ISO
                        select new CountryModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            CountryCode = c.COUNTRY_CODE,
                            CountryName = c.COUNTRY_NAME,
                            ISO = c.ISO,
                            ISO3 = c.ISO3

                        };
            return query.SingleOrDefault();
        }
        #endregion

        #region + GetCountries
        public IList<CountryModel> GetCountries()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCountries(data).ToList();
            }
        }
        #endregion

        #region + GetCountry(Guid id)
        public CountryModel GetCountry(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCountries(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region + UpdateCountry(CountryModel Country)
        public void UpdateCountry(CountryModel Country)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                COUNTRY current = data.COUNTRY.Where(x => Country.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.COUNTRY_CODE = Country.CountryCode;
                        current.NAME = Country.Name;
                        current.ISO = Country.ISO;
                        current.ISO3 = Country.ISO3;
                        current.COUNTRY_NAME = Country.CountryName;

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

        #region + InsertCountry(CountryModel Country)
        public Guid InsertCountry(CountryModel Country)
        {
            Guid id = Guid.NewGuid();
            if (Country == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {
                COUNTRY _new = new COUNTRY
                {
                    ID = id,
                    NAME = Country.Name,
                    COUNTRY_CODE = Country.CountryCode,
                    COUNTRY_NAME = Country.CountryName,
                    ISO = Country.ISO,
                    ISO3 = Country.ISO3
                };

                data.COUNTRY.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region + DeleteCountry(CountryModel Country)
        public void DeleteCountry(CountryModel Country)
        {
            if (Country == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                COUNTRY current = data.COUNTRY.Where(x => x.ID == Country.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
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

        #region + CountryModel GetCountry(string ISO)
        public CountryModel GetCountry(string ISO)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetCountries(data, ISO);
            }
        }
        #endregion

    }
}
