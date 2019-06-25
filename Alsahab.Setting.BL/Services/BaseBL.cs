using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using Alsahab.Common;
using FluentValidation;
using Alsahab.Setting.Common.Validation;
using System.Threading;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Common.Utilities;
using Alsahab.Setting.Common.Exceptions;
using Alsahab.Setting.Common.Api;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data.Contracts;
using FluentValidation.Results;

namespace Alsahab.Setting.BL
{
    public class BaseBL<TEntity, Dto, FilterDto> : IBaseBL<TEntity, Dto, FilterDto> 
        where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
        where Dto : BaseDTO//class
        where FilterDto : Dto
        // where TEntity : class, IEntity//BaseEntity<TEntity, Dto>
        // where Dto : BaseDTO
        // where FilterDto : Dto
    {
        public ResponseStatus ResponseStatus { get; set; }
        public int? ResultCount { get; set; }
        private PagingInfoDTO PagingInfo { get; set; }
        public string ErrorMessage { get; set; }
        public IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        private CultureInfo Culture { get; set; }
        private readonly IBaseDL<TEntity, Dto, FilterDto> _BaseDL;// = new IBaseDL<BranchDTO, Branch>();
        // private readonly IBaseValidator<Dto> _BaseValidator;
        // public BranchBL()
        // {
        // }

        public BaseBL(IBaseDL<TEntity, Dto, FilterDto> baseDL)//, IBaseValidator<Dto> baseValidator)
        {
            _BaseDL = baseDL;
            // _BaseValidator = baseValidator;
            ResponseStatus = ResponseStatus.BusinessError;
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            // ValidatorOptions.LanguageManager = new FluentValidation.Resources.LanguageManager();
            ValidatorOptions.LanguageManager.Culture = Culture;
            _observers = new List<Observers.ObserverBase>();
            _observers.Add(new Observers.LogObserver());
        }
        readonly List<Observers.ObserverBase> _observers;
        private UserInfoDTO User { get; set; }
        public CascadeMode CascadeMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public long? TeamID { get; set; }
        protected void Notify<TObserverState>(TObserverState stateInfo) where TObserverState : Observers.ObserverStates.ObserverStateBase
        {
            stateInfo.User = User;
            foreach (var observer in _observers)
            {
                observer.Notify(stateInfo);
            }
        }
        // protected bool Validate<TValidator, TObject>(TObject data) where TValidator : AbstractValidator<TObject>
        // {
        //     //Set Custom Translation
        //     ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();    
        //     //Create Instance From Validator    
        //     var validator = Activator.CreateInstance(typeof(TValidator));
        //     //Set Culture To Translate
        //     ValidatorOptions.LanguageManager.Culture = Culture;
        //     var result = ((AbstractValidator<TObject>)validator).Validate(data);
        //     ValidationErrors = result.Errors;
        //     return result.IsValid;
        // }

        protected bool Validate<T, TObject>(TObject data)
            where T : AbstractValidator<TObject>
        {
            //Set Custom Translation
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            //Create Instance From Validator    
            var validator = Activator.CreateInstance(typeof(T), _BaseDL);
            //Set Culture To Translate
            ValidatorOptions.LanguageManager.Culture = Culture;
            var result = ((AbstractValidator<TObject>)validator).Validate(data);
            ValidationErrors = result.Errors;
            if (!result.IsValid)            
            {
                ErrorMessage = "Validation error in business layer";
                ResponseStatus = ResponseStatus.BusinessError;
            }
            return result.IsValid;
        }

        public virtual async Task<IList<Dto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _BaseDL.GetAllAsync(cancellationToken);
        }
        public virtual IList<Dto> GetAll()
        {
            return _BaseDL.GetAll();
        }
        public virtual async Task<IList<Dto>> GetAsync(FilterDto filter, CancellationToken cancellationToken)// = new CancellationToken())
        {
            return await _BaseDL.GetAsync(filter, cancellationToken);
        }

        public virtual async Task<Dto> UpdateAsync(Dto data, CancellationToken cancellationToken)
        {
            return await _BaseDL.UpdateAsync(data, cancellationToken);
        }

        public virtual async Task<Dto> DeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            return await _BaseDL.DeleteAsync(data, cancellationToken);
        }

        public virtual async Task<Dto> InsertAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));
            // if (!Validate<BaseValidator<Dto>, Dto>(data))
            //     throw new AppException(ApiResultStatusCode.LogicError, "Validation error");

            data.CreateDate = DateTime.Now;
            //TODO
            // data.Code = GenerateCode(data);

            var response = await _BaseDL.InsertAsync(data, cancellationToken);//.BranchInsert(data);

            //TODO
            // if (Response?.ID > 0)
            // {
            //     var resp = BranchGet(new BranchDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            //     Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
            //     {
            //         Branch = resp ?? Response,
            //         User = User,
            //     };
            //     Notify(state);
            //     if (resp != null)
            //         Response = resp;
            // }

            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += _BaseDL.ErrorMessage;
                return null;
            }

            return response;
        }


        public virtual Dto Insert(Dto data)
        {
            data.CreateDate = DateTime.Now;
            //TODO
            // data.Code = GenerateCode(data);

            return _BaseDL.Insert(data);
        }

        public virtual Task<IList<Dto>> InsertListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual IList<Dto> InsertList(IList<Dto> list)
        {
            throw new NotImplementedException();
        }

        public virtual Dto Update(Dto data)
        {
            Assert.NotNull(data, nameof(data));

            return _BaseDL.Update(data);
        }

        public virtual Task<IList<Dto>> UpdateListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual IList<Dto> UpdateList(IList<Dto> list)
        {
            throw new NotImplementedException();
        }

        public virtual Dto Delete(Dto data)
        {
            Assert.NotNull(data, nameof(data));

            return _BaseDL.Delete(data);
        }

        public virtual Task<IList<Dto>> DeleteListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual IList<Dto> DeleteList(IList<Dto> list)
        {
            return _BaseDL.DeleteList(list);
        }

        public virtual IList<Dto> Get(FilterDto filter)
        {
            return _BaseDL.Get(filter);
        }
    }

    public class BaseBL<TEntity, Dto> : BaseBL<TEntity, Dto, Dto>
        where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
        where Dto : BaseDTO//class
    // where TEntity : class, IEntity//BaseEntity<TEntity, Dto>
    // where Dto : BaseDTO
    {
        public BaseBL(IBaseDL<TEntity, Dto, Dto> baseDL):base(baseDL)
        {
        }
    }
}
