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
    public class SubsystemDL : BaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO>, IScopedDependency
    {
        public SubsystemDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<SubsystemDTO>> GetAsync(SubsystemFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<SubsystemDTO>().ToListAsync(cancellationToken);
            }


            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(s => s.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(s => s.Description.Contains(filter.Description));

            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            if (filter.IsActive.HasValue)
                query = query.Where(s => s.IsActive == filter.IsActive);
            else
                query = query.Where(s => s.IsActive == true);

            if (filter.IsSystem.HasValue)
                query = query.Where(s => s.IsSystem == filter.IsSystem);

            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);

            if (filter?.CreateDateTo > DateTime.MinValue)
            {
                var value = (filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo);
                query = query.Where(s => s.CreateDate <= value);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter?.RunOrder > 0)
                query = query.Where(s => s.RunOrder == filter.RunOrder);

            if (filter.IsPart.HasValue)
                query = query.Where(s => s.IsPart == filter.IsPart);

            IList<SubsystemDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<SubsystemDTO>().ToListAsync(cancellationToken);
            return result;
        }
    }
}
