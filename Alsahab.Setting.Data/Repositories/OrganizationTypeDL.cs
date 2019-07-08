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
    public class OrganizationTypeDL : BaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO>, IScopedDependency
    {
        public OrganizationTypeDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<OrganizationTypeDTO>> GetAsync(OrganizationTypeFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<OrganizationTypeDTO>().ToListAsync(cancellationToken);
            }


            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));

            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));

            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<OrganizationTypeDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.Where(s => s.IsDeleted == false).ProjectTo<OrganizationTypeDTO>().ToListAsync(cancellationToken);
            return result;
        }

    }
}
