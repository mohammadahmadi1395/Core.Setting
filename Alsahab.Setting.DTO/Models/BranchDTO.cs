﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class BranchDTO : BaseDTO//<BranchDTO, Branch, long>
    {
        public BranchDTO()
        {            
        }
        public long? ParentId { get; set; }
        public String Title { get; set; }
        public String Code { get; set; }
        public long? HeadPersonID { get; set; }
        public String HeadMemberName { get; set; }
        public String HeadMemberPhoneNo { get; set; }                
        public String BranchPhoneNo { get; set; }
        public String BranchEmail { get; set; }
        public long? BranchAddressID { get; set; }
        public String BranchComment { get; set; }
        public bool? IsCentral { get; set; }
        public Nullable<long> LeftIndex { get; set; }
        public Nullable<long> RightIndex { get; set; }
        public Nullable<long> Depth { get; set; }
        public string OldCode { get; set; }
    }

    public class BranchValidator : AbstractValidator<BranchDTO>
    {
        public BranchValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(s => s.BranchAddressID).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}