using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;
namespace Gostar.Setting.DA
{
    public class CityDA : DataAccess
    {
        public List<CityDTO> CityGet(CityDTO data, CityFilterDTO filter)
        {
            var result = new List<CityDTO>();

            UseContext(database =>
            {
                var query = database.City.Where(t => true);
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
                    if (!string.IsNullOrWhiteSpace(data.Name))
                        query = query.Where(s => s.Name.Contains(data.Name));
                    if (data.CountryID > 0)
                        query = query.Where(s => s.CountryID == data.CountryID);
                    if (data.Code > 0)
                        query = query.Where(s => s.Code == data.Code);
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
        public CityDTO CityInsert(CityDTO data)
        {
            City city = null;
            city = Mapper.Map(data);
            UseContext(c =>
            {
                c.City.Add(city);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = city.ID;
            return data;
        }
        public List<CityDTO> CityInsert(List<CityDTO> data)
        {

            List<City> citylist = null;
            citylist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.City.AddRange(citylist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public CityDTO CityUpdate(CityDTO data)
        {
            CityDTO cityDto = new CityDTO();
            if (data.ID > 0)
            {
                cityDto = CityGet(new CityDTO { ID = data.ID }, null)?.FirstOrDefault();
                cityDto = new CityDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : cityDto.Name,
                    CountryID = data?.CountryID > 0 ? data?.CountryID : cityDto.CountryID,
                    Code = data?.Code > 0 ? data?.Code : cityDto.Code,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : cityDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            City city = Mapper.Map(cityDto);
            UseContext(databsse =>
            {
                databsse.Entry(city).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return cityDto;
        }
        public CityDTO CityDelete(CityDTO data)
        {
            CityDTO cityDto = new CityDTO();
            if (data.ID > 0)
            {
                cityDto = CityGet(new CityDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            City city = Mapper.Map(cityDto);
            UseContext(databsse =>
            {
                databsse.Entry(city).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return cityDto;
        }
    }
}
