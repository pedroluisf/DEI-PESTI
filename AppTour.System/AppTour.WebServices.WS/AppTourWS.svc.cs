using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using AppTour.Business.Services.SearchProfile;
using AppTour.Business.Services.Service;
using AppTour.Business.Services.Theme;
using AppTour.Business.Services.Topic;
using AppTour.Business.Services.User;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Search;
using AppTour.Model.Models.Theme;
using AppTour.Model.Models.Topic;
using AppTour.Model.Models.User;
using AppTour.WebServices.WS.Helpers;
using AppTour.WebServices.WS.ViewModel;
using System.Collections.Specialized;
using AppTour.Business.Services.Point;

namespace AppTour.WebServices.WS
{
    public class AppTourWS : IAppTourWS
    {
        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        #region + POST Authentication(string username, string password)
        public Stream Authentication(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string username = string.Empty;
                string password = string.Empty;

                string[] array = res.Split('&');
                int total = array.Length;

                for (int i = 0; i < total; i++)
                {
                    string[] internalArray = array[i].Split('=');
                    if (internalArray[0] == "username")
                        username = internalArray[1];
                    else
                        password = internalArray[1];
                }


                if (username != string.Empty && password != string.Empty)
                {
                    UserModel model = new UserService().Auth(username, password, false);

                    var s = new JavaScriptSerializer();
                    string jsonClient = s.Serialize(model);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                    return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
                }

                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
        }
        #endregion

        #region + POST UsernameValidation(string username)
        public Stream UsernameValidation(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string username = string.Empty;
                string[] internalArray = res.Split('=');
                if (internalArray[0] == "username")
                    username = internalArray[1];

                if (username != string.Empty)
                {
                    bool valid = new UserService().UsernameValidation(username);

                    var s = new JavaScriptSerializer();
                    string jsonClient = s.Serialize(valid);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                    return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
                }

                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }

        }
        #endregion

        #region + POST EmailValidation(string email)
        public Stream EmailValidation(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string email = string.Empty;
                string[] internalArray = res.Split('=');
                if (internalArray[0] == "email")
                    email = internalArray[1];

                if (email != string.Empty)
                {
                    bool valid = new UserService().EmailValidation(email);

                    var s = new JavaScriptSerializer();
                    string jsonClient = s.Serialize(valid);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                    return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
                }

                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
        }
        #endregion

        #region + [POST] Registration(Stream streamData)
        public Stream Registration(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string username = string.Empty;
                string password = string.Empty;
                string email = string.Empty;
                string realName = string.Empty;

                string[] array = res.Split('&');
                int total = array.Length;

                for (int i = 0; i < total; i++)
                {
                    string[] internalArray = array[i].Split('=');
                    if (internalArray[0] == "username")
                        username = internalArray[1];
                    else if (internalArray[0] == "password")
                        password = internalArray[1];
                    else if (internalArray[0] == "email")
                        email = internalArray[1];
                    else if (internalArray[0] == "realName")
                        realName = internalArray[1];
                }


                if (username != string.Empty && password != string.Empty && email != string.Empty && realName != string.Empty)
                {
                    UserModel model = new UserService().Register(username, password, realName, email, true);

                    var s = new JavaScriptSerializer();
                    string jsonClient = s.Serialize(model);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                    return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
                }

                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
        }
        #endregion

        #region + Stream Search(Stream streamData)
        public Stream Search(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string SearchTerm = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                NameValueCollection returnValues = ParseStrings.ParseFromStream(SearchTerm);

                var s = new JavaScriptSerializer();

                string ISO2 = returnValues.Get("ISO2");
                string UserId = returnValues.Get("UserId");
                string SearchProfileId = returnValues.Get("SearchProfileId");
                string Coordenates = returnValues.Get("Location");

                if (!isGuid.IsMatch(UserId) || !isGuid.IsMatch(SearchProfileId))
                {
                    string error = s.Serialize("Error IDs");
                    return new MemoryStream(Encoding.UTF8.GetBytes(error));
                }

                SearchModel model = new SearchModel
                {
                    Coordenate = Coordenates,
                    SearchProfile = new SearchProfileService().GetSearchProfile(new Guid(SearchProfileId), new Guid(UserId)),
                    Terms = null
                };

                if (model.SearchProfile == null)
                    model.SearchProfile = new SearchProfileService().GetDefaultSearchProfile(new Guid(UserId));


                IList<PointModel> pointsModel = new SearchService().Search(model);
                IList<PointSimpleModel> points = new List<PointSimpleModel>();

                pointsModel.ToList().ForEach(x => points.Add(new PointSimpleModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    Coordenate = x.Coordenate,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    PostalCode = x.PostalCode,
                    SourceURL = x.SourceURL,
                    TopicId = x.Topics.FirstOrDefault().Id.ToString(),
                    URL = x.URL
                }));

                string jsonClient = s.Serialize(points);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
            }
            catch (Exception ex)
            {
                JavaScriptSerializer s = new JavaScriptSerializer();
                string error = s.Serialize((ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                return new MemoryStream(Encoding.UTF8.GetBytes(error));
            }

        }
        #endregion

        #region + POST GetAllThemes(string language)
        public Stream GetAllThemes(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string ISO2 = string.Empty;

                string[] internalArray = res.Split('=');
                if (internalArray[0] == "ISO2")
                    ISO2 = internalArray[1];

                List<ThemeModel> themes = new ThemeService().GetActiveThemes().ToList<ThemeModel>();

                var s = new JavaScriptSerializer();
                string jsonClient = s.Serialize(themes);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
        }
        #endregion

        #region + POST GetAllTopics(string language)
        public Stream GetAllTopics(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                string ISO2 = string.Empty;

                string[] internalArray = res.Split('=');
                if (internalArray[0] == "ISO2")
                    ISO2 = internalArray[1];

                List<TopicModel> topics = new TopicService().GetActiveTopics().ToList<TopicModel>();

                var s = new JavaScriptSerializer();
                string jsonClient = s.Serialize(topics);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }
        }
        #endregion

        #region + POST GetPoint(Stream streamData);
        public Stream GetPoint(Stream streamData)
        {
            try
            {
                StreamReader reader = new StreamReader(streamData);

                string res = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                var s = new JavaScriptSerializer();

                string IdPoint = res.Split('=').GetValue(1).ToString();
                if (!isGuid.IsMatch(IdPoint))
                {
                    string error = s.Serialize("Error IDs");
                    return new MemoryStream(Encoding.UTF8.GetBytes(error));
                }

                PointModel point = new PointService().GetActivePoint(new Guid(IdPoint));

                string jsonClient = s.Serialize(point);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

                return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
            }
            catch (Exception)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(""));
            }

        }
        #endregion
    }
}
