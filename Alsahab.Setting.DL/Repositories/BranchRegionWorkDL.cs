using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Common.Utilities;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.DL.Repositories
{
    public class BranchRegionWorkDL : BaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO>, IScopedDependency
    {
        public BranchRegionWorkDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<BranchRegionWorkDTO>> GetAsync(BranchRegionWorkFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;

            if (filter == null)
            {
                                query = query.Where(s=>s.IsDeleted == false);
                ResultCount = await query.CountAsync(cancellationToken);
                if (paging?.IsPaging == true)
                {
                    int skip = (paging.Index - 1) * paging.Size;
                    query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                }
                return await query.ProjectTo<BranchRegionWorkDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (filter.ZoneID > 0)
                query = query.Where(s => s.ZoneID == filter.ZoneID);
            if (filter.BranchID > 0)
                query = query.Where(s => s.BranchID == filter.BranchID);
            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<BranchRegionWorkDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<BranchRegionWorkDTO>().ToListAsync(cancellationToken);
            return result;
        }

    }
}
