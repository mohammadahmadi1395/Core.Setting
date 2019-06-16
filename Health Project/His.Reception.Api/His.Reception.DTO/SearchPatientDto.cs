using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class SearchPatientDto
    {
        public string NationalCode { get; set; }
        public string FileNo { get; set; }
        public long HisNo { get; set; }
        public int InternalId { get; set; }
        public string FullName { get; set; }
    }
}
