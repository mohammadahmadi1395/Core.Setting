﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Alsahab.Setting.BL.BLValidation
{
    public class BranchBLValidator : BaseBLValidator<Branch, BranchDTO, BranchFilterDTO>
    {
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        public BranchBLValidator(IBaseDL<Branch, BranchDTO, BranchFilterDTO> _branchDL) : base(_branchDL)
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

}
