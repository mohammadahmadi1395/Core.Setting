using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using System.Globalization;
using Gostar.Common.Validation;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public class BaseBL
    {
        public Gostar.Common.ResponseStatus ResponseStatus { get; set; }
        public int? ResultCount { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
        public string ErrorMessage { get; set; }
        public IList<Gostar.Common.Validation.Results.ValidationFailure> ValidationErrors { get; set; }
        public CultureInfo Culture { get; set; }
        public BaseBL()
        {
            ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
            ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();
            ValidatorOptions.LanguageManager.Culture = Culture;
            _observers = new List<Observers.ObserverBase>();
            _observers.Add(new Observers.LogObserver());
        }
        readonly List<Observers.ObserverBase> _observers;
        public UserInfoDTO User { get; set; }

        //public long? TeamID { get; set; }
        protected void Notify<TObserverState>(TObserverState stateInfo) where TObserverState : Observers.ObserverStates.ObserverStateBase
        {
            stateInfo.User = User;
            foreach (var observer in _observers)
            {
                observer.Notify(stateInfo);
            }
        }
        protected bool Validate<TValidator, TObject>(TObject data) where TValidator : AbstractValidator<TObject>
        {
            //Set Custom Translation
            ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();    
            //Create Instance From Validator    
            var validator = Activator.CreateInstance(typeof(TValidator));
            //Set Culture To Translate
            ValidatorOptions.LanguageManager.Culture = Culture;
            var result = ((AbstractValidator<TObject>)validator).Validate(data);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }
    }
}
