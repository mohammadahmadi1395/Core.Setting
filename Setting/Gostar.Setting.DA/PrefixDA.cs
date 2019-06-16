using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class PrefixDA : DataAccess
    {

        public List<PrefixDTO> GetPrefixs(PrefixDTO data, PrefixFilterDTO filter, PagingInfoDTO paging = null)
        {
            var res = new List<PrefixDTO>();

            UseContext(database =>
            {
                var query = database.Prefix.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date ? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1) : filter.ToCreateDate));
                }

                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (!String.IsNullOrWhiteSpace(data?.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));
                    if (data.IsDefault.HasValue)
                        query = query.Where(s => s.IsDefault == data.IsDefault);
                    if (data.CreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate == data.CreateDate);
                    if (data.IsDeleted.HasValue)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);

                }
                ResultCount = query.Count();

                if (paging != null)
                {
                    if (paging.IsPaging)
                    {
                        int skip = (paging.Index - 1) * paging.Size;
                        query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                    }
                }
                #endregion
                res = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return res;
        }

        public PrefixDTO PrefixInsert(PrefixDTO data)
        {
            Prefix Prefix = null;
            Prefix = Mapper.Map(data);
            UseContext(c =>
            {
                c.Prefix.Add(Prefix);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Prefix.ID;
            return data;
        }
        public List<PrefixDTO> PrefixInsert(List<PrefixDTO> data)
        {

            List<Prefix> Prefixlist = null;
            Prefixlist = data.Select(s => Mapper.Map(s)).ToList();
            UseContext(c =>
            {
                c.Prefix.AddRange(Prefixlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }

        public PrefixDTO PrefixUpdate(PrefixDTO data)
        {
            PrefixDTO Prefixdto = new PrefixDTO();
            if (data.ID > 0)
            {
                Prefixdto = GetPrefixs(new PrefixDTO { ID = data.ID }, null)?.FirstOrDefault();
                Prefixdto = new PrefixDTO
                {
                    ID = data.ID,
                    Title = !String.IsNullOrWhiteSpace(data?.Title) ? data.Title : Prefixdto?.Title,
                    IsDeleted = data.IsDeleted,
                    CreateDate = Prefixdto?.CreateDate,
                    IsDefault = data.IsDefault.HasValue ? data?.IsDefault : Prefixdto.IsDefault
                };
            }
            Prefix Prefix = Mapper.Map(Prefixdto);
            UseContext(databsse =>
            {
                databsse.Entry(Prefix).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return Prefixdto;
        }

        public PrefixDTO PrefixDelete(PrefixDTO data)
        {

            PrefixDTO PrefixDto = new PrefixDTO();
            if (data.ID > 0)
            {
                PrefixDto = GetPrefixs(new PrefixDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            PrefixDto.IsDeleted = true;

            return PrefixUpdate(PrefixDto);
        }


    }
}
