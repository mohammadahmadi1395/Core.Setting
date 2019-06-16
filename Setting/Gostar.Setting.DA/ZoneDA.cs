using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class ZoneDA : DataAccess
    {
        public List<ZoneDTO> ZoneUpdate(List<ZoneDTO> data)
        {
            List<Zone> Zonelist = null;
            Zonelist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                foreach (var item in Zonelist)
                    c.Entry(item).State = System.Data.Entity.EntityState.Modified;
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public List<ZoneDTO> ZoneGet(ZoneDTO data = null, ZoneFilterDTO filter = null)
        {
            var result = new List<ZoneDTO>();
            UseContext(database =>
            {
                var query = database.Zone.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.IDList?.Count > 0)
                        query = query.Where(s => filter.IDList.Contains(s.ID));
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date ? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1) : filter.ToCreateDate));
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);

                    if (!String.IsNullOrWhiteSpace(data.Code))
                        query = query.Where(s => s.Code.Contains(data.Code));

                    if (!String.IsNullOrWhiteSpace(data.OldCode))
                        query = query.Where(s => s.OldCode.Contains(data.OldCode));

                    if (data.ParentID > 0)
                        query = query.Where(s => s.ParentID == data.ParentID);

                    if (!string.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));

                    if (!string.IsNullOrWhiteSpace(data.Type?.ToString()))
                        query = query.Where(s => s.Type == (int)data.Type);

                    if (!string.IsNullOrWhiteSpace(data.Comment))
                        query = query.Where(s => s.Comment.Contains(data.Comment));

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

        public List<ZoneDTO> AllZoneGet()
        {
            var result = new List<ZoneDTO>();
            UseContext(database =>
            {
                var query = database.Zone;
                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        public List<ZoneDTO> ZoneSearch(ZoneDTO data = null)
        {
            var result = new List<ZoneDTO>();
            UseContext(database =>
            {
                var query = (from z in database.Zone
                             where z.Title.Contains(data.Title) || z.Code.Contains(data.Title)
                                   && z.IsDeleted == false
                             select new
                             {
                                 z.Title,
                                 z.ID,
                                 z.Code
                             });
                foreach (var zone in query.ToList())
                {
                    ZoneDTO zoneDto = new ZoneDTO { Title = zone.Title, Code = zone.Code, ID = zone.ID };
                    result.Add(zoneDto);
                }

                //result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public ZoneDTO ZoneInsert(ZoneDTO data)
        {
            Zone Zone = null;
            //data.Code = GenerateCode(data);
            Zone = Mapper.Map(data);
            UseContext(c =>
            {
                c.Zone.Add(Zone);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Zone.ID;
            return data;
        }
        public List<ZoneDTO> ZoneInsert(List<ZoneDTO> data)
        {

            List<Zone> Zonelist = null;
            //foreach (var val in data)
            //    val.Code =  GenerateCode(val);
            Zonelist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Zone.AddRange(Zonelist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public ZoneDTO ZoneUpdate(ZoneDTO data)
        {
            ZoneDTO ZoneDto = new ZoneDTO();
            if (data.ID > 0)
            {
                ZoneDto = ZoneGet(new ZoneDTO { ID = data.ID }, null)?.FirstOrDefault();
                ZoneDto = new ZoneDTO
                {
                    ID = data.ID,
                    Code = ZoneDto.Code,
                    ParentID = data?.ParentID,
                    Title = !string.IsNullOrWhiteSpace(data?.Title) ? data?.Title : ZoneDto.Title,
                    Type = !string.IsNullOrWhiteSpace(data?.Type?.ToString()) ? data?.Type : ZoneDto.Type,
                    Comment = !string.IsNullOrWhiteSpace(data?.Comment) ? data?.Comment : ZoneDto.Comment,
                    CreateDate = ZoneDto.CreateDate,
                    IsDeleted = data.IsDeleted,
                    LeftIndex = ZoneDto.LeftIndex,
                    RightIndex = ZoneDto.RightIndex,
                    Depth = ZoneDto.Depth,
                    OldCode = data.OldCode
                };
            }
            Zone Zone = Mapper.Map(ZoneDto);
            UseContext(databsse =>
            {
                databsse.Entry(Zone).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return ZoneDto;
        }
        public ZoneDTO ZoneDelete(ZoneDTO data)
        {
            ZoneDTO ZoneDto = new ZoneDTO();
            if (data.ID > 0)
            {
                ZoneDto = ZoneGet(new ZoneDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            Zone Zone = Mapper.Map(ZoneDto);
            UseContext(databsse =>
            {
                databsse.Entry(Zone).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return ZoneDto;
        }

        //private string GenerateCode(ZoneDTO data)
        //{
        //    string FatherCode = string.Empty;
        //    int? MaxCode = null;
        //    var List = new List<Zone>();
        //    UseContext(database =>
        //    {
        //        List = database.Zone?.Where(s => s.ParentID == data.ParentID || s.ID == data.ParentID).ToList();
        //    });
        //    if (data.ParentID > 0)
        //    {
        //        FatherCode = List?.Where(s => s.ID == data.ParentID)?.SingleOrDefault()?.Code;
        //        if (List?.Count > 1)
        //            MaxCode = List?.Where(s => s.ParentID == data.ParentID)?.Select(s => int.Parse(s.Code.Substring(s.Code.LastIndexOf('-') + 1)))?.Max();
        //    }
        //    else if (List?.Count > 0)
        //        MaxCode = List?.Select(s => int.Parse(s.Code))?.Max();
        //    if (string.IsNullOrWhiteSpace(FatherCode))
        //    {
        //        if (MaxCode > 0)
        //            return (MaxCode + 1)?.ToString();
        //        else
        //            return 1.ToString();
        //    }
        //    else
        //    {
        //        if (MaxCode > 0)
        //            return FatherCode + "-" + (MaxCode + 1)?.ToString();
        //        else
        //            return FatherCode + "-" + (1.ToString());
        //    }

        //}

    }
}
