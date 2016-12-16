using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppTour.DataAccess.Entity;
using AppTour.DataAccess.Repository.Comment;
using AppTour.DataAccess.Repository.Picture;
using AppTour.DataAccess.Repository.PointAttribute;
using AppTour.DataAccess.Repository.Topic;
using AppTour.Model.Models.City;
using AppTour.Model.Models.Country;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.PointsAttributes;
using AppTour.Model.Models.Topic;
using AppTour.Model.Models.Search;
using System.Data.Objects;
using System.Collections.Concurrent;
using AppTour.DataAccess.Repository.Classification;

namespace AppTour.DataAccess.Repository.Point
{
    public sealed class PointRepository
    {

        #region - IQueryable GetPoints(AppTourEntities data)
        private IQueryable<PointModel> GetPoints(AppTourEntities data)
        {

            var query = from c in data.POINT
                        orderby c.NAME
                        select new PointModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            Address = c.ADDRESS,
                            PostalCode = c.POSTAL_CODE,
                            City = new CityModel
                            {
                                Id = c.CITY.ID,
                                Name = c.CITY.NAME,
                                Country = new CountryModel
                                {
                                    Id = c.CITY.COUNTRY.ID,
                                    CountryCode = c.CITY.COUNTRY.COUNTRY_CODE,
                                    CountryName = c.CITY.COUNTRY.COUNTRY_NAME,
                                    ISO = c.CITY.COUNTRY.ISO,
                                    ISO3 = c.CITY.COUNTRY.ISO3,
                                    Name = c.CITY.COUNTRY.NAME
                                }
                            },
                            Coordenate = c.COORDENATE,
                            PhoneNumber = c.PHONE_NUMBER,
                            URL = c.URL,
                            SourceURL = c.SOURCE_URL,
                            IsActive = c.IS_ACTIVE
                        };
            return query;
        }
        #endregion

