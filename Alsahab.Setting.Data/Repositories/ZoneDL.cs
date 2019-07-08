using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Common.Utilities;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data.Repositories
{
    public class ZoneDL : BaseDL<Zone, ZoneDTO, ZoneFilterDTO>, IScopedDependency
    {
        public ZoneDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public override async Task<IList<ZoneDTO>> GetAsync(ZoneFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();
            if (filter == null)
                return await query.Where(s=>s.IsDeleted == false).ProjectTo<ZoneDTO>().ToListAsync(cancellationToken);

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);

            if (filter?.CreateDateFrom > DateTime.MinValue)
            {
                var from = filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo;
                query = query.Where(s => s.CreateDate <= from);
            }

            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);

            if (!String.IsNullOrWhiteSpace(filter.Code))
                query = query.Where(s => s.Code.Contains(filter.Code));

            if (!String.IsNullOrWhiteSpace(filter.OldCode))
                query = query.Where(s => s.OldCode.Contains(filter.OldCode));

            if (filter.ParentID > 0)
                query = query.Where(s => s.ParentID == filter.ParentID);

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));

            if (!string.IsNullOrWhiteSpace(filter.Type?.ToString()))
                query = query.Where(s => s.Type == (int)filter.Type);

            if (!string.IsNullOrWhiteSpace(filter.Comment))
                query = query.Where(s => s.Comment.Contains(filter.Comment));

            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));

            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }

            return await query.ProjectTo<ZoneDTO>().ToListAsync(cancellationToken);
        }

}
}
