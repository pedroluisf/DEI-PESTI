using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AppTour.Agents.GoogleLocals.CoordenateResolver;
using AppTour.Agents.GoogleLocals.Detail;
using AppTour.Agents.GoogleLocals.Helper;
using AppTour.Agents.Service.Core;
using AppTour.Agents.Service.Interface;
using AppTour.Agents.Service.ViewModel;

namespace AppTour.Agents.GoogleLocals
{
    public class GoogleLocalsAdapter : IAgentAdapter
    {

        #region - Constantes
        private const string API_KEY = "AIzaSyBt6m7-u2L0cqwihF9NnemBkPk9Q_g2DX0";
        private const string FILENAME = "portocodpostal.csv";
        private const string MAX_DISTANCE_IN_METERS = "1000";
        private const string LANGUAGE = "pt-PT";
        private static string ERRORMESSAGE = "Erro ao inserir o ponto {0}. Razão: {1}";
        #endregion

        #region - Atributos
        IList<PointViewModel> points;
        IList<string> CodPostalList = new List<string>();
        private int MaxRequestPerDay;
        private int Rating;

        private int ActualRequests;
        private string LastReference;
        private string AdapterName;
        #endregion

        #region + Construtor
        public GoogleLocalsAdapter()
        {
        }
        #endregion

        #region + string Execute(int MaxRequestPerDay, int Rating, string LastReference)
        public string Execute(int MaxRequestPerDay, int Rating, string LastReference, string AdapterName)
        {
            this.MaxRequestPerDay = MaxRequestPerDay;
            this.Rating = Rating;
            this.LastReference = LastReference;
            this.AdapterName = AdapterName;
            ReadCSV();

            return Start(LastReference);
        }

        #endregion

        #region - void ReadCSV()
        private void ReadCSV()
        {
            string completeFileName = Directory.GetCurrentDirectory() + "\\adapters\\" + AdapterName + "\\" + FILENAME;

            using (StreamReader readFile = new StreamReader(completeFileName))
            {
                string line;
                while ((line = readFile.ReadLine()) != null)
                {
                    CodPostalList.Add(line);
                }
            }

        }
        #endregion

        #region - string Start(string LastReference)
        private string Start(string LastReference)
        {
            // Fazer os pedidos
            // Enquanto não ultrapassar o limite por dia
            new NotificationGateway().SendNotification("Inicio do Agente " + this.AdapterName);
            while (ActualRequests < MaxRequestPerDay)
            {
                if (LastReference == null)
                    LastReference = CodPostalList.FirstOrDefault();
                else
                {
                    int index = CodPostalList.IndexOf(LastReference);
                    if (index < CodPostalList.Count - 1)
                        LastReference = CodPostalList.ElementAt(index + 1);
                    else
                        LastReference = CodPostalList.ElementAt(0);
                }

                string coordenate = CoordenateFromPostalCode(LastReference);

                if (coordenate == null || coordenate == string.Empty)
                    return string.Empty;

                HttpWebRequest webRequest = WebRequest.Create(@"https://maps.googleapis.com/maps/api/place/search/json?location=" + coordenate + "&language=" + LANGUAGE + "&radius=" + MAX_DISTANCE_IN_METERS + "&sensor=false&key=" + API_KEY) as HttpWebRequest;
                webRequest.Timeout = 20000;
                webRequest.Method = "GET";

                WebResponse response = webRequest.GetResponse();
                new NotificationGateway().SendNotification("A processar a referencia: " + LastReference);
                using (var stream = response.GetResponseStream())
                {
                    var r = new StreamReader(stream);
                    var resp = r.ReadToEnd();
                    parseFiles(resp);
                }
                new NotificationGateway().SendNotification("Guardar ultima referencia: " + LastReference);
                new AgentOperationsService().SaveReference(this.AdapterName, LastReference);
            }
            return LastReference;
        }
        #endregion

        #region - string CoordenateFromPostalCode(string LastReference)
        private string CoordenateFromPostalCode(string LastReference)
        {
            if (LastReference == null || LastReference == string.Empty)
                return null;

            if (LastReference.Length != 4)
                LastReference = LastReference.Substring(0, 4);

            HttpWebRequest webRequest = WebRequest.Create(@"http://codigospostais.appspot.com/cp4?codigo=" + LastReference) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Method = "GET";

            WebResponse response = webRequest.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var r = new StreamReader(stream);
                var resposta = r.ReadToEnd();

                ResolveCoordenate coordenate = new ResolveCoordenate(resposta);
                string latitude = StringHelper.RemoveSpecialCharacters(coordenate.Latitude);
                return latitude + ',' + coordenate.Longitude;
            }

        }
        #endregion

