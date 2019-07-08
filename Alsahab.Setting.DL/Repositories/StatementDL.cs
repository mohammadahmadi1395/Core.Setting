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
    public class StatementDL : BaseDL<Statement, StatementDTO, StatementFilterDTO>, IScopedDependency
    {
        private readonly ApplicationDbContext _DbContext;
        public StatementDL(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public async override Task<IList<StatementDTO>> GetAsync(StatementFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();

            if (filter == null)
            {
                query = query.Where(s=>s.IsDeleted == false);
                ResultCount = await query.CountAsync(cancellationToken);
                if (paging?.IsPaging == true)
                {
                    int skip = (paging.Index - 1) * paging.Size;
                    query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                }
                return await query.ProjectTo<StatementDTO>().ToListAsync(cancellationToken);
            }


            if (filter?.IDList?.Count > 0)
                query = query.Where(s => filter.IDList.Contains(s.ID));

            if (filter.ID > 0)
                query = query.Where(t => t.ID == filter.ID);

            // if (filter.CreateDate > DateTime.MinValue)
            //     query = query.Where(t => t.CreateDate.Value.Date == filter.CreateDate.Value.Date);

            if (filter.IsDeleted.HasValue == true)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted);
            else
                query = query.Where(s => s.IsDeleted == false);

            if (filter?.CreateDateFrom > DateTime.MinValue)
                query = query.Where(t => t.CreateDate >= filter.CreateDateFrom);

            if (filter?.CreateDateTo > DateTime.MinValue)
                query = query.Where(t => t.CreateDate <= (filter.CreateDateTo == filter.CreateDateTo.Value.Date ? filter.CreateDateTo.Value.AddDays(1).AddTicks(-1) : filter.CreateDateTo));

            //if (!string.IsNullOrWhiteSpace(filter?.SubsystemName))
            //    query = query.Where(s => s.Subsystem.Name == filter.SubsystemName);

            //if (filter?.FilterSubsystemID > 0)
            //    query = query.Where(t => t.ss.SubsystemID == filter.FilterSubsystemID); // sub SubSystemID == filter.SubsystemID);

            if (!string.IsNullOrWhiteSpace(filter.ArabicText))
                query = query.Where(t => t.ArabicText.Contains(filter.ArabicText));

            if (!string.IsNullOrWhiteSpace(filter.PersianText))
                query = query.Where(t => t.PersianText.Contains(filter.PersianText));

            if (!string.IsNullOrWhiteSpace(filter.EnglishText))
                query = query.Where(t => t.EnglishText.Contains(filter.EnglishText));

            if (!string.IsNullOrWhiteSpace(filter.TagName))
                query = query.Where(t => t.TagName.Contains(filter.TagName));

            if (filter?.IDList?.Count > 0)
                query = query.Where(t => filter.IDList.Contains(t.ID));

            //if (filter.TypeID > 0)
            //    query = query.Where(t => t.s.TypeID == (int)filter.TypeID);

            //if ((int)filter.TypeTitle > 0)
            //    query = query.Where(s => s.TypeID == (int)filter.TypeTitle);

            IList<StatementDTO> result;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            result = await query.ProjectTo<StatementDTO>().ToListAsync(cancellationToken);
            return result;
        }
    }
}
