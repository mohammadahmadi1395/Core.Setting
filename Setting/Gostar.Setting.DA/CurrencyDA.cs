using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;

namespace Gostar.Setting.DA
{

    public class CurrencyDA : DataAccess
    {
        public List<CurrencyDTO> CurrencyGet(CurrencyDTO data, CurrencyFilterDTO filter)
        {
            var result = new List<CurrencyDTO>();

            UseContext(database =>
            {
                var query = database.Currency.Where(t => true);
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
                    if (!String.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));
                    if (!String.IsNullOrWhiteSpace(data.Symbol))
                        query = query.Where(s => s.Symbol.Contains(data.Symbol));
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else query = query.Where(s => s.IsDeleted == false);
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }

        public CurrencyDTO CurrencyInsert(CurrencyDTO data)
        {
            Currency Currency = null;
            Currency = Mapper.Map(data);
            UseContext(c =>
            {
                c.Currency.Add(Currency);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Currency.ID;
            return data;
        }

        public List<CurrencyDTO> CurrencyInsert(List<CurrencyDTO> data)
        {

            List<Currency> Currencylist = null;
            Currencylist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Currency.AddRange(Currencylist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;
        }


        public CurrencyDTO CurrencyUpdate(CurrencyDTO data)
        {
            CurrencyDTO CurrencyDto = new CurrencyDTO();
            if (data.ID > 0)
            {
                CurrencyDto = CurrencyGet(new CurrencyDTO { ID = data.ID }, null)?.FirstOrDefault();
                CurrencyDto = new CurrencyDTO
                {
                    ID = data.ID,
                    Title = !String.IsNullOrWhiteSpace(data.Title) ? data.Title : CurrencyDto.Title,
                    Symbol = !String.IsNullOrWhiteSpace(data.Symbol) ? data.Symbol : CurrencyDto.Symbol,
                    IsDeleted = data.IsDeleted,
                    CreateDate = CurrencyDto.CreateDate
                };
            }
            Currency Currency = Mapper.Map(CurrencyDto);
            UseContext(databsse =>
            {
                databsse.Entry(Currency).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return CurrencyDto;
        }


        public CurrencyDTO CurrencyDelete(CurrencyDTO data)
        {
            CurrencyDTO CurrencyDto = new CurrencyDTO();
            if (data.ID > 0)
            {
                CurrencyDto = CurrencyGet(new CurrencyDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            CurrencyDto.IsDeleted = true;

            return CurrencyUpdate(CurrencyDto);
        }


    }
}