        #region - parseFiles(string resp)
        private void parseFiles(string resp)
        {
            var result = new GoogleLocalsJSon(resp);

            if (result.Status == "OK")
            {
                if (points != null)
                    points.Clear();

                points = new List<PointViewModel>();
                ActualRequests++;
                int total = result.Results.Count();

                for (int i = 0; i < total; i++)
                {
                    if (result.Results[i].Name == result.Results[i].Vicinity)
                    {
                        string errormsg = string.Format(ERRORMESSAGE, result.Results[i].Vicinity, "Nome igual à morada");
                        new NotificationGateway().SendNotification(errormsg);
                        continue;
                    }
                    string coord = result.Results[i].Geometry.Location.Lat.ToString() + ',' + result.Results[i].Geometry.Location.Lng.ToString();

                    IList<string> topics = new List<string>();

                    if (result.Results[i].Types.Count() > 0)
                        for (int j = 0; j < result.Results[i].Types.Count(); j++)
                            topics.Add(result.Results[i].Types[j].ToString());

                    IList<string> pictures = new List<string>();

                    if (result.Results[i].Icon != null && result.Results[i].Icon != string.Empty)
                        pictures.Add(result.Results[i].Icon);

                    points.Add(new PointViewModel
                    {
                        Reference = result.Results[i].Reference,
                        Name = result.Results[i].Name,
                        Coordenate = coord,
                        Address = result.Results[i].Vicinity,
                        Topics = topics,
                        Pictures = pictures,
                        IsActive = true
                    });

                }

                // TODO: make Parallel For
                foreach (PointViewModel item in points)
                {

                    HttpWebRequest webRequest = WebRequest.Create(@"https://maps.googleapis.com/maps/api/place/details/json?reference=" + item.Reference + "&sensor=true&key=" + API_KEY) as HttpWebRequest;
                    webRequest.Timeout = 20000;
                    webRequest.Method = "GET";

                    WebResponse response = webRequest.GetResponse();

                    using (var stream = response.GetResponseStream())
                    {
                        var r = new StreamReader(stream);
                        var resposta = r.ReadToEnd();

                        var newResult = new GoogleLocalsDetailAPI(resposta);

                        if (newResult.Status == "OK")
                        {
                            // Increment Requests for API
                            ActualRequests++;

                            // Phone Number
                            if (newResult.Result.FormattedPhoneNumber != null && newResult.Result.FormattedPhoneNumber != string.Empty)
                                item.PhoneNumber = newResult.Result.FormattedPhoneNumber;
                            else
                                if (newResult.Result.InternationalPhoneNumber != null)
                                    item.PhoneNumber = newResult.Result.InternationalPhoneNumber;

                            // URL
                            if (newResult.Result.Url != null || newResult.Result.Url != string.Empty)
                            {
                                item.URL = newResult.Result.Website;
                                item.SourceURL = @"https://maps.googleapis.com/maps/api/place/details/json?reference=" + item.Reference + "&sensor=true";
                            }

                            // Postal Code
                            string postalCode = string.Empty;

                            // Get Postal Code From AddressComponents in Google API
                            foreach (var value in newResult.Result.AddressComponents)
                            {
                                int totalItem = value.Types.Count();
                                for (int i = 0; i < totalItem; i++)
                                {
                                    if (value.Types[i] == "postal_code")
                                    {
                                        postalCode = newResult.Result.AddressComponents.SingleOrDefault(x => x.Types.Contains("postal_code")).LongName;
                                        break;
                                    }
                                }
                                if (postalCode != string.Empty)
                                    break;
                            }

                            // Didnt exists .. try to search on Formatted address with pattern ####-###
                            if (postalCode == string.Empty)
                            {
                                // Verify if exists Address

                                if (newResult.Result.FormattedAddress == null || item.Address == null)
                                {

                                    string errormsg = string.Format(ERRORMESSAGE, item.Name, "Sem Morada");
                                    new NotificationGateway().SendNotification(errormsg);
                                    item.IsActive = false;
                                    continue;
                                }

                                string pattern = "[0-9]{4}-[0-9]{3}";

                                Match match = Regex.Match(newResult.Result.FormattedAddress, pattern, RegexOptions.IgnoreCase);

                                if (match.Success)
                                    postalCode = match.Value;
                                else
                                {
                                    match = Regex.Match(newResult.Result.Vicinity, pattern, RegexOptions.IgnoreCase);
                                    if (match.Success)
                                        postalCode = match.Value;
                                }
                            }
                            // Didnt Exists - try last search with #### in adress name
                            if (postalCode == string.Empty)
                            {
                                string[] splitMorada = item.Address.Split(',');
                                string getLastString = splitMorada[splitMorada.Length - 1];

                                string pattern = "[0-9]{4}";

                                Match match = Regex.Match(getLastString, pattern, RegexOptions.IgnoreCase);

                                if (match.Success)
                                    postalCode = match.Value;
                                else
                                {
                                    // Failed get Postal Code
                                    // Get Next Item
                                    string errormsg = string.Format(ERRORMESSAGE, item.Name, "Sem código postal");
                                    new NotificationGateway().SendNotification(errormsg);
                                    item.IsActive = false;
                                    continue;
                                }
                            }

                            item.PostalCode = postalCode;
                            // City

                            // TODO: Get in locality @ address_components
                            item.City = new CityViewModel
                            {
                                Country = newResult.Result.AddressComponents.SingleOrDefault(x => x.Types.Contains("country")).LongName,
                                ISO = newResult.Result.AddressComponents.SingleOrDefault(x => x.Types.Contains("country")).ShortName,
                                Name = newResult.Result.Vicinity
                            };
                        }
                        else if (newResult.Status == "OVER_QUERY_LIMIT")
                            ActualRequests = MaxRequestPerDay;
                        else
                            ActualRequests++;
                    }
                    response.Close();

                }
                new NotificationGateway().SendNotification("Tentar inserir " + points.Where(x => x.IsActive).Count() + " pontos");
                new AgentOperationsService().InsertPoints(points.Where(x => x.IsActive).ToList());
            }
            else if (result.Status == "OVER_QUERY_LIMIT")
            {
                ActualRequests = MaxRequestPerDay;
            }
            else
                ActualRequests++;
        }
        #endregion

    }
}
