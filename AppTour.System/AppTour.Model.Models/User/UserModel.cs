using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;

namespace AppTour.Model.Models.User
{
    [DataContract]
    public class UserModel : BaseModel
    {
        public UserModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string NewPassword { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string RealName { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<UserValidator, UserModel>(this);
        }
    }
}
