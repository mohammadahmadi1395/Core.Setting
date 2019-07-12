using System;
using System.Linq;
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
using Alsahab.Setting.DL.Interfaces;
using FluentValidation.Results;
using Alsahab.Setting.BL.BLValidation;

namespace Alsahab.Setting.BL
{
    public class BaseBL<TEntity, Dto, FilterDto> : IBaseBL<TEntity, Dto, FilterDto>
        where Dto : BaseDTO, IBaseDTO //class
        where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
        where FilterDto : Dto
    {
        #region properties
        //  protected bool FormHasTree {  get;  set; }
        public bool NeedToAutoCode { get; set; }
        public UserInfoDTO User { get; set; }
        public Language Language { get; set; }
        public int? ResultCount { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
        public CascadeMode CascadeMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        private CultureInfo Culture { get; set; }
        public readonly List<Alsahab.Setting.BL.Log.ObserverBase<Dto>> _observers;
        private readonly IBaseDL<TEntity, Dto, FilterDto> _BaseDL;// = new IBaseDL<Dto, Branch>();
        private readonly IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> _LogDL;
        #endregion

        #region constructor
        public BaseBL(IBaseDL<TEntity, Dto, FilterDto> baseDL
                    , IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)//, IBaseValidator<Dto> baseValidator)
        {
            _BaseDL = baseDL;
            _LogDL = logDL;
            //     FormHasTree = false;
            NeedToAutoCode = false;
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            // ValidatorOptions.LanguageManager = new FluentValidation.Resources.LanguageManager();
            ValidatorOptions.LanguageManager.Culture = Culture;
            _observers = new List<Log.ObserverBase<Dto>>();
            _observers.Add(new Log.LogObserver<Dto>(logDL));
        }
        #endregion constructor

        #region Log
        protected void Notify<TObserverState>(TObserverState stateInfo) where TObserverState : Log.ObserverStateBase<Dto>
        {
            stateInfo.User = User;
            foreach (var observer in _observers)
            {
                observer.Notify(stateInfo);
            }
        }


        // public async Task RegisterLogAsync(Dto response, ActionType type, CancellationToken cancellationToken)
        // {
        //     if (response?.ID > 0)
        //     {
        //         Log.ObserverStateBase<Dto> state = new Log.ObserverStateBase<Dto>
        //         {
        //             User = User,
        //             Type = type,
        //             DTO = response
        //         };
        //         Notify(state);
        //     }
        // }
        // public async Task RegisterListLogAsync(IList<Dto> response, ActionType type, CancellationToken cancellationToken)
        // {
        //     foreach (var dto in response)
        //     {
        //         var temp = dto;
        //         if (temp?.ID > 0)
        //         {
        //             // temp = await _BaseDL.GetByIdAsync(cancellationToken, temp?.ID);
        //             Log.ObserverStateBase<Dto> state = new Log.ObserverStateBase<Dto>
        //             {
        //                 User = User,
        //                 Type = type,
        //                 DTO = temp
        //             };
        //             Notify(state);
        //         }
        //     }
        // }
        public async void RegisterLog(Dto response, ActionType type)
        {
            await Task.Run(() =>
            {
                if (response?.ID > 0)
                {
                    // response = _BaseDL.GetById(response?.ID);
                    Log.ObserverStateBase<Dto> state = new Log.ObserverStateBase<Dto>
                    {
                        User = User,
                        Type = type,
                        DTO = response
                    };
                    Notify(state);
                }
            });
        }
        public async void RegisterListLog(IList<Dto> response, ActionType type)
        {
            await Task.Run(() =>
            {
                foreach (var dto in response)
                {
                    var temp = dto;
                    if (temp?.ID > 0)
                    {
                        // temp = _BaseDL.GetById(temp?.ID);
                        Log.ObserverStateBase<Dto> state = new Log.ObserverStateBase<Dto>
                        {
                            User = User,
                            Type = type,
                            DTO = temp
                        };
                        Notify(state);
                    }
                }
            });
        }
        #endregion Log

        #region Validation
        protected bool Validate<T, TObject>(TObject data)
            where T : AbstractValidator<TObject>
        {
            Assert.NotNull(data as Dto, nameof(data));
            //Set Custom Translation
            ValidatorOptions.LanguageManager = new ErrorLanguageManager();
            var blInstanceType = typeof(T).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(T)) && t.BaseType.GenericTypeArguments[1] == typeof(TObject)).ToList().FirstOrDefault();
            var dtoInstanceType = typeof(TObject).Assembly.GetTypes().FirstOrDefault(t => t.BaseType.GenericTypeArguments.Length > 0 && t.BaseType.GenericTypeArguments[0].Name.Equals(typeof(TObject).Name));
            //Create Instance From Validator    
            var blValidator = Activator.CreateInstance(blInstanceType, _BaseDL);
            var dtoValidator = Activator.CreateInstance(dtoInstanceType, new object[] { });
            //Set Culture To Translate
            ValidatorOptions.LanguageManager.Culture = Culture;
            var blResult = ((AbstractValidator<TObject>)blValidator).Validate(data);
            var cc = (AbstractValidator<TObject>)dtoValidator;
            var xc = cc.Validate(data);
            var dtoResult = ((AbstractValidator<TObject>)dtoValidator).Validate(data);
            ValidationErrors = blResult.Errors;
            ((List<ValidationFailure>)ValidationErrors).AddRange(dtoResult.Errors);

            string error = string.Empty;
            foreach (var verror in ValidationErrors)
                error += "\n" + verror.ErrorMessage;

            if (!blResult.IsValid || !dtoResult.IsValid)
                throw new AppException(ResponseStatus.ServerError, error);
            return true;
        }
        public virtual void CheckDeletePermision(Dto data)
        {
            Assert.NotNull(data, nameof(data));

            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "node Entered Is Mistake.");
        }
        public async virtual Task CheckDeletePermisionAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));
            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "node Entered Is Mistake.");
        }
        #endregion Validation

        #region Update
        protected async Task<Dto> MergeNewAndOldDataForUpdateAsync(Dto data, CancellationToken cancellationToken)
        {
            Assert.NotNull(data, nameof(data));
            //داده‌های قبلی را می‌گیرد و تنها داده‌های جدید دارای مقدار را آپدیت می‌کند
            var old_data = await _BaseDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            foreach (var propery in data.GetType().GetProperties())
            {
                var value = propery.GetValue(data);
                if (value != null)
                    propery.SetValue(old_data, value, null);
            }
            data = old_data;
            return data;
        }
        protected Dto MergeNewAndOldDataForUpdate(Dto data)
        {
            Assert.NotNull(data, nameof(data));
            //داده‌های قبلی را می‌گیرد و تنها داده‌های جدید دارای مقدار را آپدیت می‌کند
            var old_data = _BaseDL.GetById(data.ID ?? 0);
            foreach (var propery in data.GetType().GetProperties())
            {
                var value = propery.GetValue(data);
                if (value != null)
                    propery.SetValue(old_data, value, null);
            }
            data = old_data;
            return data;
        }
        public virtual async Task<Dto> UpdateAsync(Dto data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdateAsync(data, cancellationToken);

            Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(data);

            var response = await _BaseDL.UpdateAsync(data, cancellationToken);

            RegisterLog(response, ActionType.Update);

            return response;
        }
        public virtual Dto Update(Dto data)
        {
            data = MergeNewAndOldDataForUpdate(data);
            Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(data);

            var response = _BaseDL.Update(data);

            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();

            RegisterLog(response, ActionType.Update);

            return response;
        }
        public async virtual Task<IList<Dto>> UpdateListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                Dto temp = dto;
                temp = MergeNewAndOldDataForUpdate(temp);
                Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(temp);
                tempList.Add(temp);
            }
            var response = await _BaseDL.UpdateListAsync(tempList, cancellationToken);

            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();

            RegisterListLog(response, ActionType.Update);

            return response;
        }
        public virtual IList<Dto> UpdateList(IList<Dto> list)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                Dto temp = dto;
                temp = MergeNewAndOldDataForUpdate(temp);
                Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(temp);
                tempList.Add(temp);
            }
            var response = _BaseDL.UpdateList(tempList);

            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();


            RegisterListLog(response, ActionType.Update);

            return response;
        }
        #endregion Update

        #region Get
        public virtual async Task<IList<Dto>> GetAllAsync(CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var result = await _BaseDL.GetAllAsync(cancellationToken, paging);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }
        public virtual IList<Dto> GetAll(PagingInfoDTO paging = null)
        {
            var result = _BaseDL.GetAll(paging);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }
        public virtual IList<Dto> Get(FilterDto filter, PagingInfoDTO paging = null)
        {
            var response = _BaseDL.Get(filter, paging);
            ResultCount = _BaseDL.ResultCount;
            return response;
        }
        public virtual async Task<IList<Dto>> GetAsync(FilterDto filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)// = new CancellationToken())
        {
            var result = await _BaseDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _BaseDL.ResultCount;
            return result;
        }
        #endregion Get

        #region Delete
        public virtual async Task<Dto> DeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            await CheckDeletePermisionAsync(data, cancellationToken);
            data = await _BaseDL.GetByIdAsync(cancellationToken, data.ID);
            await _BaseDL.DeleteAsync(data, cancellationToken);
            RegisterLog(data, ActionType.Delete);
            return data;
        }
        public virtual Dto Delete(Dto data)
        {
            CheckDeletePermision(data);
            data = _BaseDL.GetById(data.ID);
            var response = _BaseDL.Delete(data);
            RegisterLog(data, ActionType.Delete);
            return response;
        }
        public async virtual Task<IList<Dto>> DeleteListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                var temp = dto;
                await CheckDeletePermisionAsync(temp, cancellationToken);
                temp = await _BaseDL.GetByIdAsync(cancellationToken, temp.ID);
                tempList.Add(temp);
            }
            await _BaseDL.DeleteListAsync(tempList, cancellationToken);
            RegisterListLog(tempList, ActionType.Delete);
            return tempList;
        }
        public virtual IList<Dto> DeleteList(IList<Dto> list)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                var temp = dto;
                CheckDeletePermision(temp);
                temp = _BaseDL.GetById(temp.ID);
                tempList.Add(temp);
            }
            _BaseDL.DeleteList(tempList);
            RegisterListLog(tempList, ActionType.Delete);
            return tempList;
        }
        #endregion Delete

        #region Insert
        public virtual async Task<Dto> InsertAsync(Dto data, CancellationToken cancellationToken)
        {
            Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(data);
            data.CreateDate = DateTime.Now;
            var response = await _BaseDL.InsertAsync(data, cancellationToken);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterLog(response, ActionType.Insert);
            return response;
        }
        public virtual Dto Insert(Dto data)
        {
            Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(data);
            data.CreateDate = DateTime.Now;
            var response = _BaseDL.Insert(data);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterLog(response, ActionType.Insert);
            return response;
        }
        public virtual async Task<IList<Dto>> InsertListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            foreach (var d in list)
            {
                Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _BaseDL.InsertListAsync(list, cancellationToken);

            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();

            RegisterListLog(list, ActionType.Insert);

            return response;
        }
        public virtual IList<Dto> InsertList(IList<Dto> list)
        {
            foreach (var d in list)
            {
                Validate<BaseBLValidator<TEntity, Dto, FilterDto>, Dto>(d);
                d.CreateDate = DateTime.Now;
            }

            var response = _BaseDL.InsertList(list);

            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();

            RegisterListLog(list, ActionType.Insert);

            return response;
        }
        #endregion Insert

        #region SoftDelete
        public async virtual Task<Dto> SoftDeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            await CheckDeletePermisionAsync(data, cancellationToken);
            data = await _BaseDL.GetByIdAsync(cancellationToken, data.ID);
            data.IsDeleted = true;
            await _BaseDL.UpdateAsync(data, cancellationToken);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterLog(data, ActionType.SoftDelete);
            return data;
        }
        public async virtual Task<IList<Dto>> SoftDeleteListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                await CheckDeletePermisionAsync(dto, cancellationToken);
                var temp = dto;
                temp = await _BaseDL.GetByIdAsync(cancellationToken, dto.ID);
                temp.IsDeleted = true;
                tempList.Add(temp);
            }
            await _BaseDL.UpdateListAsync(tempList, cancellationToken);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterListLog(tempList, ActionType.SoftDelete);
            return tempList;
        }
        public virtual Dto SoftDelete(Dto data)
        {
            CheckDeletePermision(data);
            data = _BaseDL.GetById(data.ID);
            data.IsDeleted = true;
            _BaseDL.Update(data);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterLog(data, ActionType.SoftDelete);
            return data;
        }
        public virtual IList<Dto> SoftDeleteList(IList<Dto> list)
        {
            var tempList = new List<Dto>();
            foreach (var dto in list)
            {
                CheckDeletePermision(dto);
                var temp = dto;
                temp = _BaseDL.GetById(dto.ID);
                temp.IsDeleted = true;
                tempList.Add(temp);
            }
            _BaseDL.UpdateList(tempList);
            //if (FormHasTree)
            //    UpdateTreeIndicesAndCodes();
            RegisterListLog(tempList, ActionType.SoftDelete);
            return tempList;
        }

        #endregion SoftDelete


    }


    public class BaseTreeBL<TEntity, Dto, FilterDto> : BaseBL<TEntity, Dto, FilterDto>
           where Dto : BaseTreeDTO //class
    where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
    where FilterDto : Dto
    {

        private readonly IBaseDL<TEntity, Dto, FilterDto> _BaseDL;// = new IBaseDL<Dto, Branch>();

        #region constructor
        public BaseTreeBL(IBaseDL<TEntity, Dto, FilterDto> baseDL
                    , IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(baseDL, logDL)
        {
            _BaseDL = baseDL;

        }
        #endregion constructor
        #region Validation
        public override void CheckDeletePermision(Dto data)
        {
            base.CheckDeletePermision(data);

            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "node Entered Is Mistake.");
            var deletingItem = _BaseDL.GetById(data.ID);
            var myLeft = deletingItem.LeftIndex;
            var myRight = deletingItem.RightIndex;
            var deleteCount = AllDtos.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();
            if (deleteCount > 1)
                throw new AppException(ResponseStatus.LoginError, "You can't delete this node. this node has child");

        }

        public async override Task CheckDeletePermisionAsync(Dto data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);
            var deletingItem = await _BaseDL.GetByIdAsync(cancellationToken, data.ID);
            var myLeft = deletingItem.LeftIndex;
            var myRight = deletingItem.RightIndex;
            var deleteCount = AllDtos.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();
            if (deleteCount > 1)
                throw new AppException(ResponseStatus.LoginError, "You can't delete this node. this node has child");

        }
        #endregion Validation
        #region Update
        public override async Task<Dto> UpdateAsync(Dto data, CancellationToken cancellationToken)
        {
            var response = await base.UpdateAsync(data, cancellationToken);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override Dto Update(Dto data)
        {
            var response = base.Update(data);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override async Task<IList<Dto>> UpdateListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {

            var response = await base.UpdateListAsync(list, cancellationToken);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override IList<Dto> UpdateList(IList<Dto> list)
        {

            var response = base.UpdateList(list);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        #endregion Update
        #region Insert
        public override async Task<Dto> InsertAsync(Dto data, CancellationToken cancellationToken)
        {

            var response = await base.InsertAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();
            return response;

        }
        public override Dto Insert(Dto data)
        {
            var response = base.Insert(data);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override async Task<IList<Dto>> InsertListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            var response = await base.InsertListAsync(list, cancellationToken);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override IList<Dto> InsertList(IList<Dto> list)
        {
            var response = base.InsertList(list);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        #endregion Insert
        #region SoftDelete
        public override async Task<Dto> SoftDeleteAsync(Dto data, CancellationToken cancellationToken)
        {
            var response = await base.UpdateAsync(data, cancellationToken);
            UpdateTreeIndicesAndCodes();
            return data;
        }
        public override async Task<IList<Dto>> SoftDeleteListAsync(IList<Dto> list, CancellationToken cancellationToken)
        {
            var response = await base.UpdateListAsync(list, cancellationToken);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override Dto SoftDelete(Dto data)
        {

            var response = base.Update(data);
            UpdateTreeIndicesAndCodes();
            return response;
        }
        public override IList<Dto> SoftDeleteList(IList<Dto> list)
        {
            var response = base.UpdateList(list);
            UpdateTreeIndicesAndCodes();
            return response;
        }

        #endregion SoftDelete
        #region related to tree
        private IList<Dto> _AllDtos;
        public IList<Dto> AllDtos
        {
            get
            {
                if (!(_AllDtos?.Count > 0))
                    _AllDtos = base.GetAll();
                return _AllDtos;
            }
        }
        private List<Dto> TreeNodes = new List<Dto>();
        private long? _index = 0, _depth = 2;
        private void UpdateTreeIndicesAndCodes()
        {
            _AllDtos = null;
            var dtos = AllDtos;
            foreach (var node in dtos)
            {
                node.LeftIndex = null;
                node.RightIndex = null;
                node.Depth = null;
                node.Code = null;
                TreeNodes.Add(node);
            }

            List<Dto> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllDtosIndices(root);
                }

            var codedDtos = new List<Dto>();
            if (NeedToAutoCode)
            {
                codedDtos = GenerateNewCodeList(rootList);
                base.UpdateList(codedDtos);
            }
        }
        // مراحل:
        // ۱- ابتدا اندیس چپ را تنظیم می‌کند
        // ۲- سپس عمق را تنظیم می‌کند
        // ۳- اندیس چپ و عمق را برای فرزندش در صورت وجود تنظیم می‌کند
        // ۴- در صورت عدم وجود فرزند، اندیس راست را تنظیم می‌کند
        // ۵- به سراغ برادر (در صورت وجود) می‌رود و مراحل اول تا چهارم را برای آن انجام می‌دهد
        private void RecursiveUpdateAllDtosIndices(Dto BaseDLta)
        {
            if (!(BaseDLta?.ID > 0) || !(TreeNodes?.Count > 0))
                return;

            TreeNodes.FirstOrDefault(i => i.ID == BaseDLta.ID).LeftIndex = ++_index;
            TreeNodes.FirstOrDefault(i => i.ID == BaseDLta.ID).Depth = _depth;

            var tempChild = GetNotIndexedChild(BaseDLta);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllDtosIndices(tempChild);
            }

            TreeNodes.FirstOrDefault(i => i.ID == BaseDLta.ID).RightIndex = ++_index;
            var tempBrother = GetNotIndexedBrother(BaseDLta);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllDtosIndices(tempBrother);
            else
                _depth--;
        }
        // private String GenerateCodeInInsert(Dto data)
        // {
        //     if (!(data?.ParentID > 0))
        //         return (AllDtos?.Where(s => s.ParentID == null)?.ToList()?.Count + 1).ToString();
        //     else
        //     {
        //         var r = AllDtos?.Where(s => s.ParentID == data?.ParentID)?.ToList()?.Count;
        //         var parentCode = AllDtos?.FirstOrDefault(s => s.ID == data?.ParentID)?.Code;
        //         return String.Format("{0}-{1}", parentCode, (r + 1)?.ToString());
        //     }
        // }
        private List<Dto> GenerateNewCodeList(List<Dto> data)
        {
            List<Dto> res = new List<Dto>();
            for (int thisBranch = 0; thisBranch < data?.Count; thisBranch++)
            {
                var parent = GetParent(data[thisBranch]);
                data[thisBranch].Code = (parent == null) ?
                    string.Format("{0}", (thisBranch + 1)) :
                    string.Format("{0}-{1}", parent?.Code, (thisBranch + 1));

                res.Add(data[thisBranch]);

                var childs = AllDtos?.Where(s => s.ParentID == data[thisBranch]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisBranch].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodeList(AllDtos?.Where(s => s.ParentID == childs[child]?.ID)?.ToList()));
                }
            }
            return res;
        }
        private Dto GetNotIndexedBrother(Dto node)
        {
            if (!(node.ParentID > 0))
                return null;
            var parent = TreeNodes.FirstOrDefault(i => i.ID == node.ParentID);
            var brother = GetNotIndexedChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        private Dto GetNotIndexedChild(Dto node)
        {
            return TreeNodes.FirstOrDefault(i => i.ParentID == node.ID && !(i.LeftIndex > 0));
        }
        private Dto GetParent(Dto data)
        {
            return AllDtos?.FirstOrDefault(s => s.ID == data?.ParentID);
        }
        #endregion related to tree

    }

    public class BaseBL<TEntity, Dto> : BaseBL<TEntity, Dto, Dto>
        where TEntity : BaseEntity<TEntity, Dto, long>, IEntity
        where Dto : BaseTreeDTO, IBaseDTO

    {
        public BaseBL(IBaseDL<TEntity, Dto, Dto> baseDL
                    , IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(baseDL, logDL)
        {
        }
    }
}
