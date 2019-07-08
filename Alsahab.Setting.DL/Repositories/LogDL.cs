using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Common.Utilities;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.DL.Repositories
{
    public class LogDL : BaseDL<Log, LogDTO, LogFilterDTO>, IScopedDependency
    {
        public LogDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<LogDTO>> GetAsync(LogFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;

            if (filter == null)
            {
                query = query.Where(s => s.IsDeleted == false);
                ResultCount = await query.CountAsync(cancellationToken);
                if (paging?.IsPaging == true)
                {
                    int skip = (paging.Index - 1) * paging.Size;
                    query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                }
                return await query.ProjectTo<LogDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));


            if (filter?.UserIDS?.Count > 0)
                query = query.Where(p => filter.UserIDS.Contains(p.UserID));

            if (filter?.EntityIDs?.Count > 0)
                query = query.Where(p => filter.EntityIDs.Contains(p.EntityID));

            if (filter?.ActionTypeIDs?.Count > 0)
                query = query.Where(p => filter.ActionTypeIDs.Contains(p.ActionTypeID));

            if (filter.FromDate > DateTime.MinValue)
            {
                var fsd = filter.FromDate;//.Date;
                query = query.Where(p => p.CreateDate >= fsd);
            }

            if (filter.ToDate > DateTime.MinValue)
            {
                var tsd = ((DateTime)filter.ToDate).Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.CreateDate <= tsd);
            }

            IList<LogDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<LogDTO>().ToListAsync(cancellationToken);
            return result;
        }

    }
}
