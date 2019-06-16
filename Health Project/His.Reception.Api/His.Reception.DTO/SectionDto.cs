using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string No { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? SuperVisorPersonelId { get; set; }
    }
}
