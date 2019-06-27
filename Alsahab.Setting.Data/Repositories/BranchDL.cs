using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data.Repositories
{
    public class BranchDL : BaseDL<Branch, BranchDTO, BranchFilterDTO>, IScopedDependency// , IBaseDL<Branch, BranchDTO>,
    {
        public BranchDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<BranchDTO>> GetAsync(BranchFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            if (filter == null)
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.Successful;
                ErrorMessage = "";
                return await query.ProjectTo<BranchDTO>().ToListAsync(cancellationToken);
            }

            if (filter?.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (filter?.ParentId > 0)
                query = query.Where(s => s.ParentId == filter.ParentId);
            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);
            if (filter?.CreateDateTo > DateTime.MinValue)
            {
                var date = filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo;
                query = query.Where(s => s.CreateDate <= date);
            }
            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));
            if (filter.IsLeafNode)
                query = query.Where(s => s.RightIndex == s.LeftIndex + 1);
            if (!string.IsNullOrWhiteSpace(filter.Code))
                query = query.Where(s => s.Code.Equals(filter.Code));
            if (!string.IsNullOrWhiteSpace(filter.OldCode))
                query = query.Where(s => s.OldCode.Equals(filter.OldCode));
            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));
            if (filter.HeadPersonID > 0)
                query = query.Where(s => s.HeadPersonId == filter.HeadPersonID);
            if (!string.IsNullOrWhiteSpace(filter.BranchPhoneNo))
                query = query.Where(s => s.BranchPhoneNo.Contains(filter.BranchPhoneNo));
            if (!string.IsNullOrWhiteSpace(filter.BranchEmail))
                query = query.Where(s => s.BranchEmail.Contains(filter.BranchEmail));
            if (!string.IsNullOrWhiteSpace(filter.BranchAddressID.ToString()))
                query = query.Where(s => s.BranchAddressId == filter.BranchAddressID);
            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));
            if (filter.IsCentral == true)
                query = query.Where(s => s.IsCentral == true);
            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }

            ResponseStatus = Alsahab.Common.ResponseStatus.Successful;
            ErrorMessage = "";
            return await query.ProjectTo<BranchDTO>().ToListAsync(cancellationToken);
        }

        public override IList<BranchDTO> Get(BranchFilterDTO filter, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            if (filter == null)
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.Successful;
                ErrorMessage = "";
                return query.ProjectTo<BranchDTO>().ToList();
            }

            if (filter?.ID > 0)
                query = query.Where(s => s.ID == filter.ID);
            if (filter?.ParentId > 0)
                query = query.Where(s => s.ParentId == filter.ParentId);
            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(s => s.CreateDate >= filter.CreateDateFrom);
            if (filter?.CreateDateTo > DateTime.MinValue)
            {
                var date = filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo;
                query = query.Where(s => s.CreateDate <= date);
            }
            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));
            if (filter.IsLeafNode)
                query = query.Where(s => s.RightIndex == s.LeftIndex + 1);
            if (!string.IsNullOrWhiteSpace(filter.Code))
                query = query.Where(s => s.Code.Equals(filter.Code));
            if (!string.IsNullOrWhiteSpace(filter.OldCode))
                query = query.Where(s => s.OldCode.Equals(filter.OldCode));
            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));
            if (filter.HeadPersonID > 0)
                query = query.Where(s => s.HeadPersonId == filter.HeadPersonID);
            if (!string.IsNullOrWhiteSpace(filter.BranchPhoneNo))
                query = query.Where(s => s.BranchPhoneNo.Contains(filter.BranchPhoneNo));
            if (!string.IsNullOrWhiteSpace(filter.BranchEmail))
                query = query.Where(s => s.BranchEmail.Contains(filter.BranchEmail));
            if (!string.IsNullOrWhiteSpace(filter.BranchAddressID.ToString()))
                query = query.Where(s => s.BranchAddressId == filter.BranchAddressID);
            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));
            if (filter.IsCentral == true)
                query = query.Where(s => s.IsCentral == true);
            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }

            ResponseStatus = Alsahab.Common.ResponseStatus.Successful;
            ErrorMessage = "";
            return query.ProjectTo<BranchDTO>().ToList();
        }

    }
}
