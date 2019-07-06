using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.BL.Validation;
using System.Threading;
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
{
    public class SubsystemBL : BaseBL<Subsystem, SubsystemDTO, SubsystemFilterDTO>
    {
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        private readonly IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> _SubpartDL;
        public SubsystemBL(IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL,
                           IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL,
                           IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> subpartDL,
                           IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(subsystemDL, logDL)
        {
            _SubsystemDL = subsystemDL;
            _StatementSubsystemDL = statementSubsystemDL;
            _SubpartDL = subpartDL;
        }

        public async override Task CheckDeletePermisionAsync(SubsystemDTO data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);

            var statementSubsystemIDList = await _StatementSubsystemDL.GetAsync(new StatementSubsystemFilterDTO { SubsystemID = data.ID }, cancellationToken);
            var subpartIDList = await _SubpartDL.GetAsync(new DTO.SubpartFilterDTO { SubsystemID = data.ID }, cancellationToken);

            if (statementSubsystemIDList.Count > 0 || subpartIDList.Count > 0)
                throw new AppException(ResponseStatus.LoginError, "This Subsystem use in another Tables,Please Delete  them First");
        }

    }
}
