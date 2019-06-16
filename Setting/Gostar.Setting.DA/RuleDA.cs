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
    public class RuleDA:DataAccess
    {

        public List<RuleDTO> RuleGet(RuleDTO data, RuleFilterDTO filter, PagingInfoDTO paging = null)
        {
            var result = new List<RuleDTO>();

            UseContext(database =>
            {
                var query = database.Rule.Where(t => true);
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
                    if (!String.IsNullOrWhiteSpace(data.Type.ToString()))
                        query = query.Where(s => s.Type == (int)data.Type);
                    if (!String.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));
                    if (!String.IsNullOrWhiteSpace(data.Description))
                        query = query.Where(s => s.Description.Contains(data.Description));
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else query = query.Where(s => s.IsDeleted == false);
                ResultCount = query.Count();

                if (paging != null)
                {
                    if (paging.IsPaging)
                    {
                        int skip = (paging.Index - 1) * paging.Size;
                        query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
                    }
                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }

        public RuleDTO RuleInsert(RuleDTO data)
        {
            Rule Rule = null;
            Rule = Mapper.Map(data);
            UseContext(c =>
            {
                c.Rule.Add(Rule);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Rule.ID;
            return data;
        }

        public List<RuleDTO> RuleInsert(List<RuleDTO> data)
        {

            List<Rule> Rulelist = null;
            Rulelist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Rule.AddRange(Rulelist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;
        }


        public RuleDTO RuleUpdate(RuleDTO data)
        {
            RuleDTO RuleDto = new RuleDTO();
            if (data.ID > 0)
            {
                RuleDto = RuleGet(new RuleDTO { ID = data.ID }, null)?.FirstOrDefault();
                RuleDto = new RuleDTO
                {
                    ID = data.ID,
                    Title = !String.IsNullOrWhiteSpace(data.Title) ? data.Title : RuleDto.Title,
                    Description = !String.IsNullOrWhiteSpace(data.Description) ? data.Description : RuleDto.Description,
                    Type = !String.IsNullOrWhiteSpace(data.Type.ToString()) ? data.Type : RuleDto.Type,
                    IsDeleted = data.IsDeleted,
                    CreateDate = RuleDto.CreateDate
                };
            }
            Rule Rule = Mapper.Map(RuleDto);
            UseContext(databsse =>
            {
                databsse.Entry(Rule).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return RuleDto;
        }


        public RuleDTO RuleDelete(RuleDTO data)
        {
            RuleDTO RuleDto = new RuleDTO();
            if (data.ID > 0)
            {
                RuleDto = RuleGet(new RuleDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            RuleDto.IsDeleted = true;

            return RuleUpdate(RuleDto);
        }

    }
}
