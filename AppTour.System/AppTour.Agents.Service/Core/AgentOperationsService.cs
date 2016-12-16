using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppTour.Agents.Service.ViewModel;
using AppTour.Agents.Service.ViewModel.JSon;
using AppTour.Business.Services.Agent;
using AppTour.Business.Services.City;
using AppTour.Business.Services.Country;
using AppTour.Business.Services.Point;
using AppTour.Business.Services.Topic;
using AppTour.Model.Models.Agent;
using AppTour.Model.Models.City;
using AppTour.Model.Models.Picture;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Topic;
using System.Collections.Concurrent;

namespace AppTour.Agents.Service.Core
{
    public class PointTemporary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Coordenate { get; set; }
    }

    public class AgentOperationsService
    {
        #region - Atributos
        private string ERRORMESSAGE = "Erro ao inserir o ponto {0}. Razão: {1}";

        #region + static MutablePointList
        public static ConcurrentBag<PointTemporary> MutablePointList { get; set; }
        #endregion

        #endregion

        #region + Construtor
        public AgentOperationsService()
        {
            IList<PointModel> AllPoints = new PointService().GetActivePoints();

            MutablePointList = new ConcurrentBag<PointTemporary>();

            AllPoints.AsParallel().ForAll(x =>
            {
                MutablePointList.Add(new PointTemporary
                {
                    Id = x.Id,
                    Coordenate = x.Coordenate,
                    Name = x.Name
                });
            });

        }
        #endregion

        #region + bool ExistsInList(PointViewModel point)
        private bool ExistsInList(PointViewModel point)
        {
            // TODO procurar por distancia de pontos com coordenadas (lat,long)   
            return MutablePointList.AsParallel().Where(x => x.Name.Contains(point.Name) || x.Coordenate == point.Coordenate).Count() > 0;
        }
        #endregion

        #region + InsertPoint(PointViewModel point)
        public void InsertPoint(PointViewModel point)
        {
            if (!ExistsInList(point))
            {
                string errormsg = string.Format(ERRORMESSAGE, point.Name, "Já existe o ponto na base de dados");
                new NotificationGateway().SendNotification(errormsg);
                return;
            }

            // Topics
            IList<TopicModel> topics = new List<TopicModel>();
            if (point.Topics != null && point.Topics.Count > 0)
            {
                int total = point.Topics.Count;
                for (int i = 0; i < total; i++)
                {
                    Guid TopicId = TopicMapper.Instance.GetTopicIdFromSentence(point.Topics[i].ToString());

                    if (TopicId != Guid.Empty)
                    {
                        TopicModel myTopic = new TopicService().GetTopic(TopicId);
                        topics.Add(myTopic);
                        break;
                    }

                }
            }

            // Importar pontos que tenham pelo menos um topic
            if (topics.Count == 0)
            {
                string errormsg = string.Format(ERRORMESSAGE, point.Name, "Não tem tópicos mapeados!");
                new NotificationGateway().SendNotification(errormsg);
                return;
            }

            PointModel actualPoint = new PointModel
            {
                Id = Guid.NewGuid(),
                Address = point.Address,
                Coordenate = point.Coordenate,
                PostalCode = (point.PostalCode == null ? string.Empty : point.PostalCode),
                Name = point.Name,
                PhoneNumber = (point.PhoneNumber == null ? string.Empty : point.PhoneNumber),
                IsActive = true,
                SourceURL = point.SourceURL,
                URL = point.URL
            };

            actualPoint.Topics = topics;

            string cityName = string.Empty;

            if (point.PostalCode != null && point.PostalCode != string.Empty)
            {
                string codPostalTreated = string.Empty;
                if (point.PostalCode.Count() == 4)
                    codPostalTreated = point.PostalCode + "000";
                else if (point.PostalCode.Count() != 7)
                {
                    string[] arr;
                    arr = point.PostalCode.Split('-');
                    codPostalTreated = arr[0] + arr[1];
                }
                else
                    codPostalTreated = point.PostalCode;

                cityName = GetLocalByPostalCode(codPostalTreated);

                if (cityName == null)
                {
                    codPostalTreated = codPostalTreated.Substring(0, 4) + "100";
                    cityName = GetLocalByPostalCode(codPostalTreated);
                    if (cityName == null)
                    {
                        new NotificationGateway().SendNotification("Erro ao inserir o ponto " + point.Name + " Razão: sem Cidade/Localidade assignada.");
                        point.IsActive = false;
                        return;
                    }

                }

            }

            cityName = (cityName == string.Empty ? point.City.Name : cityName);

            IList<CityModel> cities = new CityService().GetCities(cityName);
            CityModel myCity = new CityModel();
            if (cities.Count == 0)
            {
                myCity = new CityModel
                {
                    Id = Guid.NewGuid(),
                    Name = cityName,
                    Country = new CountryService().GetCountry(point.City.ISO)
                };

                Guid id = new CityService().InsertCity(myCity);
            }
            else
                myCity = cities.FirstOrDefault();

            actualPoint.City = myCity;

            // Pictures
            if (point.Pictures.Count > 0)
            {
                IList<PictureModel> pictures = new List<PictureModel>();

                int total = point.Pictures.Count;
                for (int i = 0; i < total; i++)
                {
                    pictures.Add(new PictureModel
                    {
                        Event = null,
                        Id = Guid.NewGuid(),
                        Picture_URL = point.Pictures[i].ToString(),
                        Point = actualPoint
                    });
                }

                actualPoint.Pictures = pictures;
            }
            NoticeService.Instance.Notify("A inserir o ponto " + actualPoint.Name + " na base de dados");
            Parallel.Invoke(() =>
            {
                Guid pointId = new PointService().InsertPoint(actualPoint);
                MutablePointList.Add(new PointTemporary
                {
                    Id = Guid.Empty,
                    Coordenate = actualPoint.Coordenate,
                    Name = actualPoint.Name
                });
            });
        }
        #endregion

        #region + InsertPoints(IList<PointViewModel> points)
        public void InsertPoints(IList<PointViewModel> points)
        {
            //points.ToList().ForEach(x => InsertPoint(x));
            Parallel.ForEach(points, currentPoint => InsertPoint(currentPoint));
        }
        #endregion

        #region + string GetLocalByPostalCode(string PostalCode7)
        private string GetLocalByPostalCode(string PostalCode7)
        {
            HttpWebRequest webRequest = WebRequest.Create(@"http://codigospostais.appspot.com/cp7?codigo=" + PostalCode7) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Method = "GET";

            WebResponse response = webRequest.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var r = new StreamReader(stream);
                var resposta = r.ReadToEnd();

                CityResolver codPostal = new CityResolver(resposta);
                return codPostal.Localidade;
            }
        }
        #endregion

        #region SaveLastReference
        public void SaveReference(string AdapterName, string LastReference)
        {
            if (AdapterName == null || AdapterName == string.Empty)
                return;

            AgentModel agent = new AgentService().GetAgent(AdapterName);

            if (agent != null)
            {
                agent.LastReference = LastReference;
                new AgentService().UpdateAgent(agent);
            }
        }
        #endregion
    }
}
