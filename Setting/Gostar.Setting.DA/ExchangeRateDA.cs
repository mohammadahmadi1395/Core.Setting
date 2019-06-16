using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;
using System.Globalization;

namespace Gostar.Setting.DA
{

    public class ExchangeRateDA : DataAccess
    {
       
        public List<ExchangeRateDTO> ExchangeRateGet(ExchangeRateDTO data, ExchangeRateFilterDTO filter)
        {
        
            var result = new List<ExchangeRateDTO>();

            UseContext(database =>
            {
                var query = database.ExchangeRate.Where(t => true);
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
                    if (data.FromCurrencyID>0)
                        query = query.Where(s => s.FromCurrencyID == data.FromCurrencyID);
                    if (data.ToCurrencyID>0)
                        query = query.Where(s => s.ToCurrencyID == data.ToCurrencyID);
                    if (data.Year > DateTime.MinValue)
                        query = query.Where(s => s.Year.Year == data.Year.Value.Year);
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue)
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

        public ExchangeRateDTO ExchangeRateInsert(ExchangeRateDTO data)
        {
            ExchangeRate ExchangeRate = null;
            ExchangeRate = Mapper.Map(data);
            UseContext(c =>
            {
                c.ExchangeRate.Add(ExchangeRate);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = ExchangeRate.ID;
            return data;
        }

        public List<ExchangeRateDTO> ExchangeRateInsert(List<ExchangeRateDTO> data)
        {

            List<ExchangeRate> ExchangeRatelist = null;
            ExchangeRatelist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.ExchangeRate.AddRange(ExchangeRatelist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;
        }

        public ExchangeRateDTO ExchangeRateUpdate(ExchangeRateDTO data)
        {
            ExchangeRateDTO ExchangeRateDto = new ExchangeRateDTO();
            if (data.ID > 0)
            {
                ExchangeRateDto = ExchangeRateGet(new ExchangeRateDTO { ID = data.ID }, null)?.FirstOrDefault();
                ExchangeRateDto = new ExchangeRateDTO
                {
                    ID = data.ID,
                    FromCurrencyID=!String.IsNullOrWhiteSpace(data.FromCurrencyID.ToString())?data.FromCurrencyID:ExchangeRateDto.FromCurrencyID,
                    ToCurrencyID=!String.IsNullOrWhiteSpace(data.ToCurrencyID.ToString())?data.ToCurrencyID:ExchangeRateDto.ToCurrencyID,
                    Ratio=!String.IsNullOrWhiteSpace(data.Ratio.ToString())?data?.Ratio:ExchangeRateDto.Ratio,
                    Year=data.Year>DateTime.MinValue ? data.Year:ExchangeRateDto.Year,
                    IsDeleted = data.IsDeleted,
                    CreateDate = ExchangeRateDto.CreateDate
                };
            }
            ExchangeRate ExchangeRate = Mapper.Map(ExchangeRateDto);
            UseContext(databsse =>
            {
                databsse.Entry(ExchangeRate).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return ExchangeRateDto;
        }


        public ExchangeRateDTO ExchangeRateDelete(ExchangeRateDTO data)
        {
            ExchangeRateDTO ExchangeRateDto = new ExchangeRateDTO();
            if (data.ID > 0)
            {
                ExchangeRateDto = ExchangeRateGet(new ExchangeRateDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            ExchangeRateDto.IsDeleted = true;

            return ExchangeRateUpdate(ExchangeRateDto);
        }



    }
}
