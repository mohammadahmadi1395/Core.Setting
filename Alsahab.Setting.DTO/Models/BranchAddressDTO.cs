using System;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class BranchAddressDTO : BaseDTO
    {
        /// <summary>
        /// Just For Get
        /// </summary>
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ZoneID { get; set; }
        public String Address { get; set; }
        public string FullAddress
        {
            get
            {
                return ZoneDTO?.ZoneAddress + " " + Address;
            }
        }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ZoneDTO ZoneDTO { get; set; }

    }

    public class BranchAddressValidator : AbstractValidator<BranchAddressDTO>
    {
        public BranchAddressValidator()
        {
            //RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }

}