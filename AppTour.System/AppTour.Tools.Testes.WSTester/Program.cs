using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AppTour.Tools.Testes.WSTester.AppTourWebService;

namespace AppTour.Tools.Testes.WSTester
{
    class Program
    {
        static void Main(string[] args)
        {
            AppTourWSClient authService = new AppTourWebService.AppTourWSClient();
            string cookieHeader = "";

            using (OperationContextScope scope = new OperationContextScope(authService.InnerChannel))
            {
                HttpRequestMessageProperty requestProperty = new HttpRequestMessageProperty();
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestProperty;

                UserModel isGood = authService.Authentication("jonas", "jona");

                MessageProperties properties = OperationContext.Current.IncomingMessageProperties;
                HttpResponseMessageProperty responseProperty = (HttpResponseMessageProperty)properties[HttpResponseMessageProperty.Name];
                cookieHeader = responseProperty.Headers[HttpResponseHeader.SetCookie];
            }
        }
    }
}
