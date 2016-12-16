using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using FluentValidation.Results;

namespace AppTour.Model.Models
{
    [DataContract]
    public abstract class BaseModel : IDataErrorInfo
    {
        #region + bool IsValid
        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }
        #endregion

        #region + virtual ValidationResult SelfValidate()
        public virtual ValidationResult SelfValidate()
        {
            return new ValidationResult();
        }
        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get { return ValidationHelper.GetError(SelfValidate()); }
        }

        public string this[string columnName]
        {
            get
            {
                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<FluentValidation.Results.ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
            }
        }
        #endregion
    }
}
