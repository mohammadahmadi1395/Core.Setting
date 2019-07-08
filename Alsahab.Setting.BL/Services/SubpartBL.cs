using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.Entities.Models;
using System.Threading;
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
{
    public class SubpartBL : BaseBL<Subpart, SubpartDTO, SubpartFilterDTO>
    {
        private readonly IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> _SubpartDL;
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        public SubpartBL(IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> subpartDL,
                        IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL,
                        IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(subpartDL, logDL)
        {
            _SubpartDL = subpartDL;
            _SubsystemDL = subsystemDL;
        }
        public override async Task CheckDeletePermisionAsync(SubpartDTO data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);
            
            var Subsystem = await _SubsystemDL.GetByIdAsync(cancellationToken, data.SubsystemID);
            if ((Subsystem.ID > 0))
                throw new AppException(ResponseStatus.DatabaseError, "This subpart is used in antoher tables.");
        }
    }
}