        #region - IQueryable<PointModel> GetActivePoints(AppTourEntities data, TopicModel Topic)
        private IQueryable<PointModel> GetActivePoints(AppTourEntities data, TopicModel Topic)
        {

            var query = from c in data.POINT
                        join t in data.POINT_TOPIC on c.ID equals t.ID_POINT
                        where t.TOPIC.NAME == Topic.Name && c.IS_ACTIVE
                        select new PointModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            Address = c.ADDRESS,
                            PostalCode = c.POSTAL_CODE,
                            City = new CityModel
                            {
                                Id = c.CITY.ID,
                                Name = c.CITY.NAME,
                                Country = new CountryModel
                                {
                                    Id = c.CITY.COUNTRY.ID,
                                    CountryCode = c.CITY.COUNTRY.COUNTRY_CODE,
                                    CountryName = c.CITY.COUNTRY.COUNTRY_NAME,
                                    ISO = c.CITY.COUNTRY.ISO,
                                    ISO3 = c.CITY.COUNTRY.ISO3,
                                    Name = c.CITY.COUNTRY.NAME
                                }
                            },
                            Coordenate = c.COORDENATE,
                            PhoneNumber = c.PHONE_NUMBER,
                            URL = c.URL,
                            SourceURL = c.SOURCE_URL,
                            IsActive = c.IS_ACTIVE
                        };
            return query;
        }
        #endregion

        #region - int GetTotalPoints(AppTourEntities data)
        private int GetTotalPoints(AppTourEntities data)
        {
            return data.POINT.Count();
        }
        #endregion

        #region + int GetTotalPoints()
        public int GetTotalPoints()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return GetTotalPoints(data);
            }
        }
        #endregion

        #region + GetPoints()
        public IList<PointModel> GetPoints()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                var points = this.GetPoints(data).ToList();
                points.ToList().ForEach(x =>
                {
                    x.Topics = new TopicRepository().GetTopicsForPoint(x.Id);
                    x.Attributes = new PointAttributeRepository().GetAttributeForPoints(x.Id);
                });
                return points;
            }
        }
        #endregion

        #region + GetPoints(int Skip, int Total)
        public IList<PointModel> GetPoints(int Skip, int Total)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return GetPoints(data).Skip(Skip).Take(Total).ToList();
            }
        }
        #endregion

        #region + GetPoint(Guid Id)
        public PointModel GetPoint(Guid Id)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                PointModel point = this.GetPoints(data).SingleOrDefault(x => x.Id == Id);

                Parallel.Invoke(() =>
                {
                    point.Topics = new TopicRepository().GetTopicsForPoint(point.Id);
                    point.Attributes = new PointAttributeRepository().GetAttributeForPoints(point.Id);
                    point.Pictures = new PictureRepository().GetPicturesFromPoint(point.Id);
                    point.Comments = new CommentRepository().GetCommentsForPoint(point.Id);
                    point.Classifications = new ClassificationRepository().GetClassificationForPoint(point);
                });

                return point;
            }
        }
        #endregion

        #region + GetPointForSearch(Guid Id)
        public PointModel GetPointForSearch(Guid Id)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                PointModel point = this.GetPoints(data).SingleOrDefault(x => x.Id == Id);
                point.Topics = new TopicRepository().GetTopicsForPoint(point.Id);

                return point;
            }
        }
        #endregion

        #region + GetActivePoint(Guid Id)
        public PointModel GetActivePoint(Guid Id)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                PointModel point = this.GetPoints(data).SingleOrDefault(x => x.Id == Id && x.IsActive);

                Parallel.Invoke(() =>
                {
                    point.Topics = new TopicRepository().GetTopicsForPoint(point.Id);
                    point.Attributes = new PointAttributeRepository().GetAttributeForPoints(point.Id);
                    point.Comments = new CommentRepository().GetCommentsForPoint(point.Id);
                    point.Classifications = new ClassificationRepository().GetClassificationForPoint(point);
                    point.Pictures = new PictureRepository().GetPicturesFromPoint(point.Id);
                });

                return point;
            }
        }
        #endregion

        #region + GetActivePoints()
        public IList<PointModel> GetActivePoints()
        {
            using (AppTourEntities data = new AppTourEntities())
            {

                var points = this.GetPoints(data).Where(f => f.IsActive).ToList();

                Parallel.ForEach(points, x =>
                {
                    x.Topics = new TopicRepository().GetTopicsForPoint(x.Id);
                    x.Attributes = new PointAttributeRepository().GetAttributeForPoints(x.Id);
                });

                return points.ToList();

            }
        }
        #endregion

        #region + GetActivePoints(TopicModel Topic)
        public IList<PointModel> GetActivePoints(TopicModel Topic)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                var points = this.GetActivePoints(data, Topic).ToList();

                Parallel.ForEach(points, x =>
                {
                    x.Topics = new TopicRepository().GetTopicsForPoint(x.Id);
                    x.Attributes = new PointAttributeRepository().GetAttributeForPoints(x.Id);
                });

                return points.ToList();

            }
        }
        #endregion

        /* POC
        #region + GetActivePoints(Guid Topic)
        public IList<PointModel> GetActivePoints(Guid TopicId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                var points = this.GetPoints(data).ToList();

                Parallel.ForEach(points, x =>
                {
                    x.Topics = new TopicRepository().GetTopicsForPoint(x.Id);
                    x.Attributes = new PointAttributeRepository().GetAttributeForPoints(x.Id);
                });

                return points.ToList().Where(x => x.Topics.All(y => y.Id.Equals(TopicId))).ToList();
            }
        }
        #endregion
        */

        #region + UpdatePoint(PointModel point)
        public void UpdatePoint(PointModel point)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                POINT current = data.POINT.Include("POINT_TOPIC").Single(x => point.Id == x.ID);

                if (current != null)
                {
                    try
                    {
                        current.NAME = point.Name;
                        current.ADDRESS = point.Address;
                        current.POSTAL_CODE = point.PostalCode;
                        current.CITY = data.CITY.Where(x => x.ID == point.City.Id).FirstOrDefault();
                        current.COORDENATE = point.Coordenate;
                        current.PHONE_NUMBER = point.PhoneNumber;
                        current.URL = point.URL;
                        current.SOURCE_URL = point.SourceURL;
                        current.IS_ACTIVE = point.IsActive;

                        if (point.Topics != null && point.Topics.Count() > 0)
                        {
                            current.POINT_TOPIC.ToList().ForEach(x =>
                            {
                                data.POINT_TOPIC.DeleteObject(x);
                            });
                            // Adicionar Topicos
                            foreach (TopicModel item in point.Topics)
                            {
                                POINT_TOPIC pt = new POINT_TOPIC
                                {
                                    ID = Guid.NewGuid(),
                                    TOPIC = data.TOPIC.Single(x => x.ID.Equals(item.Id)),
                                    POINT = current
                                };
                                current.POINT_TOPIC.Add(pt);
                            }
                        }


                        // Atributos
                        if (point.Attributes != null && point.Attributes.Count > 0)
                            foreach (PointAttributeModel attribute in point.Attributes)
                            {
                                POINTS_ATTRIBUTES attr;
                                if (attribute.Id != Guid.Empty)
                                {
                                    attr = current.POINTS_ATTRIBUTES.Where(x => x.ID == attribute.Id).Single();
                                    attr.POINT = data.POINT.Single(x => x.ID == attribute.Point.Id);
                                    attr.KEYPAIR = attribute.KeyPair;
                                    attr.VALUE_BOOL = attribute.Value_bool;
                                    attr.VALUE_STRING = attribute.Value_string;
                                    attr.VALUE_NUMBER = Decimal.Parse(attribute.Value_number.ToString());
                                    attr.VALUE_DATE = attribute.Value_Date;
                                    attr.VALUE_TYPE = attribute.Value_Type;
                                    attr.IS_ACTIVE = attribute.IsActive;
                                }
                                else
                                {
                                    attr = new POINTS_ATTRIBUTES
                                    {
                                        ID = Guid.NewGuid(),
                                        POINT = data.POINT.Single(x => x.ID == attribute.Point.Id),
                                        KEYPAIR = attribute.KeyPair,
                                        VALUE_BOOL = attribute.Value_bool,
                                        VALUE_STRING = attribute.Value_string,
                                        VALUE_NUMBER = Decimal.Parse(attribute.Value_number.ToString()),
                                        VALUE_DATE = attribute.Value_Date,
                                        VALUE_TYPE = attribute.Value_Type,
                                        IS_ACTIVE = attribute.IsActive
                                    };

                                }
                                current.POINTS_ATTRIBUTES.Add(attr);

                            }
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

        #region + InsertPoint(PointModel point)
        public Guid InsertPoint(PointModel point)
        {
            if (point == null)
                throw new ArgumentNullException("point");


            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    POINT novo = new POINT
                    {
                        ID = point.Id == null || point.Id == Guid.Empty ? Guid.NewGuid() : point.Id,
                        NAME = point.Name,
                        ADDRESS = point.Address,
                        POSTAL_CODE = point.PostalCode,
                        CITY = data.CITY.FirstOrDefault(x => x.ID == point.City.Id),
                        COORDENATE = point.Coordenate,
                        PHONE_NUMBER = point.PhoneNumber,
                        URL = point.URL,
                        SOURCE_URL = point.SourceURL,
                        IS_ACTIVE = point.IsActive
                    };

                    if (point.Attributes != null && point.Attributes.Count() > 0)
                        // Adicionar Atributos
                        point.Attributes.ToList().ForEach(x => novo.POINTS_ATTRIBUTES.Add(new POINTS_ATTRIBUTES
                        {
                            ID = x.Id == null || x.Id == Guid.Empty ? Guid.NewGuid() : x.Id,
                            POINT = novo,
                            KEYPAIR = x.KeyPair,
                            VALUE_BOOL = x.Value_bool,
                            VALUE_STRING = x.Value_string,
                            VALUE_NUMBER = Decimal.Parse(x.Value_number.ToString()),
                            VALUE_DATE = x.Value_Date,
                            VALUE_TYPE = x.Value_Type,
                            IS_ACTIVE = x.IsActive
                        }));

                    if (point.Topics != null && point.Topics.Count() > 0)
                    {
                        // Adicionar Topicos
                        foreach (var item in point.Topics)
                        {
                            novo.POINT_TOPIC.Add(new POINT_TOPIC
                            {
                                ID = Guid.NewGuid(),
                                TOPIC = data.TOPIC.Single(x => x.ID.Equals(item.Id)),
                                POINT = novo
                            });

                        }
                    }
                    if (point.Pictures != null && point.Pictures.Count > 0)
                    {
                        foreach (var item in point.Pictures)
                        {
                            novo.PICTURE.Add(new PICTURE
                            {
                                ID = (item.Id == null || item.Id == Guid.Empty ? Guid.NewGuid() : item.Id),
                                EVENT = (item.Event == null ? null : data.EVENT.Where(x => x.ID == item.Event.Id).SingleOrDefault()),
                                POINT = (item.Point == null ? null : data.POINT.Where(x => x.ID == item.Point.Id).SingleOrDefault()),
                                PIC_URL = item.Picture_URL
                            });
                        }
                    }

                    // Guardar tudo

                    data.POINT.AddObject(novo);
                    data.SaveChanges();

                    return novo.ID;
                }
                catch (UpdateException upd)
                {
                    throw new UpdateException(upd.InnerException.Message);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new ApplicationException(e.InnerException.Message);

                    throw;
                }
            }
        }
        #endregion

        #region + DeactivatePoint(PointModel point)
        public bool DesactivatePoint(PointModel point)
        {
            if (point == null)
                throw new ArgumentNullException("point");

            using (AppTourEntities data = new AppTourEntities())
            {
                POINT actual = data.POINT.Single(x => x.ID == point.Id);

                if (actual != null)
                {
                    actual.IS_ACTIVE = false;
                    data.SaveChanges();

                    return true;
                }
                return false;
            }
        }
        #endregion

        #region + IList<PointModel> GetFromSearch(SearchModel searchModel)
        public IList<PointModel> GetFromSearch(SearchModel searchModel)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                Guid searchProfileId = searchModel.SearchProfile != null ? searchModel.SearchProfile.Id : Guid.Empty;
                ObjectResult<Guid?> pointIds = data.Search(searchProfileId, searchModel.Terms, searchModel.Coordenate);

                if (pointIds != null)
                {
                    ConcurrentBag<PointModel> points = new ConcurrentBag<PointModel>();
                    pointIds.AsParallel().ForAll(x =>
                    {
                        points.Add(GetPointForSearch(x.Value));
                    });

                    return points.ToList();
                }
                return null;
            }

        }
        #endregion
    }
}
