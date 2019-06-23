using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        where TEntity : class, IEntity//BaseEntity<TEntity, Dto>
        where Dto : BaseDTO
        where FilterDto : Dto
    {
        public ResponseStatus ResponseStatus { get; set; }
        public int? ResultCount { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
        public string ErrorMessage { get; set; }
        public IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        public CultureInfo Culture { get; set; }
        private readonly IBaseDL<TEntity, Dto, FilterDto> _BaseDL;// = new IBaseDL<BranchDTO, Branch>();
        // public BranchBL()
        // {
        // }

        public BaseBL(IBaseDL<TEntity, Dto, FilterDto> baseDL)
        {
            _BaseDL = baseDL;
            ResponseStatus = ResponseStatus.BusinessError;
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            // ValidatorOptions.LanguageManager = new FluentValidation.Resources.LanguageManager();
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
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            //Create Instance From Validator    
            var validator = Activator.CreateInstance(typeof(TValidator));
            //Set Culture To Translate
            ValidatorOptions.LanguageManager.Culture = Culture;
            var result = ((AbstractValidator<TObject>)validator).Validate(data);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }

        public virtual async Task<IList<Dto>> GetAsync(FilterDto filter, CancellationToken cancellationToken = new CancellationToken())
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
            if (!Validate<AbstractValidator<Dto>, Dto>(data))
                throw new AppException(ApiResultStatusCode.LogicError, "Validation error");

            data.CreateDate = DateTime.Now;
            //TODO
            // data.Code = GenerateCode(data);

            var response = await _BaseDL.AddAsync(data, cancellationToken);//.BranchInsert(data);

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
            throw new NotImplementedException();
        }

        public virtual Task<List<Dto>> InsertListAsync(List<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual List<Dto> InsertList(List<Dto> list)
        {
            throw new NotImplementedException();
        }

        public virtual Dto Update(Dto data)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<Dto>> UpdateListAsync(List<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual List<Dto> UpdateList(List<Dto> list)
        {
            throw new NotImplementedException();
        }

        public virtual Dto Delete(Dto data)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<Dto>> DeleteListAsync(List<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual List<Dto> DeleteList(List<Dto> list)
        {
            throw new NotImplementedException();
        }

        public virtual List<Dto> Get(FilterDto filter)
        {
            throw new NotImplementedException();
        }

    }

    public class BaseBL<TEntity, Dto> : BaseBL<TEntity, Dto, Dto>
    where TEntity : class, IEntity//BaseEntity<TEntity, Dto>
    where Dto : BaseDTO
    {
        public BaseBL(IBaseDL<TEntity, Dto, Dto> baseDL) : base(baseDL)
        {
        }
    }
}
