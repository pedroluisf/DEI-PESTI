using System;
using AppTour.Business.ServiceContracts.Classification;
using AppTour.Model.Models.Classification;
using AppTour.DataAccess.Repository.Classification;

namespace AppTour.Business.Services.Classification
{
    public class ClassificationService : IClassificationService
    {
        public void AddVote(ClassificationModel Classification)
        {
            new ClassificationRepository().AddNewVote(Classification);
        }
    }
}
