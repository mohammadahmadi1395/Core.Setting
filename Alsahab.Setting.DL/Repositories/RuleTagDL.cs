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
    public class RuleTagDL : BaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO>, IScopedDependency
    {
        public RuleTagDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<RuleTagDTO>> GetAsync(RuleTagFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<RuleTagDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);
            if (filter?.CreateDateTo > DateTime.MinValue)
            {
                var value = (filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo);
                query = query.Where(s => s.CreateDate <= value);
            }

            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (filter.RuleID > 0)
                query = query.Where(s => s.RuleID == filter.RuleID);
            if (filter.FormTypeID > 0)
                query = query.Where(s => s.FormTypeID == filter.FormTypeID);
            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<RuleTagDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<RuleTagDTO>().ToListAsync(cancellationToken);
            return result;
        }
    }
}
