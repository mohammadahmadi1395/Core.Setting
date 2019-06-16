using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;
namespace Gostar.Setting.DA
{
    public class TypeoforganizationDA : DataAccess
    {
        public List<TypeoforganizationDTO> TypeoforganizationGet(TypeoforganizationDTO data, PagingInfoDTO paging = null)
        {
            var result = new List<TypeoforganizationDTO>();
            UseContext(database =>
            {
                var query = database.Typeoforganization.Where(t => true);
                #region Filter
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (!string.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));

                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));

                    if (data.IsDeleted.HasValue)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                #endregion
                ResultCount = query.Count();

                if (paging != null)
                {
                    if (paging.IsPaging)
                    {
                        int skip = (paging.Index - 1) * paging.Size;
                        query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                    }
                }
                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public TypeoforganizationDTO TypeoforganizationInsert(TypeoforganizationDTO data)
        {
            Typeoforganization Typeoforganization = null;
            Typeoforganization = Mapper.Map(data);
            UseContext(c =>
            {
                c.Typeoforganization.Add(Typeoforganization);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Typeoforganization.ID;
            return data;
        }
        public List<TypeoforganizationDTO> TypeoforganizationInsert(List<TypeoforganizationDTO> data)
        {

            List<Typeoforganization> Typeoforganizationlist = null;
            Typeoforganizationlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Typeoforganization.AddRange(Typeoforganizationlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }

        public TypeoforganizationDTO TypeoforganizationUpdate(TypeoforganizationDTO data)
        {
            TypeoforganizationDTO TypeoforganizationDto = new TypeoforganizationDTO();
            if (data.ID > 0)
            {
                TypeoforganizationDto = TypeoforganizationGet(new TypeoforganizationDTO { ID = data.ID })?.FirstOrDefault();
                TypeoforganizationDto = new TypeoforganizationDTO
                {
                    ID = data.ID,
                    Title = !string.IsNullOrWhiteSpace(data?.Title) ? data?.Title : TypeoforganizationDto.Title,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : TypeoforganizationDto.CreateDate,
                    IsDeleted = data.IsDeleted

                };
            }
            Typeoforganization Typeoforganization = Mapper.Map(TypeoforganizationDto);
            UseContext(databsse =>
            {
                databsse.Entry(Typeoforganization).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return TypeoforganizationDto;
        }
        public TypeoforganizationDTO TypeoforganizationDelete(TypeoforganizationDTO data)
        {
            TypeoforganizationDTO TypeoforganizationDto = new TypeoforganizationDTO();
            if (data.ID > 0)
            {
                TypeoforganizationDto = TypeoforganizationGet(new TypeoforganizationDTO { ID = data.ID })?.SingleOrDefault();
            }
            Typeoforganization Typeoforganization = Mapper.Map(TypeoforganizationDto);
            UseContext(databsse =>
            {
                databsse.Entry(Typeoforganization).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return TypeoforganizationDto;
        }
    }
}
