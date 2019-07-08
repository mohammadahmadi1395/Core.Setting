using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common.Validation;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL.BLValidation
{
    internal class StatementSubsystemBLValidator :BaseBLValidator<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO>//: Alsahab.Setting.DTO.StatementSubsystemValidator
    {
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        public StatementSubsystemBLValidator(IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL) : base(statementSubsystemDL)
        {
            _StatementSubsystemDL = statementSubsystemDL;
        }
    }
}
