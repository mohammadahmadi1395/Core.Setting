using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class StatementDA : DataAccess
    {
        public List<DTO.StatementSubsystemDTO> StatementGet(DTO.StatementDTO data)
        {
            var result = new List<DTO.StatementSubsystemDTO>();
            UseContext(database =>
            {
                //var query = from s in database.Statement
                //            join ss in database.StatementSubsystem on s.ID equals ss.StatementID
                //            where s.IsDeleted == false
                //            select new { s, ss };
                var query = from s in database.Statement
                            join ss1 in database.StatementSubsystem on s.ID equals ss1.StatementID into ss2
                            from ss in ss2.DefaultIfEmpty()
                            where true
                            select new { ss, s };

                #region Data
                if (data != null)
                {
                    //if (data?.FilterSubsystemID > 0)
                    //    query = query.Where(t => t.ss.SubsystemID == data.FilterSubsystemID);

                    if (data.ID > 0)
                        query = query.Where(t => t.s.ID == data.ID);

                    if (data.CreateDate > DateTime.MinValue)
                        query = query.Where(t => t.s.CreateDate.Value.Date == data.CreateDate.Value.Date);

                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.s.IsDeleted == false);

                    if (data?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(t => t.s.CreateDate >= data.FromCreateDate);

                    if (data?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(t => t.s.CreateDate <= (data.ToCreateDate == data.ToCreateDate.Value.Date ? data.ToCreateDate.Value.AddDays(1).AddTicks(-1) : data.ToCreateDate));

                    //if (!string.IsNullOrWhiteSpace(data?.SubsystemName))
                    //    query = query.Where(s => s.Subsystem.Name == data.SubsystemName);

                    //if (data?.FilterSubsystemID > 0)
                    //    query = query.Where(t => t.ss.SubsystemID == data.FilterSubsystemID); // sub SubSystemID == data.SubsystemID);

                    if (!string.IsNullOrWhiteSpace(data.ArabicText))
                        query = query.Where(t => t.s.ArabicText.Contains(data.ArabicText));

                    if (!string.IsNullOrWhiteSpace(data.PersianText))
                        query = query.Where(t => t.s.PersianText.Contains(data.PersianText));

                    if (!string.IsNullOrWhiteSpace(data.EnglishText))
                        query = query.Where(t => t.s.EnglishText.Contains(data.EnglishText));

                    if (!string.IsNullOrWhiteSpace(data.TagName))
                        query = query.Where(t => t.s.TagName.Contains(data.TagName));

                    if (data?.IDList?.Count > 0)
                        query = query.Where(t => data.IDList.Contains(t.s.ID));
                  
                    //if (data.TypeID > 0)
                    //    query = query.Where(t => t.s.TypeID == (int)data.TypeID);

                    //if ((int)data.TypeTitle > 0)
                    //    query = query.Where(s => s.TypeID == (int)data.TypeTitle);
                }
                #endregion

                result = query?.ToList().Select(t => Mapper.Map(t.s, t.ss))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }

        public DTO.StatementDTO StatementInsert(DTO.StatementDTO data)
        {
            if (data?.ID > 0)
                return data;
            Statement Statement = null;
            Statement = Mapper.Map(data);
            UseContext(c =>
            {
                c.Statement.Add(Statement);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            data.ID = (int)Statement?.ID;
            return data;
        }
        public List<DTO.StatementDTO> StatementInsert(List<DTO.StatementDTO> data)
        {
            List<DTO.StatementDTO> NewData = new List<DTO.StatementDTO>();
            foreach (var d in data)
                if (!(d.ID > 0))
                    NewData.Add(d);
            if (!(NewData?.Count > 0))
                return data;
            data = NewData;
            List<Statement> Statementlist = null;
            Statementlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Statement.AddRange(Statementlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            for (int i = 0; i < Statementlist.Count; i++)
                data[i].ID = Statementlist[i].ID;
            return data;

        }
        public DTO.StatementDTO StatementUpdate(DTO.StatementDTO data)
        {
            //DTO.StatementDTO Statementdto = new DTO.StatementDTO();
            //if (data.ID > 0)
            //{
            //    Statementdto = StatementGet(new DTO.StatementDTO { ID = data.ID })?.Select(s => new DTO.StatementDTO
            //    {
            //        ID = s.ID,
            //        ArabicText = s.ArabicText,
            //        CreateDate = s.CreateDate,
            //        EnglishText = s.EnglishText,
            //        IsDeleted = s.IsDeleted,
            //        PersianText = s.PersianText,
            //        TagName = s.TagName,                    
            //    })?.FirstOrDefault();
            //    Statementdto = new DTO.StatementDTO
            //    {
            //        ID = data.ID,
            //        TagName = !string.IsNullOrWhiteSpace(data?.TagName) ? data?.TagName : Statementdto.TagName,
            //        ArabicText = !string.IsNullOrWhiteSpace(data.ArabicText) ? data.ArabicText : Statementdto.ArabicText,
            //        EnglishText = !string.IsNullOrWhiteSpace(data.EnglishText) ? data.EnglishText : Statementdto.EnglishText,
            //        PersianText = !string.IsNullOrWhiteSpace(data.PersianText) ? data.PersianText : Statementdto.PersianText,
            //        //SubsystemID = data.SubsystemID > 0 ? data.SubsystemID : Statementdto.SubsystemID,
            //        //TypeID = data.TypeID > 0 ? data.TypeID : Statementdto.TypeID,
            //        CreateDate = data?.CreateDate > DateTime.MinValue ? data?.CreateDate : Statementdto.CreateDate,
            //        IsDeleted = data.IsDeleted,
            //    };
            //}
            Statement Statement = Mapper.Map(data);
            UseContext(databsse =>
            {
                databsse.Entry(Statement).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public DTO.StatementDTO StatementDelete(DTO.StatementDTO data)
        {
            DTO.StatementDTO StatementDto = new DTO.StatementDTO();
            //if (data.ID > 0)
            //{
            //    StatementDto = StatementGet(new DTO.StatementDTO { ID = data.ID })?.SingleOrDefault();
            //}

            //Statement labwork = Mapper.Map(data);
            UseContext(databsse =>
            {
                Statement labwork = databsse.Statement.Where(s => s.ID == data.ID)?.FirstOrDefault();
                databsse.Entry(labwork).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return StatementDto;
        }

        public StatementSubsystemDTO StatementSubsystemDelete(StatementSubsystemDTO data)
        {
            DTO.StatementSubsystemDTO StatementSubsystemDto = new DTO.StatementSubsystemDTO();
            if (data.ID > 0)
            {
                StatementSubsystemDto = StatementSubsystemGet(new DTO.StatementSubsystemDTO { ID = data.ID })?.SingleOrDefault();
            }

            StatementSubsystem labwork = Mapper.Map(StatementSubsystemDto);
            UseContext(databsse =>
            {
                databsse.Entry(labwork).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return StatementSubsystemDto;

        }

        public StatementSubsystemDTO StatementSubsystemInsert(StatementSubsystemDTO data)
        {
            StatementSubsystem labwork = Mapper.Map(data);
            UseContext(databsse =>
            {
                databsse.Entry(labwork).State = System.Data.Entity.EntityState.Added;
                data.ID = databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }

        public List<StatementSubsystemDTO> StatementSubsystemGet(StatementSubsystemDTO data)
        {
            var result = new List<DTO.StatementSubsystemDTO>();
            UseContext(database =>
            {
                var query = database.StatementSubsystem.Where(t => true);

                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(t => t.ID == data.ID);

                    if (data?.SubsystemID > 0)
                        query = query.Where(s => s.SubsystemID == data.SubsystemID);

                    if (data?.StatementID > 0)
                        query = query.Where(s => s.StatementID == data.StatementID);

                    if (!string.IsNullOrWhiteSpace(data?.SubsystemName))
                        query = query.Where(s => s.Subsystem.Name.Trim() == data.SubsystemName);

                    if (!string.IsNullOrWhiteSpace(data.ArabicText))
                        query = query.Where(t => t.Statement.ArabicText.Contains(data.ArabicText));

                    if (!string.IsNullOrWhiteSpace(data.PersianText))
                        query = query.Where(t => t.Statement.PersianText.Contains(data.PersianText));

                    if (!string.IsNullOrWhiteSpace(data.EnglishText))
                        query = query.Where(t => t.Statement.EnglishText.Contains(data.EnglishText));

                    if (!string.IsNullOrWhiteSpace(data.TagName))
                        query = query.Where(t => t.Statement.TagName.Contains(data.TagName));

                }
                #endregion

                result = query?.ToList().Select(t => Mapper.Map(t))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
    }
}
