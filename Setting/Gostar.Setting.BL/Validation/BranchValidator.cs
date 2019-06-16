using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common.Validation;

namespace Gostar.Setting.BL.Validation
{
  internal   class BranchValidator : Gostar.Setting.DTO.Validation.BranchValidator
    {
        BranchBL BranchBL = new BranchBL();

        public BranchValidator() : base()
        {
            RuleFor(x => x.Title).Must(TitleNotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must(CodeNotExist).When(x => !string.IsNullOrWhiteSpace(x.Code)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x=>x.IsCentral).Must((DTO, IsCentral)=>Notcentral( IsCentral,DTO.ID??0)).When(x => x.IsCentral.HasValue && x.IsCentral == true).WithMessage(ValidatorOptions.LanguageManager.GetString("NotCentral"));
        }

        private bool TitleNotExist(string title)
        {
            var result = BranchBL.BranchGet(new BranchDTO { Title = title });
            var Count = result?.Where(s => s.Title == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool CodeNotExist(string code)
        {
            var result = BranchBL.BranchGet(new BranchDTO { Code =code  });
            var Count = result?.Where(s => s.Code == code)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool Notcentral(bool? isCental,long id)
        {
            if (!isCental.HasValue)
                return true;
            var Branch = BranchBL.BranchGet(new BranchDTO { IsCentral = true });
            if (Branch.Count > 0 && !(id > 0 && id == Branch.FirstOrDefault().ID))
            {
                return false;
            }
            return true;
        }

    }
}
