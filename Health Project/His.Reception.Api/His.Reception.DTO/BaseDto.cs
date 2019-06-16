using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class BaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool IsAdmin { get; set; }
    }
}

