using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class FilterPatientDto
    {
        //public string StartDate { get; set; }
        //public string EndDate { get; set; }
        public string FileNo { get; set; }
        public long HisNo { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public int SexID { get; set; }
        public int PageNumber { get; set; }
    }
}
