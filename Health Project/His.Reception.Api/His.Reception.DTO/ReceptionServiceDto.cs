using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class ReceptionServiceDto
    {
        public int ServiceId { get; set; }
        public long ReceptoinId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
