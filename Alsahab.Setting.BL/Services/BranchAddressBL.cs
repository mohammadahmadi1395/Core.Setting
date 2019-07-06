using System;
using System.Collections.Generic;
using System.Linq;
using Alsahab.Common.Exceptions;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Common;
using System.Threading.Tasks;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class BranchAddressBL : BaseBL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO>
    {
        private readonly IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> _BranchAddressDL;
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public BranchAddressBL(IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> branchAddressDL,
                                IBaseDL<Branch, BranchDTO, BranchFilterDTO> branchDL,
                                IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(branchAddressDL, logDL)
        {
            _BranchAddressDL = branchAddressDL;
            _BranchDL = branchDL;
            _ZoneDL = zoneDL;
        }
        private async Task CheckDeletePermission(BranchAddressDTO data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);
            if ((await _BranchDL.GetAsync(new BranchFilterDTO { BranchAddressID = data.ID }, cancellationToken)).Count() > 0)
                throw new AppException(ResponseStatus.LoginError, "This BranchAddress use in another Tables,Please Delete  them First");
        }

        public override async Task<IList<BranchAddressDTO>> GetAsync(BranchAddressFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            // zbl.User = User;
            var responseAddress = await _BranchAddressDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _BranchAddressDL.ResultCount;

            var allZone = await _ZoneDL.GetAllAsync(cancellationToken);

            foreach (var val in responseAddress)
                val.ZoneDTO.ZoneAddress = allZone.FirstOrDefault(s => s.ID == val.ZoneDTO.ID)?.ZoneAddress;

            return responseAddress;
        }

    }
}
