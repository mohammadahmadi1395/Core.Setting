using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Alsahab.Common.Exceptions;
using Alsahab.Setting.BL;
using Alsahab.Setting.BL.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Common;
using System.Threading.Tasks;
using Alsahab.Common.Utilities;

namespace Alsahab.Setting.BL
{
    public class BranchRegionWorkBL : BaseBL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO>
    {
        private readonly IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> _BranchRegionWorkDL;
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public BranchRegionWorkBL(IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> branchRegionWorkDL, IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL)
            : base(branchRegionWorkDL)
        {
            _BranchRegionWorkDL = branchRegionWorkDL;
            _ZoneDL = zoneDL;
        }
        private bool Validate(BranchRegionWorkDTO data)
        {

            return Validate<BLBranchRegionWorkValidator, BranchRegionWorkDTO>(data ?? new BranchRegionWorkDTO());
        }

        public override async Task<IList<BranchRegionWorkDTO>> GetAsync(BranchRegionWorkFilterDTO filter, CancellationToken cancellationToken)
        {
            var response = await _BranchRegionWorkDL.GetAsync(filter, cancellationToken);
            if (_BranchRegionWorkDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.LoginError, _BranchRegionWorkDL.ErrorMessage);

            var responseZone = await _ZoneDL.GetAllAsync(cancellationToken);
            if (_ZoneDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.LoginError, _ZoneDL.ErrorMessage);

            var result = (from Rw in response
                          join Zone in responseZone on Rw.ZoneID equals Zone.ID
                          select new BranchRegionWorkDTO
                          {
                              ID = Rw.ID,
                              BranchID = Rw.BranchID,
                              ZoneID = Rw.ZoneID,
                              CreateDate = Rw.CreateDate,
                              IsDeleted = Rw.IsDeleted,
                              ZoneAndChilds = Zone.ZoneAndChilds,
                              ZoneAndParents = Zone.ZoneAndParents
                          })?.ToList();
            return result;
        }

        public override async Task<BranchRegionWorkDTO> InsertAsync(BranchRegionWorkDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;

            var response = await _BranchRegionWorkDL.InsertAsync(data, cancellationToken);
            if (_BranchRegionWorkDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.LoginError, _BranchRegionWorkDL.ErrorMessage);

            response = await _BranchRegionWorkDL.GetByIdAsync(cancellationToken, response.ID ?? 0);
            Alsahab.Setting.BL.Observers.ObserverStates.BranchRegionWorkAdd state = new Alsahab.Setting.BL.Observers.ObserverStates.BranchRegionWorkAdd
            {
                BranchRegionWork = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        // public override async Task<IList<BranchRegionWorkDTO>> InsertListAsync(List<BranchRegionWorkDTO> data)
        // {
            //TODO: از آقای صفری یا متقیان پرسیده شود که این چیست؟
            // List<BranchRegionWorkDTO> tempList = new List<BranchRegionWorkDTO>();
            // if (data.Count == 1 && data.FirstOrDefault().ZoneID == null)
            // {
            //     var rd = _BranchRegionWorkDL.BranchRegionWorkDeleteByBranchID(data?.FirstOrDefault().BranchID);
            //     if (rd != Common.ResponseStatus.Successful)
            //     {
            //         ErrorMessage += "Cant Update !";
            //         return null;
            //     }
            //     else
            //     {
            //         ResponseStatus = Common.ResponseStatus.Successful;
            //         return data;
            //     }
            // }

            // foreach (var val in data)
            // {
            //     val.CreateDate = DateTime.Now;
            //     Validate(val);
            //     tempList.Add(val);
            // }

            // data = tempList;
            // if (!(data.Count > 0))
            // {
            //     ErrorMessage += "This Region(s) Is Exists ! \n";
            //     return null;
            // }
            // var ResponseDelete = _BranchRegionWorkDL.BranchRegionWorkDeleteByBranchID(data?.FirstOrDefault().BranchID);
            // if (ResponseDelete != Common.ResponseStatus.Successful)
            // {
            //     ErrorMessage += "Cant Update !";
            //     return null;
            // }
            // var Response = _BranchRegionWorkDL.BranchRegionWorkInsert(data);

            // List<BranchRegionWorkDTO> respList = new List<BranchRegionWorkDTO>();
            // foreach (var val in Response)
            // {
            //     var resp = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
            //     Observers.ObserverStates.BranchRegionWorkAdd state = new Observers.ObserverStates.BranchRegionWorkAdd
            //     {
            //         BranchRegionWork = resp ?? val,
            //         User = User,
            //     };
            //     Notify(state);
            //     respList.Add(resp);
            // }

            // ResponseStatus = _BranchRegionWorkDL.ResponseStatus;
            // if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            // {
            //     ErrorMessage += _BranchRegionWorkDL.ErrorMessage;
            //     return null;
            // }

            // return respList ?? Response;
        // }

        public override async Task<BranchRegionWorkDTO> UpdateAsync(BranchRegionWorkDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _BranchRegionWorkDL.UpdateAsync(data, cancellationToken);
            if (_BranchRegionWorkDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchRegionWorkDL.ErrorMessage);

            response = await _BranchRegionWorkDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            Observers.ObserverStates.BranchRegionWorkEdit state = new Observers.ObserverStates.BranchRegionWorkEdit
            {
                BranchRegionWork = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        public override async Task<BranchRegionWorkDTO> SoftDeleteAsync(BranchRegionWorkDTO data, CancellationToken cancellationToken)
        {
            //TODO:
            // باید بررسی شود و در صورت لزوم تعریف شود
            // CheckDeletePermision(data);

            data = await _BranchRegionWorkDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            data.IsDeleted = true;
            var response = await _BranchRegionWorkDL.UpdateAsync(data, cancellationToken);

            Observers.ObserverStates.BranchRegionWorkDelete state = new Observers.ObserverStates.BranchRegionWorkDelete
            {
                BranchRegionWork = response,
                User = User,
            };
            Notify(state);

            return response;
        }
    }
}
