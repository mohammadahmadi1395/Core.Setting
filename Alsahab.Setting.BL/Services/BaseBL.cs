using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using Alsahab.Common;
using FluentValidation;
using Alsahab.Common.Validation;
using System.Threading;
using Alsahab.Setting.Entities;
using Alsahab.Common.Utilities;
using Alsahab.Common.Exceptions;
using Alsahab.Common.Api;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data.Interfaces;
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
        public UserInfoDTO User { get; set; }
        public Language Language { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public int? ResultCount { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
        public string ErrorMessage { get; set; }
        public CascadeMode CascadeMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        private CultureInfo Culture { get; set; }
        private readonly IBaseDL<TEntity, Dto, FilterDto> _BaseDL;// = new IBaseDL<BranchDTO, Branch>();
        readonly List<Observers.ObserverBase> _observers;
        // private readonly IBaseValidator<Dto> _BaseValidator;
        // public BranchBL()
        // {
        // }

        public BaseBL(IBaseDL<TEntity, Dto, FilterDto> baseDL)//, IBaseValidator<Dto> baseValidator)
        {
            _BaseDL = baseDL;
            // _BaseValidator = baseValidator;
            ResponseStatus = ResponseStatus.ServerError;
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            // ValidatorOptions.LanguageManager = new FluentValidation.Resources.LanguageManager();
            ValidatorOptions.LanguageManager.Culture = Culture;
            _observers = new List<Observers.ObserverBase>();
            _observers.Add(new Observers.LogObserver());
        }

        //public long? TeamID { get; set; }
        protected void Notify<TObserverState>(TObserverState stateInfo) where TObserverState : Observers.ObserverStates.ObserverStateBase
        {
            stateInfo.User = User;
            foreach (var observer in _observers)
            {
                observer.Notify(stateInfo);
            }
        }

        protected async Task<Dto> MergeNewAndOldDataForUpdate(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));
            //داده‌های قبلی را می‌گیرد و تنها داده‌های جدید دارای مقدار را آپدیت می‌کند
            var old_data = await _BaseDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            Assert.NotNull(old_data, nameof(old_data));
            foreach (var propery in data.GetType().GetProperties())
            {
                var value = propery.GetValue(data);
                if (value != null)
                    propery.SetValue(old_data, value, null);
            }
            data = old_data;
            return data;
        }

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

            string error = string.Empty;
            foreach (var verror in ValidationErrors)
                error += "\n" + verror.ErrorMessage;

            if (!result.IsValid)
                throw new AppException(ResponseStatus.ServerError, error);
            return true;
        }

        public virtual async Task<IList<Dto>> GetAllAsync(CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var result = await _BaseDL.GetAllAsync(cancellationToken);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }
        public virtual IList<Dto> GetAll(PagingInfoDTO paging = null)
        {
            var result = _BaseDL.GetAll(paging);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }
        public virtual async Task<IList<Dto>> GetAsync(FilterDto filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)// = new CancellationToken())
        {
            var result = await _BaseDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }

        public virtual async Task<Dto> UpdateAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));

            // فیلدهای خالی را با مقادیر قبلی پر می‌کند
            var old_data = await _BaseDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            if (old_data == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");

            foreach (var propery in data.GetType().GetProperties())
            {
                var value = propery.GetValue(data);
                if (value != null)
                    propery.SetValue(old_data, value, null);
            }

            data = old_data;

            return await _BaseDL.UpdateAsync(data, cancellationToken);
        }

        public virtual async Task<Dto> DeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            return await _BaseDL.DeleteAsync(data, cancellationToken);
        }

        public virtual async Task<Dto> InsertAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));

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
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            // {
            //     ErrorMessage += _BaseDL.ErrorMessage;
            //     return null;
            // }

            return response;
        }


        public virtual Dto Insert(Dto data)
        {
            data.CreateDate = DateTime.Now;
            //TODO
            // data.Code = GenerateCode(data);

            var response = _BaseDL.Insert(data);
            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);

            return response;
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

            var response = _BaseDL.Update(data);
            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            return response;
        }

        public async virtual Task<Dto> SoftDeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));
            data.IsDeleted = true;

            var response = await _BaseDL.UpdateAsync(data, cancellationToken);
            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            return response;
        }

        public virtual Dto SoftDelete(Dto data)
        {
            Assert.NotNull(data, nameof(data));
            data.IsDeleted = true;
            var response = _BaseDL.Update(data);

            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            return response;
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

            var response = _BaseDL.Delete(data);
            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            return response;

        }

        public virtual Task<IList<Dto>> DeleteListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual IList<Dto> DeleteList(IList<Dto> list)
        {
            var response = _BaseDL.DeleteList(list);
            ResponseStatus = _BaseDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.ServerError, _BaseDL.ErrorMessage);
            return response;

        }

        public virtual IList<Dto> Get(FilterDto filter, PagingInfoDTO paging = null)
        {
            var response = _BaseDL.Get(filter, paging);
            ResultCount = _BaseDL.ResultCount;
            return response;

        }
    }

    public class BaseBL<TEntity, Dto> : BaseBL<TEntity, Dto, Dto>
        where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
        where Dto : BaseDTO//class
                           // where TEntity : class, IEntity//BaseEntity<TEntity, Dto>
                           // where Dto : BaseDTO
    {
        public BaseBL(IBaseDL<TEntity, Dto, Dto> baseDL) : base(baseDL)
        {
        }
    }
}
