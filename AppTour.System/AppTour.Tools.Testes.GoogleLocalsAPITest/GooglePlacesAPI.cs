using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AppTour.Agents.Service.Core;
using AppTour.Agents.Service.ViewModel;
using AppTour.Tools.Testes.GoogleLocalsAPITest.Detail;

namespace AppTour.Tools.Testes.GoogleLocalsAPITest
{
    public class GooglePlacesAPI
    {
        #region - Atributos
        private const string API_KEY = "AIzaSyBt6m7-u2L0cqwihF9NnemBkPk9Q_g2DX0";
        IList<PointViewModel> points = new List<PointViewModel>();
        #endregion

        #region + Construtor
        public GooglePlacesAPI()
        { }
        #endregion

        #region + Start
        public void Start()
        {
            HttpWebRequest webRequest = WebRequest.Create(@"https://maps.googleapis.com/maps/api/place/search/json?location=41.24119,-8.61768&radius=7500&sensor=false&key=" + API_KEY) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Method = "GET";

            webRequest.BeginGetResponse(new AsyncCallback(RequestCompleted), webRequest);
        }
        #endregion

        #region - parseFiles(string resp)
        private void parseFiles(string resp)
        {
            var result = new GoogleLocalsJSon(resp);

            int total = result.Results.Count();

            for (int i = 0; i < total; i++)
            {
                if (result.Results[i].Name == result.Results[i].Vicinity)
                    continue;

                Console.WriteLine(result.Results[i].Name);
                string coord = result.Results[i].Geometry.Location.Lat.ToString() + ',' + result.Results[i].Geometry.Location.Lng.ToString();
                Console.WriteLine("Coordenada: " + coord);

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
                    Pictures = pictures
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
                                // Failed get Postal Code
                                // Get Next Item
                                continue;
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
                }
                response.Close();

            }
            new AgentOperationsService().InsertPoints(points);
        }
        #endregion

        #region - RequestCompleted(IAsyncResult result)
        private void RequestCompleted(IAsyncResult result)
        {
            var request = (HttpWebRequest)result.AsyncState;
            var response = (HttpWebResponse)request.EndGetResponse(result);
            using (var stream = response.GetResponseStream())
            {

                var r = new StreamReader(stream);
                var resp = r.ReadToEnd();
                parseFiles(resp);

            }

        }
        #endregion
    }
}
