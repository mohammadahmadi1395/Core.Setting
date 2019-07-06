using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;

namespace Alsahab.Setting.BL
{
    public class StatementSubsystemBL : BaseBL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO>
    {
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        public StatementSubsystemBL(IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL,
                                    IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) 
                                    : base(statementSubsystemDL, logDL)
        {
            _StatementSubsystemDL = statementSubsystemDL;
        }
    }
}
