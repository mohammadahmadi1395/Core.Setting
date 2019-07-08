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
    public class BranchAddressDL : BaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO>, IScopedDependency
    {
        public BranchAddressDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<BranchAddressDTO>> GetAsync(BranchAddressFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<BranchAddressDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter?.FromStartDate > DateTime.MinValue)
                query = query.Where(s => s.StartDate >= filter.FromStartDate);
            if (filter?.ToStartDate > DateTime.MinValue)
                query = query.Where(s => s.StartDate <= filter.ToStartDate);
            if (filter?.FromEndDate > DateTime.MinValue)
                query = query.Where(s => s.EndDate >= filter.FromEndDate);
            if (filter?.ToEndDate > DateTime.MinValue)
                query = query.Where(s => s.EndDate <= filter.ToEndDate);
            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);
            if (filter?.CreateDateTo > DateTime.MinValue)
                query = query.Where(s => s.CreateDate <= filter.CreateDateTo);
            if (filter?.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (filter?.StartDate > DateTime.MinValue)
                query = query.Where(s => s.StartDate.ToString().Contains(filter.StartDate.ToString()));
            if (filter?.EndDate > DateTime.MinValue)
                query = query.Where(s => s.EndDate.ToString().Contains(filter.EndDate.ToString()));
            if (filter?.CreateDate > DateTime.MinValue)
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));
            if (filter?.ZoneID > 0)
                query = query.Where(s => s.ZoneID == filter.ZoneID);
            if (!String.IsNullOrWhiteSpace(filter?.Address))
                query = query.Where(s => s.Address.Contains(filter.Address));


            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<BranchAddressDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.Where(s => s.IsDeleted == false).ProjectTo<BranchAddressDTO>().ToListAsync(cancellationToken);
            return result;
        }

    }
}
