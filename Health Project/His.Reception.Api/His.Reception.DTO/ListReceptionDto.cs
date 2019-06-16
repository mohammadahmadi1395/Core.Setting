using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class ListReceptionDto
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string FullName { get; set; }
        public string ReferDate { get; set; }
        public long ReceptionId { get; set; }
        public string NationalCode { get; set; }
    }
}
