using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public  class FilterReceptionDto
    {
        public string FileNo { get; set; }
        public long HisNo { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public int SexID { get; set; }
        public int PageNumber { get; set; }
        public long ReceptionId { get; set; }
        public long InternalId{ get; set; }
        public long DoctorId{ get; set; }
        public bool IsToday { get; set; }

        public string ReceptionStartDate { get; set; }
        public string ReceptionEndDate { get; set; }
    }
}
