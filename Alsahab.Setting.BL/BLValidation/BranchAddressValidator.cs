using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLBranchAddressValidator : BaseBLValidator<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO>// : Alsahab.Setting.DTO.BranchAddressValidator
    {
        private readonly IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> _BranchAddressDL;
        public BLBranchAddressValidator(IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> branchAddressDL) : base(branchAddressDL)
        {
            _BranchAddressDL = branchAddressDL;
        }
    }
}
