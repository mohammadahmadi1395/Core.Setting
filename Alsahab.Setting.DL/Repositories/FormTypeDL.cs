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
    public class FormTypeDL : BaseDL<FormType, FormTypeDTO, FormTypeFilterDTO>, IScopedDependency
    {
        public FormTypeDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<IList<FormTypeDTO>> GetAsync(FormTypeFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
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
                return await query.ProjectTo<FormTypeDTO>().ToListAsync(cancellationToken);
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

            if (filter?.RequestTypelist?.Count > 0)
            {
                List<int?> temp = new List<int?>();
                foreach (var val in filter?.RequestTypelist)
                    temp.Add((int?)val);
                query = query?.Where(s => s.EnumID != null && (bool)temp.Contains(s.EnumID));
            }
            if (filter.ID > 0)
                query = query.Where(s => s.ID == filter.ID);

            if (filter.SubSystemID > 0)
                query = query.Where(s => s.SubSystemID == filter.SubSystemID);

            if (!String.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.Contains(filter.Title));

            if (!String.IsNullOrWhiteSpace(filter.Enum.ToString()))
                query = query.Where(s => s.EnumID == (int)filter.Enum);

            if (!String.IsNullOrWhiteSpace(filter.PublicCode))
                query = query.Where(s => s.PublicCode.Contains(filter.PublicCode));

            if (!String.IsNullOrWhiteSpace(filter.Coment))
                query = query.Where(s => s.Comment.Contains(filter.Coment));

            if (!string.IsNullOrWhiteSpace(filter.CreateDate.ToString()))
                query = query.Where(s => s.CreateDate.ToString().Contains(filter.CreateDate.ToString()));


            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            IList<FormTypeDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<FormTypeDTO>().ToListAsync(cancellationToken);
            return result;
        }

    }
}
