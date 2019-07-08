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
    public class RuleDL : BaseDL<Rule, RuleDTO, RuleFilterDTO>, IScopedDependency
    {
        public RuleDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async override Task<IList<RuleDTO>> GetAsync(RuleFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<RuleDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);
            if (filter?.CreateDateTo > DateTime.MinValue)
                query = query.Where(s => s.CreateDate <= (filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo));
            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (!String.IsNullOrWhiteSpace(filter.Type.ToString()))
                query = query.Where(s => s.Type == (int)filter.Type);
            if (!String.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));
            if (!String.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(s => s.Description.Contains(filter.Description));
            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));
            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<RuleDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<RuleDTO>().ToListAsync(cancellationToken);
            return result;
        }
    }
}
