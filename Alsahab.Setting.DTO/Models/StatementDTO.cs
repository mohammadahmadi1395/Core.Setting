using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Alsahab.Common;
using System.Runtime.Serialization;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class StatementDTO : BaseDTO
    {
        public string TagName { get; set; }
        public List<SubsystemDTO> SubsystemList { get; set; }
        public long? FilterSubsystemID { get; set; }
        //public long SubsystemID { get; set; }
        //public string SubsystemName { get; set; }
        //public Enums.StatementTypes TypeID { get; set; }
        //public string TypeTitle { get { return ((Enums.StatementTypes)TypeID).GetDescription(); } } //Enums.StatementTypes TypeTitle { get; set; }
        public List<long?> SubsystemIDList { get; set; }
        public string SubsystemNameList { get; set; }
        public string PersianText { get; set; }
        public string EnglishText { get; set; }
        public string ArabicText { get; set; }
        public List<long?> IDList { get; set; }
    }

    public class StatementValidator : AbstractValidator<StatementDTO>
    {
        public StatementValidator()
        {
            RuleFor(x => x.TagName).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.SubsystemList).NotEmpty();
            RuleFor(x => x.ArabicText).NotEmpty();
            RuleFor(x => x.PersianText).NotEmpty();
            RuleFor(x => x.EnglishText).NotEmpty();
        }
    }
}