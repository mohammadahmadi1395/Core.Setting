using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;
namespace Gostar.Setting.DA
{
    public class RegionAgentDA : DataAccess
    {
        public List<RegionAgentDTO> RegionAgentGet(RegionAgentDTO data, RegionAgentFilterDTO filter)
        {
            var result = new List<RegionAgentDTO>();
            UseContext(database =>
            {
                var query = (from ra in database.RegionAgent.Where(t => true)
                             join r in database.Region.Where(t => t.IsDeleted == false) on ra.RegionID equals r.ID
                             join a in database.Area.Where(t => t.IsDeleted == false) on r.AreaID equals a.ID
                             join ci in database.City.Where(t => t.IsDeleted == false) on a.CityID equals ci.ID
                             join co in database.Country.Where(t => t.IsDeleted == false) on ci.CountryID equals co.ID
                             select new
                             { ra, r, a, ci, co });
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromStartDate > DateTime.MinValue)
                        query = query.Where(s => s.ra.StartDate >= filter.FromStartDate);
                    if (filter?.ToStartDate > DateTime.MinValue)
                        if (filter.ToStartDate == filter.ToStartDate.Value.Date)
                        {
                            var to = filter.ToStartDate.Value.AddDays(1).AddTicks(-1);
                            query = query.Where(s => s.ra.StartDate <= to);
                        }
                        else
                            query = query.Where(s => s.ra.StartDate <= filter.ToStartDate);
                    if (filter?.FromEndDate > DateTime.MinValue)
                        query = query.Where(s => s.ra.EndDate >= filter.FromEndDate);
                    if (filter?.ToEndDate > DateTime.MinValue)
                        if (filter.ToEndDate == filter.ToEndDate.Value.Date)
                        {
                            DateTime to = filter.ToEndDate.Value.AddDays(1).AddTicks(-1);
                            query = query.Where(s => s.ra.EndDate <= to);
                        }
                        else
                            query = query.Where(s => s.ra.EndDate <= filter.ToEndDate);
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ra.ID == data.ID);
                    if (data.AgentPersonID > 0)
                        query = query.Where(s => s.ra.PersonID == data.AgentPersonID);
                    if (data.RegionID > 0)
                        query = query.Where(s => s.ra.RegionID == data.RegionID);
                    if (!string.IsNullOrWhiteSpace(data.RegionName))
                        query = query.Where(s => s.r.Name.Contains(data.RegionName));
                    if (!string.IsNullOrWhiteSpace(data.StartDate.ToString()))
                        query = query.Where(s => s.ra.StartDate.ToString().Contains(data.StartDate.ToString()));
                    if (!string.IsNullOrWhiteSpace(data.EndDate.ToString()))
                        query = query.Where(s => s.ra.EndDate.ToString().Contains(data.EndDate.ToString()));
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.ra.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.ra.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.ra.IsDeleted == false);
                    if (data?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.ra.CreateDate >= data.FromCreateDate);
                    if (data?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.ra.CreateDate <= (data.ToCreateDate == data.ToCreateDate.Value.Date ? data.ToCreateDate.Value.AddDays(1).AddTicks(-1) : data.ToCreateDate));
                    if (data?.CountryID > 0)
                        query = query.Where(s => s.co.ID == data.CountryID);
                    if (!string.IsNullOrWhiteSpace(data?.CountryName))
                        query = query.Where(s => s.co.Name.Contains(data.CountryName));
                    if (data?.CityID > 0)
                        query = query.Where(s => s.ci.ID == data.CityID);
                    if (!string.IsNullOrWhiteSpace(data?.CityName))
                        query = query.Where(s => s.ci.Name.Contains(data.CityName));
                    if (data?.AreaID > 0)
                        query = query.Where(s => s.a.ID == data.AreaID);
                    if (!string.IsNullOrWhiteSpace(data?.AreaName))
                        query = query.Where(s => s.a.Name.Contains(data.AreaName));
                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s.ra, s.r, s.a, s.ci, s.co))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        public RegionAgentDTO RegionAgentInsert(RegionAgentDTO data)
        {
            RegionAgent regionagent = null;
            regionagent = Mapper.Map(data);
            UseContext(c =>
            {
                c.RegionAgent.Add(regionagent);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            data.ID = regionagent.ID;
            return data;
        }
        public List<RegionAgentDTO> RegionAgentInsert(List<RegionAgentDTO> data)
        {

            List<RegionAgent> regionagentlist = null;
            regionagentlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.RegionAgent.AddRange(regionagentlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public RegionAgentDTO RegionAgentUpdate(RegionAgentDTO data)
        {
            RegionAgentDTO regionagentDto = new RegionAgentDTO();
            if (data.ID > 0)
            {
                regionagentDto = RegionAgentGet(new RegionAgentDTO { ID = data.ID }, null)?.FirstOrDefault();
                regionagentDto = new RegionAgentDTO
                {
                    ID = data.ID,
                    AgentPersonID = data?.AgentPersonID > 0 ? data?.AgentPersonID : regionagentDto.AgentPersonID,
                    RegionID = data?.RegionID > 0 ? data?.RegionID : regionagentDto.RegionID,
                    StartDate = data?.StartDate > DateTime.MinValue ? data.StartDate : regionagentDto.StartDate,
                    EndDate = data?.EndDate > DateTime.MinValue ? data.EndDate : (DateTime?)null,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : regionagentDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            RegionAgent regionagent = Mapper.Map(regionagentDto);
            UseContext(databsse =>
            {
                databsse.Entry(regionagent).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return regionagentDto;
        }
        public RegionAgentDTO RegionAgentDelete(RegionAgentDTO data)
        {
            RegionAgentDTO regionagentDto = new RegionAgentDTO();
            if (data.ID > 0)
            {
                regionagentDto = RegionAgentGet(new RegionAgentDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            RegionAgent regionagent = Mapper.Map(regionagentDto);
            UseContext(databsse =>
            {
                databsse.Entry(regionagent).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return regionagentDto;
        }
    }
}
