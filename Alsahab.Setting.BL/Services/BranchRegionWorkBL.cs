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
        public BranchRegionWorkBL(IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> branchRegionWorkDL, 
                                IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(branchRegionWorkDL, logDL)
        {
            _BranchRegionWorkDL = branchRegionWorkDL;
            _ZoneDL = zoneDL;
        }

        public override async Task<IList<BranchRegionWorkDTO>> GetAsync(BranchRegionWorkFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO pagine = null)
        {
            var response = await _BranchRegionWorkDL.GetAsync(filter, cancellationToken, pagine);

            var responseZone = await _ZoneDL.GetAllAsync(cancellationToken);

            if (!(responseZone?.Count > 0))
                throw new NotFoundException("Zone table is empty.");

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

    }
}
