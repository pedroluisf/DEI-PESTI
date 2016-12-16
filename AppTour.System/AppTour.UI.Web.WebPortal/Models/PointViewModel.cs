using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppTour.Model.Models.City;
using AppTour.Model.Models.Picture;
using AppTour.Model.Models.PointsAttributes;

namespace AppTour.UI.Web.WebPortal.Models
{
    public class PointViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public CityModel City { get; set; }

        [Required]
        public string Coordenate { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Url)]
        public string URL { get; set; }

        [DataType(DataType.Url)]
        public string SourceURL { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<PointAttributeModel> Attributes { get; set; }

        public IEnumerable<PictureModel> Pictures { get; set; }

        [Required]
        public string[] SelectedTopicId { get; set; }

        public IEnumerable<TopicViewModel> Topics { get; set; }
    }

    public class TopicViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}