using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class SubsystemDA : DataAccess
    {
        public List<SubsystemDTO> SubsystemGet(SubsystemDTO data)
        {
            var result = new List<SubsystemDTO>();
            UseContext(database =>
            {
                var query = database.Subsystem.Where(t => true);
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

                    if (data?.RunOrder > 0)
                        query = query.Where(s => s.RunOrder == data.RunOrder);

                    if (data.IsPart.HasValue)
                        query = query.Where(s => s.IsPart == data.IsPart);
                }
                else query = query.Where(s => s.IsDeleted == false);
                #endregion

                var r = query?.ToList();
                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        public SubsystemDTO SubsystemInsert(SubsystemDTO dto)
        {
            Subsystem entity = null;
            entity = Mapper.Map(dto);
            UseContext(c =>
            {
                c.Subsystem.Add(entity);
                c.SaveChanges();
            });
            if (entity.ID > 0)
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
                dto = SubsystemGet(new SubsystemDTO { ID = entity.ID })?.FirstOrDefault();
            }
            else ResponseStatus = Common.ResponseStatus.DatabaseError;
            return dto;
        }
        public List<SubsystemDTO> SubsystemInsert(List<SubsystemDTO> data)
        {

            List<Subsystem> Subsystemlist = null;
            Subsystemlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Subsystem.AddRange(Subsystemlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public SubsystemDTO SubsystemUpdate(SubsystemDTO data)
        {
            SubsystemDTO Subsystemdto = new SubsystemDTO();
            if (data.ID > 0)
            {
                Subsystemdto = SubsystemGet(new SubsystemDTO { ID = data.ID })?.FirstOrDefault();
                Subsystemdto = new SubsystemDTO
                {
                    ID = data.ID,
                    Name = !string.IsNullOrWhiteSpace(data?.Name) ? data?.Name : Subsystemdto.Name,
                    CreateDate = data?.CreateDate > DateTime.MinValue ? data?.CreateDate : Subsystemdto.CreateDate,
                    IsDeleted = data.IsDeleted,
                    Description = !string.IsNullOrWhiteSpace(data?.Description) ? data.Description : Subsystemdto.Description,
                    IsActive = data.IsActive.HasValue ? data.IsActive : Subsystemdto.IsActive,
                    IsSystem = data.IsSystem.HasValue ? data.IsSystem : Subsystemdto.IsSystem,
                    RunOrder = data?.RunOrder > 0 ? data.RunOrder : Subsystemdto?.RunOrder,
                    IsPart = data.IsPart.HasValue? data.IsPart: Subsystemdto?.IsPart,
                };
            }
            Subsystem Subsystem = Mapper.Map(Subsystemdto);
            UseContext(databsse =>
            {
                databsse.Entry(Subsystem).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return Subsystemdto;
        }
        public SubsystemDTO SubsystemDelete(SubsystemDTO data)
        {
            SubsystemDTO SubsystemDto = new SubsystemDTO();
            if (data.ID > 0)
            {
                SubsystemDto = SubsystemGet(new SubsystemDTO { ID = data.ID })?.SingleOrDefault();
            }
            Subsystem subSys = Mapper.Map(SubsystemDto);
            UseContext(databsse =>
            {
                databsse.Entry(subSys).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return SubsystemDto;
        }
    }
}