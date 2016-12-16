using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AppTour.WebServices.WS
{
    [ServiceContract]
    public interface IAppTourWS
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "/Authentication", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream Authentication(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UsernameValidation", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream UsernameValidation(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/EmailValidation", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream EmailValidation(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Registration", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream Registration(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Search", ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream Search(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetAllThemes", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream GetAllThemes(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetAllTopics", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream GetAllTopics(Stream streamData);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetPoint", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Stream GetPoint(Stream streamData);
    }

}
