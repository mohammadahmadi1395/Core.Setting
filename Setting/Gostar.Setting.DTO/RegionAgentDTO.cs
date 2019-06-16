using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class RegionAgentDTO : BaseDTO
    {
        public long? AgentPersonID { get; set; }
        public string AgentFullName { get { return AgentName + " " + AgentFatherName + " " + AgentGrandFatherName; } }
        public string AgentName { get; set; }
        public string AgentFatherName { get; set; }
        public string AgentGrandFatherName { get; set; }
        public string AgentMobile { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public long? CountryID { get; set; }
        public string CountryName { get; set; }
        public long? CityID { get; set; }
        public string CityName { get; set; }
        public long? AreaID { get; set; }
        public string AreaName { get; set; }
        public long? RegionID { get; set; }
        public string RegionName { get; set; }
        public string CityAreaRegionCode { get; set; }
        //        public string RegionCode { get; set; }
    }
}
