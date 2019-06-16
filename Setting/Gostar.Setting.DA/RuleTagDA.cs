using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;
namespace Gostar.Setting.DA
{
    public class RuleTagDA : DataAccess
    {

        public List<RuleTagDTO> RuleTagGet(RuleTagDTO data, RuleTagFilterDTO filter)
        {
            var result = new List<RuleTagDTO>();

            UseContext(database =>
            {
                var query = database.RuleTag.Where(t => true);
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
                    if (data.RuleID > 0)
                        query = query.Where(s => s.RuleID == data.RuleID);
                    if (data.FormTypeID > 0)
                        query = query.Where(s => s.FormTypeID == data.FormTypeID);
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

        public RuleTagDTO RuleTagInsert(RuleTagDTO data)
        {
            RuleTag RuleTag = null;
            RuleTag = Mapper.Map(data);
            UseContext(c =>
            {
                c.RuleTag.Add(RuleTag);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = RuleTag.ID;
            return data;
        }

        public List<RuleTagDTO> RuleTagInsert(List<RuleTagDTO> data)
        {

            List<RuleTag> RuleTaglist = null;
            RuleTaglist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.RuleTag.AddRange(RuleTaglist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;
        }


        public RuleTagDTO RuleTagUpdate(RuleTagDTO data)
        {
            RuleTagDTO RuleTagDto = new RuleTagDTO();
            if (data.ID > 0)
            {
                RuleTagDto = RuleTagGet(new RuleTagDTO { ID = data.ID }, null)?.FirstOrDefault();
                RuleTagDto = new RuleTagDTO
                {
                    ID = data.ID,
                    RuleID = RuleTagDto.RuleID,
                    FormTypeID = RuleTagDto.FormTypeID,
                    IsDeleted = data.IsDeleted,
                    CreateDate = RuleTagDto.CreateDate
                };
            }
            RuleTag RuleTag = Mapper.Map(RuleTagDto);
            UseContext(databsse =>
            {
                databsse.Entry(RuleTag).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return RuleTagDto;
        }


        public RuleTagDTO RuleTagDelete(RuleTagDTO data)
        {
            RuleTagDTO RuleTagDto = new RuleTagDTO();
            if (data.ID > 0)
            {
                RuleTagDto = RuleTagGet(new RuleTagDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            RuleTagDto.IsDeleted = true;

            return RuleTagUpdate(RuleTagDto);
        }

    }
}
