using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLStatementValidator : BaseBLValidator<Statement, StatementDTO, StatementFilterDTO>// : Alsahab.Setting.DTO.StatementValidator
    {
        private readonly IBaseDL<Statement, StatementDTO, StatementFilterDTO> _StatementDL;
        public BLStatementValidator(IBaseDL<Statement, StatementDTO, StatementFilterDTO> statementDL) : base(statementDL)
        {
            _StatementDL = statementDL;
        }
    }
}
