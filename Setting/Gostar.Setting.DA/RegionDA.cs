using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class RegionDA : DataAccess
    {
        public List<RegionDTO> RegionGet(RegionDTO data, RegionFilterDTO filter)
        {
            var result = new List<RegionDTO>();
            UseContext(database =>
            {
                var query = database.Region.Where(t => true);
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
                    if (data.CityID > 0)
                        query = query.Where(s => s.Area.City.ID == data.CityID);
                    if (data.CountryID > 0)
                        query = query.Where(s => s.Area.City.Country.ID == data.CountryID);
                    if (data.AreaID > 0)
                        query = query.Where(s => s.AreaID == data.AreaID);
                    if (data.Code > 0)
                        query = query.Where(s => s.Code == data.Code);
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else
                {
                    query = query.Where(s => s.IsDeleted == false);

                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public RegionDTO RegionInsert(RegionDTO data)
        {
            Region region = null;
            region = Mapper.Map(data);
            UseContext(c =>
            {
                c.Region.Add(region);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            data.ID = region.ID;
            return data;
        }
        public List<RegionDTO> RegionInsert(List<RegionDTO> data)
        {

            List<Region> regionlist = null;
            regionlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Region.AddRange(regionlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public RegionDTO RegionUpdate(RegionDTO data)
        {
            RegionDTO regionDto = new RegionDTO();
            if (data.ID > 0)
            {
                regionDto = RegionGet(new RegionDTO { ID = data.ID }, null)?.FirstOrDefault();
                regionDto = new RegionDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : regionDto.Name,
                    AreaID = data?.AreaID > 0 ? data?.AreaID : regionDto.AreaID,
                    Code = data?.Code > 0 ? data?.Code : regionDto.Code,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : regionDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            Region region = Mapper.Map(regionDto);
            UseContext(databsse =>
            {
                databsse.Entry(region).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return regionDto;
        }
        public RegionDTO RegionDelete(RegionDTO data)
        {
            RegionDTO regionDto = new RegionDTO();
            if (data.ID > 0)
            {
                regionDto = RegionGet(new RegionDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            Region region = Mapper.Map(regionDto);
            UseContext(databsse =>
            {
                databsse.Entry(region).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return regionDto;
        }
    }
}
