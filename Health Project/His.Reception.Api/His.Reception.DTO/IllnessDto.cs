using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class IllnessDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Icd9 { get; set; }
        public string Icd10 { get; set; }
        public bool? IsSpecial { get; set; }
        public string Code { get; set; }
        public bool? IsAdmin { get; set; }

    }
}
