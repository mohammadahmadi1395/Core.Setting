using His.Reception.DTO;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class IllnessMapper
    {
        public static Illness Map(IllnessDto illnessdto)
        {
            return new Illness
            {
                Code = illnessdto.Code,
                Icd10 = illnessdto.Icd10,
                Icd9 = illnessdto.Icd9,
                IsAdmin = illnessdto.IsAdmin,
                IsSpecial = illnessdto.IsSpecial,
                Id = illnessdto.Id,
                Note = illnessdto.Note,
                Title = illnessdto.Title,
                TitleLang2 = illnessdto.TitleLang2
            };
        }

        public static IllnessDto Map(Illness illness)
        {
            return new IllnessDto
            {
                Code = illness.Code,
                Icd10 = illness.Icd10,
                Icd9 = illness.Icd9,
                IsAdmin = illness.IsAdmin,
                IsSpecial = illness.IsSpecial,
                Id = illness.Id,
                Note = illness.Note,
                Title = illness.Title,
                TitleLang2 = illness.TitleLang2
            };
        }
    }
}
