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
    public class CountryDA : DataAccess
    {
        public List<CountryDTO> CountryGet(CountryDTO data, CountryFilterDTO filter, PagingInfoDTO paging = null)
        {
            var result = new List<CountryDTO>();
            UseContext(database =>
            {
                var query = database.Country.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromPhoneCode > 0)
                        query = query.Where(s => s.PhoneCode >= filter.FromPhoneCode);
                    if (filter?.ToPhoneCode > 0)
                        query = query.Where(s => s.PhoneCode <= filter.ToPhoneCode);
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1):filter.ToCreateDate));
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (!string.IsNullOrWhiteSpace(data.Name))
                        query = query.Where(s => s.Name.Contains(data.Name));
                    if (data.PhoneCode > 0)
                        query = query.Where(s => s.PhoneCode == data.PhoneCode);
                    if (!string.IsNullOrWhiteSpace(data.ShortName))
                        query = query.Where(s => s.ShortName.Contains(data.ShortName));
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));

                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else
                query = query.Where(s => s.IsDeleted == false);
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

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public CountryDTO CountryInsert(CountryDTO data)
        {
            Country country= null;
            country = Mapper.Map(data);
            UseContext(c =>
            {
                c.Country.Add(country);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = country.ID;
            return data;
        }
        public List<CountryDTO> CountryInsert(List<CountryDTO> data)
        {

            List<Country> countrylist = null;
            countrylist= data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Country.AddRange(countrylist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }

        public CountryDTO CountryUpdate(CountryDTO data)
        {
            CountryDTO countryDto= new CountryDTO();
            if (data.ID > 0)
            {
                countryDto = CountryGet(new CountryDTO{ ID = data.ID },null)?.FirstOrDefault();
                countryDto = new CountryDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : countryDto.Name,
                    PhoneCode = data?.PhoneCode > 0 ? data?.PhoneCode : countryDto.PhoneCode,
                    ShortName = !string.IsNullOrWhiteSpace(data?.ShortName) ? data?.ShortName : countryDto.ShortName,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : countryDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            Country country = Mapper.Map(countryDto);
            UseContext(databsse =>
            {
                databsse.Entry(country).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return countryDto;
        }
        public CountryDTO CountryDelete(CountryDTO data)
        {
            CountryDTO countryDto= new CountryDTO();
            if (data.ID > 0)
            {
                countryDto= CountryGet(new CountryDTO{ ID = data.ID },null)?.SingleOrDefault();
            }
            Country country= Mapper.Map(countryDto);
            UseContext(databsse =>
            {
                databsse.Entry(country).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return countryDto;
        }
    }
}
