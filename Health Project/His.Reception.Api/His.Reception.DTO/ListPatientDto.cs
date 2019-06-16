using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class ListPatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public long InternalId { get; set; }
        public long? HisNo { get; set; }
        public string CreateDate { get; set; }
        public string FileNo { get; set; }
    }
}
