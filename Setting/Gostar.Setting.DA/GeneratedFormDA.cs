using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class GeneratedFormDA : DataAccess
    {

        public List<GeneratedFormDTO> GeneratedFormGet(GeneratedFormDTO data)
        {
            var result = new List<GeneratedFormDTO>();
            UseContext(database =>
            {
                var query = database.GeneratedForm.Where(t => true);

                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);

                    if (!string.IsNullOrWhiteSpace(data.PublicCode))
                        query = query.Where(s => s.PublicCode == data.PublicCode);

                    if (!string.IsNullOrWhiteSpace(data.PrivateCode))
                        query = query.Where(s => s.PrivateCode == data.PrivateCode);

                    if (data.SubSystemID > 0)
                        query = query.Where(s => s.SubsystemID == data.SubSystemID);

                    if (data.UniqeCode > 0)
                        query = query.Where(s => s.UniqeCode == data.UniqeCode);

                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));

                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }

        public GeneratedFormDTO GenerateForm(FormTypeDTO data)
        {
            DateTime Today = DateTime.Now;
            data = new FormTypeDA().FormTypeGet(data,null)?.FirstOrDefault();
            var SubSystem = new SubsystemDA().SubsystemGet(new SubsystemDTO { ID = data?.SubSystemID })?.FirstOrDefault();
            data.SubSystemShortName = SubSystem.ShortName;

            String PrivateCode = String.Format("{0}{1}{2}", Today.Year.ToString().Substring(2, 2), Today.Month.ToString("00"), Today.Day.ToString("00"));
            var ThisFormGeneratedCodes = GeneratedFormGet(new GeneratedFormDTO { SubSystemID = data?.SubSystemID });
            var maxcode = ThisFormGeneratedCodes?.Max(s => s.UniqeCode);
            var thisuniqecode = (long?)1;
            if (maxcode != null)
                thisuniqecode = maxcode + 1;
            PrivateCode = String.Format("{0}{1}{2}{3}", PrivateCode, data?.SubSystemShortName, data?.PublicCode.Substring(6,4),  thisuniqecode <= 999 ? thisuniqecode?.ToString("0000") : thisuniqecode.ToString());
            GeneratedFormDTO GeneratedForm = new GeneratedFormDTO
            {
                PublicCode = data?.PublicCode,
                PrivateCode = PrivateCode,
                SubSystemID = data.SubSystemID,
                UniqeCode = thisuniqecode,
                CreateDate = Today,
            };
            DA.Entities.GeneratedForm G = null;
            G = Mapper.Map(GeneratedForm);
            UseContext(c =>
            {
                c.GeneratedForm.Add(G);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            GeneratedForm.ID = G.ID;

            return GeneratedForm;

        }





    }
}
