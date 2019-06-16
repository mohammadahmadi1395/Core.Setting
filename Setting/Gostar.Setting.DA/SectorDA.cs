using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class SectorDA : DataAccess
    {

        public List<SectorDTO> SectorGet(SectorDTO data, SectorFilterDTO filter)
        {
            var result = new List<SectorDTO>();
            UseContext(database =>
            {
                var query = database.Sector.Where(t => true);
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

                    if (data.Code > 0)
                        query = query.Where(s => s.Code == data.Code);

                    if (data.RegionID > 0)
                        query = query.Where(s => s.RegionID == data.RegionID);

                    if (!string.IsNullOrWhiteSpace(data.RegionName))
                        query = query.Where(s => s.Region.Name.Contains(data.RegionName));

                    if (data.AreaID > 0)
                        query = query.Where(s => s.Region.Area.ID == data.AreaID);

                    if (!string.IsNullOrWhiteSpace(data.AreaName))
                        query = query.Where(s => s.Region.Area.Name.Contains(data.AreaName));

                    if (data.CityID > 0)
                        query = query.Where(s => s.Region.Area.City.ID == data.CityID);

                    if (!string.IsNullOrWhiteSpace(data.CityName))
                        query = query.Where(s => s.Region.Area.City.Name.Contains(data.CityName));

                    if (data.CountryID > 0)
                        query = query.Where(s => s.Region.Area.City.Country.ID == data.CountryID);

                    if (!string.IsNullOrWhiteSpace(data.CountryName))
                        query = query.Where(s => s.Region.Area.City.Country.Name.Contains(data.CountryName));

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
        public SectorDTO SectorInsert(SectorDTO data)
        {
            Sector Sector = null;
            Sector = Mapper.Map(data);
            UseContext(c =>
            {
                c.Sector.Add(Sector);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Sector.ID;
            return data;
        }
        public List<SectorDTO> SectorInsert(List<SectorDTO> data)
        {

            List<Sector> Sectorlist = null;
            Sectorlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Sector.AddRange(Sectorlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public SectorDTO SectorUpdate(SectorDTO data)
        {
            SectorDTO SectorDto = new SectorDTO();
            if (data.ID > 0)
            {
                SectorDto = SectorGet(new SectorDTO { ID = data.ID }, null)?.FirstOrDefault();
                SectorDto = new SectorDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : SectorDto.Name,
                    RegionID = data?.RegionID > 0 ? data?.RegionID : SectorDto.RegionID,
                    Code = data?.Code > 0 ? data?.Code : SectorDto.Code,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : SectorDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            Sector Sector = Mapper.Map(SectorDto);
            UseContext(databsse =>
            {
                databsse.Entry(Sector).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return SectorDto;
        }
        public SectorDTO SectorDelete(SectorDTO data)
        {
            SectorDTO SectorDto = new SectorDTO();
            if (data.ID > 0)
            {
                SectorDto = SectorGet(new SectorDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            Sector Sector = Mapper.Map(SectorDto);
            UseContext(databsse =>
            {
                databsse.Entry(Sector).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return SectorDto;
        }

    }
}
