using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using FluentValidation;

namespace Alsahab.Setting.BL.Validation
{
    internal class BranchValidator : BaseValidator<BranchDTO>//Alsahab.Setting.DTO.BranchValidator
    {
        private readonly IBaseBL<Branch, BranchDTO> BranchBL;//BranchBL BranchBL;
        public BranchValidator(IBaseBL<Branch, BranchDTO> _branchBL)
        {
            BranchBL = _branchBL;
        }
        // BranchBL BranchBL = new BranchBL();

        public BranchValidator() : base()
        {
            RuleFor(x => x.Title).Must(TitleNotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must(CodeNotExist).When(x => !string.IsNullOrWhiteSpace(x.Code)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.IsCentral).Must((DTO, IsCentral) => Notcentral(IsCentral, DTO.ID ?? 0)).When(x => x.IsCentral.HasValue && x.IsCentral == true).WithMessage(ValidatorOptions.LanguageManager.GetString("NotCentral"));
        }

        private bool TitleNotExist(string title)
        {
            var result =  BranchBL.Get(new BranchFilterDTO { Title = title });
            var Count = result?.Where(s => s.Title.Equals(title))?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool CodeNotExist(string code)
        {
            var result = BranchBL.Get(new BranchFilterDTO { Code = code });
            var Count = result?.Where(s => s.Code.Equals(code))?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool Notcentral(bool? isCental, long id)
        {
            if (!isCental.HasValue)
                return true;
            var branch = BranchBL.Get(new BranchFilterDTO { IsCentral = true });
            if (branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID))
            {
                return false;
            }
            return true;
        }

    }
}
