using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.BL.Validation;
using Alsahab.Setting.Data.Interfaces;
using System.Threading;
using Alsahab.Common.Exceptions;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class FormTypeBL : BaseBL<FormType, FormTypeDTO, FormTypeFilterDTO>
    {
        private readonly IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> _FormTypeDL;
        public FormTypeBL(IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> formTypeDL) : base(formTypeDL)
        {
            _FormTypeDL = formTypeDL;
        }

        // private bool Validate(FormTypeDTO data)
        // {
        //     return Validate<BLFormTypeValidator, FormTypeDTO>(data ?? new FormTypeDTO());
        // }
        
        private bool CheckDeletePermision(FormTypeDTO data)
        {
            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "Id is empty");

            if (_FormTypeDL.GetById(data.ID).Enum != null)
                throw new AppException(ResponseStatus.LoginError, "This Type Is Non Deleteable");

            return true;
        }

        public override async Task<IList<FormTypeDTO>> GetAsync(FormTypeFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _FormTypeDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _FormTypeDL.ResultCount;
            return response;
        }

        // public async override Task<FormTypeDTO> InsertAsync(FormTypeDTO data, CancellationToken cancellationToken)
        // {
        //     data = await base.InsertAsync(data, cancellationToken);
        //     // Validate(data);

        //     data.CreateDate = DateTime.Now;

        //     var response = await _FormTypeDL.InsertAsync(data, cancellationToken);

        //     if (_FormTypeDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //         throw new AppException(ResponseStatus.DatabaseError, _FormTypeDL.ErrorMessage);

        //     response = await _FormTypeDL.GetByIdAsync(cancellationToken, response?.ID);

        //     Observers.ObserverStates.FormTypeAdd state = new Observers.ObserverStates.FormTypeAdd
        //     {
        //         FormType = response,
        //         User = User,
        //     };
        //     Notify(state);

        //     return response;
        // }
        public async override Task<IList<FormTypeDTO>> InsertListAsync(IList<FormTypeDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                // Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _FormTypeDL.InsertListAsync(data, cancellationToken);
            if (_FormTypeDL.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _FormTypeDL.ErrorMessage);

            List<FormTypeDTO> respList = new List<FormTypeDTO>();
            foreach (var val in response)
            {
                var resp = await _FormTypeDL.GetByIdAsync(cancellationToken, val.ID);
                Observers.ObserverStates.FormTypeAdd state = new Observers.ObserverStates.FormTypeAdd
                {
                    FormType = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            return respList;
        }
        
        public async override Task<FormTypeDTO> UpdateAsync(FormTypeDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            // Validate(data);

            var response = await _FormTypeDL.UpdateAsync(data, cancellationToken);
            if (_FormTypeDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _FormTypeDL.ErrorMessage);

            response = await _FormTypeDL.GetByIdAsync(cancellationToken, response?.ID);

            Observers.ObserverStates.FormTypeEdit state = new Observers.ObserverStates.FormTypeEdit
            {
                FormType = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        public async override Task<FormTypeDTO> SoftDeleteAsync(FormTypeDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermision(data);

            data.IsDeleted = true;
            var response = await _FormTypeDL.UpdateAsync(data, cancellationToken);

            Observers.ObserverStates.FormTypeDelete state = new Observers.ObserverStates.FormTypeDelete
            {
                FormType = response,
                User = User,
            };
            Notify(state);

            return response;
        }
    }
}
