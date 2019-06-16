using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class SubpartDA : DataAccess
    {
        public List<SubpartDTO> SubpartGet(SubpartDTO data)
        {
            var result = new List<SubpartDTO>();
            UseContext(database =>
            {
                var query = database.Subpart.Where(t => true);
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);

                    if (!string.IsNullOrWhiteSpace(data.Name))
                        query = query.Where(s => s.Name.Contains(data.Name));

                    if (!string.IsNullOrWhiteSpace(data.Description))
                        query = query.Where(s => s.Description.Contains(data.Description));

                    if (data.IsDeleted.HasValue)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);

                    if (data.IsActive.HasValue)
                        query = query.Where(s => s.IsActive == data.IsActive);
                    else
                        query = query.Where(s => s.IsActive == true);

                    if (data.IsSystem.HasValue)
                        query = query.Where(s => s.IsSystem == data.IsSystem);

                    if (data?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= data.FromCreateDate);

                    if (data?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (data.ToCreateDate == data.ToCreateDate.Value.Date ? data.ToCreateDate.Value.AddDays(1).AddTicks(-1) : data.ToCreateDate));

                    if (data?.IDList?.Count > 0)
                        query = query.Where(s => data.IDList.Contains(s.ID));

                    if (data.SubsystemID > 0)
                        query = query.Where(s => s.Subsystem.ID == data.SubsystemID);

                    if (!string.IsNullOrWhiteSpace(data.SubsystemName))
                        query = query.Where(s => s.Subsystem.Name.Contains(data.SubsystemName));
                }
                else query = query.Where(s => s.IsDeleted == false);
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        public SubpartDTO SubpartInsert(SubpartDTO dto)
        {
            Subpart entity = null;
            entity = Mapper.Map(dto);
            UseContext(c =>
            {
                c.Subpart.Add(entity);
                c.SaveChanges();
            });
            if (entity.ID > 0)
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
                dto = SubpartGet(new SubpartDTO { ID = entity.ID })?.FirstOrDefault();
            }
            return dto;
        }
        public List<SubpartDTO> SubpartInsert(List<SubpartDTO> data)
        {

            List<Subpart> Subpartlist = null;
            Subpartlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Subpart.AddRange(Subpartlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public SubpartDTO SubpartUpdate(SubpartDTO data)
        {
            SubpartDTO Subpartdto = new SubpartDTO();
            if (data.ID > 0)
            {
                Subpartdto = SubpartGet(new SubpartDTO { ID = data.ID })?.FirstOrDefault();
                Subpartdto = new SubpartDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : Subpartdto.Name,
                    CreateDate = data?.CreateDate > DateTime.MinValue ? data?.CreateDate : Subpartdto.CreateDate,
                    IsDeleted = data.IsDeleted,
                    Description = !string.IsNullOrWhiteSpace(data?.Description) ? data.Description : Subpartdto.Description,
                    IsActive = data.IsActive.HasValue ? data.IsActive : Subpartdto.IsActive,
                    IsSystem = data.IsSystem.HasValue ? data.IsSystem : Subpartdto.IsSystem,
                    SubsystemID = data.SubsystemID > 0 ? data.SubsystemID : Subpartdto.SubsystemID,
                };
            }
            Subpart Subpart = Mapper.Map(Subpartdto);
            UseContext(databsse =>
            {
                databsse.Entry(Subpart).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return Subpartdto;
        }
        public SubpartDTO SubpartDelete(SubpartDTO data)
        {
            SubpartDTO SubpartDto = new SubpartDTO();
            if (data.ID > 0)
            {
                SubpartDto = SubpartGet(new SubpartDTO { ID = data.ID })?.SingleOrDefault();
            }
            Subpart subSys = Mapper.Map(SubpartDto);
            UseContext(databsse =>
            {
                databsse.Entry(subSys).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return SubpartDto;
        }
    }
}