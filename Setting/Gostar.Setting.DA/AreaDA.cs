using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;
namespace Gostar.Setting.DA
{
    public class AreaDA : DataAccess
    {
        public List<AreaDTO> AreaGet(AreaDTO data, AreaFilterDTO filter)
        {
            var result = new List<AreaDTO>();
            UseContext(database =>
            {
                var query = database.Area.Where(t => true);
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
                        query = query.Where(s => s.CityID == data.CityID);
                    if (data.CountryID > 0)
                        query = query.Where(s => s.City.Country.ID == data.CountryID);
                    if (data.Code>0)
                        query = query.Where(s => s.Code==data.Code);
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
        public AreaDTO AreaInsert(AreaDTO data)
        {
            Area area = null;
            area = Mapper.Map(data);
            UseContext(c =>
            {
                c.Area.Add(area);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });

            data.ID = area.ID;
            return data;
        }
        public List<AreaDTO> AreaInsert(List<AreaDTO> data)
        {

            List<Area> arealist = null;
            arealist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Area.AddRange(arealist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public AreaDTO AreaUpdate(AreaDTO data)
        {
            AreaDTO areadto = new AreaDTO();
            if (data.ID > 0)
            {
                areadto = AreaGet(new AreaDTO { ID = data.ID }, null)?.FirstOrDefault();
                areadto = new AreaDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : areadto.Name,
                    CityID = data?.CityID > 0 ? data?.CityID : areadto.CityID,
                    Code = data?.Code>0 ? data?.Code : areadto.Code,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : areadto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            Area area = Mapper.Map(areadto);
            UseContext(databsse =>
            {
                databsse.Entry(area).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return areadto;
        }
        public AreaDTO AreaDelete(AreaDTO data)
        {
            AreaDTO areaDto = new AreaDTO();
            if (data.ID > 0)
            {
                areaDto = AreaGet(new AreaDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            Area labwork = Mapper.Map(areaDto);
            UseContext(databsse =>
            {
                databsse.Entry(labwork).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return areaDto;
        }
    }
}
