using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;

namespace Gostar.Setting.DA
{
    public class FormTypeDA : DataAccess
    {

        public List<FormTypeDTO> FormTypeGet(FormTypeDTO data, FormTypeFilterDTO filter)
        {
            var result = new List<FormTypeDTO>();

            UseContext(database =>
            {
                var query = database.FormType.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date ? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1) : filter.ToCreateDate));
                    if(filter?.RequestTypelist?.Count>0)
                    {
                        List<int?> temp = new List<int?>();
                        foreach (var val in filter?.RequestTypelist)
                            temp.Add((int?)val);
                        query = query?.Where(s => s.EnumID != null && (bool)temp.Contains(s.EnumID));
                    }
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);

                    if (data.SubSystemID > 0)
                        query = query.Where(s => s.SubSystemID == data.SubSystemID);

                    if (!String.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));

                    if (!String.IsNullOrWhiteSpace(data.Enum.ToString()))
                        query = query.Where(s => s.EnumID ==(int)data.Enum);

                    if (!String.IsNullOrWhiteSpace(data.PublicCode))
                        query = query.Where(s => s.PublicCode.Contains(data.PublicCode));

                    if (!String.IsNullOrWhiteSpace(data.Coment))
                        query = query.Where(s => s.Coment.Contains(data.Coment));

                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));


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

        public FormTypeDTO FormTypeInsert(FormTypeDTO data)
        {
            FormType FormType = null;
            FormType = Mapper.Map(data);
            UseContext(c =>
            {
                c.FormType.Add(FormType);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = FormType.ID;
            return data;
        }

        public List<FormTypeDTO> FormTypeInsert(List<FormTypeDTO> data)
        {

            List<FormType> FormTypelist = null;
            FormTypelist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.FormType.AddRange(FormTypelist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;
        }


        public FormTypeDTO FormTypeUpdate(FormTypeDTO data)
        {
            FormTypeDTO FormTypeDto = new FormTypeDTO();
            if (data.ID > 0)
            {
                FormTypeDto = FormTypeGet(new FormTypeDTO { ID = data.ID }, null)?.FirstOrDefault();
                FormTypeDto = new FormTypeDTO
                {
                    ID = data.ID,
                    SubSystemID = data?.SubSystemID > 0 ? data?.SubSystemID : FormTypeDto.SubSystemID,
                    Enum = FormTypeDto?.Enum,
                    PublicCode = !String.IsNullOrWhiteSpace(data?.PublicCode) ? data.PublicCode : FormTypeDto.PublicCode,
                    Title = !String.IsNullOrWhiteSpace(data?.Title) ? data.Title : FormTypeDto.Title,
                    Coment = !String.IsNullOrWhiteSpace(data?.Coment) ? data.Coment : FormTypeDto.Coment,
                    IsDeleted = data.IsDeleted,
                    CreateDate = FormTypeDto.CreateDate
                };
            }
            FormType FormType = Mapper.Map(FormTypeDto);
            UseContext(databsse =>
            {
                databsse.Entry(FormType).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return FormTypeDto;
        }


        public FormTypeDTO FormTypeDelete(FormTypeDTO data)
        {
            FormTypeDTO FormTypeDto = new FormTypeDTO();
            if (data.ID > 0)
            {
                FormTypeDto = FormTypeGet(new FormTypeDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            FormTypeDto.IsDeleted = true;

            return FormTypeUpdate(FormTypeDto);
        }
    }
}
