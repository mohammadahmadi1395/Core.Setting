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

namespace Alsahab.Setting.Data.Repositories
{
    public class PrefixDL : BaseDL<Prefix, PrefixDTO, PrefixFilterDTO>, IScopedDependency
    {
        public PrefixDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async override Task<IList<PrefixDTO>> GetAsync(PrefixFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<PrefixDTO>().ToListAsync(cancellationToken);
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
            if (!String.IsNullOrWhiteSpace(filter?.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));
            if (filter.IsDefault.HasValue)
                query = query.Where(s => s.IsDefault == filter.IsDefault);
            if (filter.CreateDate > DateTime.MinValue)
                query = query.Where(s => s.CreateDate == filter.CreateDate);
            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<PrefixDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.Where(s => s.IsDeleted == false).ProjectTo<PrefixDTO>().ToListAsync(cancellationToken);
            return result;
        }
    }

}
