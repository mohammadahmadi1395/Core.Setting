using System;
using System.Collections.Generic;
using System.Linq;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.BL.Validation;
using Alsahab.Common.Exceptions;
using Alsahab.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Alsahab.Setting.BL
{
    public class ZoneBL : BaseBL<Zone, ZoneDTO, ZoneFilterDTO>
    {
        #region dependency injection
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public ZoneBL(IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL,
                    IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(zoneDL, logDL)
        {
            _ZoneDL = zoneDL;
        }
        #endregion dependency injection

        public async override Task<IList<ZoneDTO>> GetAsync(ZoneFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _ZoneDL.GetAsync(filter, cancellationToken, paging);
            foreach (var val in response)
            {
                var parentList = new List<string>();
                var thisItem = val;
                parentList.Add(val.Title);
                while (thisItem.ParentID != null)
                {
                    var parent = AllDtos.FirstOrDefault(s => s.ID == thisItem.ParentID);
                    parentList.Add(parent.Title);
                    thisItem = parent;
                }
                val.ZoneAddress = String.Join("-", parentList);
                //TODO:
                // val.ZoneAndChilds = GetZoneChilds(val.ID ?? 0);
                // val.ZoneAndParents = GetZoneParents(val.ID ?? 0);
            }

            ResultCount = _ZoneDL.ResultCount;
            return response;
        }
    }
}
