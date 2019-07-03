using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Alsahab.Setting.BL.Validation
{
    // internal class BLBranchValidator : Alsahab.Setting.DTO.BranchValidator
    // {
    //     private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
    //     public BLBranchValidator(IBaseDL<Branch, BranchDTO, BranchFilterDTO> _branchDL) : base()
    //     {
    //         _BranchDL = _branchDL;
    //         RuleFor(x => x.Title).Must((DTO, title) => UniqueTitleCondition(title, DTO.ID ?? 0)).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
    //         RuleFor(x => x.Code).Must((DTO, code) => UniqueCodeCondition(code, DTO.ID ?? 0)).When(x => !string.IsNullOrWhiteSpace(x.Code)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
    //         RuleFor(x => x.IsCentral).Must((DTO, IsCentral) => OnlyOneCentralCondition(IsCentral, DTO.ID ?? 0)).When(x => x.IsCentral.HasValue && x.IsCentral == true).WithMessage(ValidatorOptions.LanguageManager.GetString("NotCentral"));
    //         //TODO: باید بررسی شود که آیا چنین محدودیتی داریم یا خیر؟
    //         // RuleFor(x => x.HeadPersonID).Must((DTO,HeadPersonID) => UniqueHeadPersonID(HeadPersonID ?? 0, DTO.ID ?? 0)).When(x => x.HeadPersonID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
    //     }

    //     private bool UniqueTitleCondition(string title, long id)
    //     {
    //         var branch = _BranchDL.Get(new BranchFilterDTO { Title = title })?.Where(s => s.Title.Equals(title))?.ToList();
    //         return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
    //     }

    //     private bool UniqueCodeCondition(string code, long id)
    //     {
    //         var branch = _BranchDL.Get(new BranchFilterDTO { Code = code })?.Where(s => s.Code.Equals(code))?.ToList();
    //         return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
    //     }

    //     private bool OnlyOneCentralCondition(bool? isCental, long id)
    //     {
    //         if (!isCental.HasValue)
    //             return true;
    //         var branch = _BranchDL.Get(new BranchFilterDTO { IsCentral = true });
    //         return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
    //     }

    //     private bool UniqueHeadPersonID(long personHeadId, long id)
    //     {
    //         var branch = _BranchDL.Get(new BranchFilterDTO { HeadPersonID = personHeadId });
    //         return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
    //     }
    // }

    public class BLBranchValidator : BaseBLValidator<Branch, BranchDTO, BranchFilterDTO>
    {
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        public BLBranchValidator(IBaseDL<Branch, BranchDTO, BranchFilterDTO> _branchDL) : base(_branchDL)
        {
            _BranchDL = _branchDL;
            RuleFor(x => x.Title).Must((DTO, title) => UniqueTitleCondition(title, DTO.ID ?? 0)).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
            RuleFor(x => x.Code).Must((DTO, code) => UniqueCodeCondition(code, DTO.ID ?? 0)).When(x => !string.IsNullOrWhiteSpace(x.Code)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
            RuleFor(x => x.IsCentral).Must((DTO, IsCentral) => OnlyOneCentralCondition(IsCentral, DTO.ID ?? 0)).When(x => x.IsCentral.HasValue && x.IsCentral == true).WithMessage(ValidatorOptions.LanguageManager.GetString("NotCentral"));
            //TODO: باید بررسی شود که آیا چنین محدودیتی داریم یا خیر؟
            // RuleFor(x => x.HeadPersonID).Must((DTO,HeadPersonID) => UniqueHeadPersonID(HeadPersonID ?? 0, DTO.ID ?? 0)).When(x => x.HeadPersonID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }

        private bool UniqueTitleCondition(string title, long id)
        {
            var branch = _BranchDL.Get(new BranchFilterDTO { Title = title })?.Where(s => s.Title.Equals(title))?.ToList();
            return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
        }

        private bool UniqueCodeCondition(string code, long id)
        {
            var branch = _BranchDL.Get(new BranchFilterDTO { Code = code })?.Where(s => s.Code.Equals(code))?.ToList();
            return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
        }

        private bool OnlyOneCentralCondition(bool? isCental, long id)
        {
            if (!isCental.HasValue)
                return true;
            var branch = _BranchDL.Get(new BranchFilterDTO { IsCentral = true });
            return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
        }

        private bool UniqueHeadPersonID(long personHeadId, long id)
        {
            var branch = _BranchDL.Get(new BranchFilterDTO { HeadPersonID = personHeadId });
            return !(branch.Count > 0 && !(id > 0 && id == branch.FirstOrDefault().ID));
        }

    }

    public abstract class BaseBLValidator<TEntity, TDto, TFilterDto> : BaseDTOValidator<TDto>
    where TEntity : class, IEntity
    where TDto : BaseDTO
    where TFilterDto : TDto
    {
        private readonly IBaseDL<TEntity, TDto, TFilterDto> _BaseDL;
        public BaseBLValidator(IBaseDL<TEntity, TDto, TFilterDto> _BaseDL) : base()
        {

        }
    }
}
