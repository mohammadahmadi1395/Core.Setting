using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using System.Threading;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class PrefixBL : BaseBL<Prefix, PrefixDTO, PrefixFilterDTO>
    {
        private readonly IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> _PrefixDL;
        public PrefixBL(IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> prefixDL,
                        IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(prefixDL, logDL)
        {
            _PrefixDL = prefixDL;
        }
    }
}
