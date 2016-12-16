using System.ServiceModel;
using AppTour.Model.Models.Classification;

namespace AppTour.Business.ServiceContracts.Classification
{
    [ServiceContract]
    public interface IClassificationService
    {
        [OperationContract]
        void AddVote(ClassificationModel Classification);
    }
}
