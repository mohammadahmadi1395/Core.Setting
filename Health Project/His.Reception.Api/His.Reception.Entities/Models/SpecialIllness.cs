using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class SpecialIllness
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
    }
}
