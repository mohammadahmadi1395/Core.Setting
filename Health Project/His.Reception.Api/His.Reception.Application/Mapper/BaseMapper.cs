using His.Reception.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class BaseMapper 
    {
        public static BaseDto Map(object t )
        {
            return new BaseDto
            {
                Id = Convert.ToInt32(t.GetType().GetProperty("Id").GetValue(t) ?? 0),
                Title = t.GetType().GetProperty("Title")?.GetValue(t)?.ToString(),
                TitleLang2 = t.GetType().GetProperty("TitleLang2").GetValue(t)?.ToString(),
                Note = t.GetType().GetProperty("Note").GetValue(t)?.ToString(),
                Code1 = t.GetType().GetProperty("Code1").GetValue(t)?.ToString(),
                Code2 = t.GetType().GetProperty("Code2").GetValue(t)?.ToString(),
                IsAdmin = Boolean.Parse((string)(t.GetType().GetProperty("IsAdmin")?.GetValue(t) ?? true).ToString())
            };
        }

        public static object Map(object obj,BaseDto baseDto)
        {
            obj.GetType().GetProperty("Id").SetValue(obj, baseDto.Id,null);
            obj.GetType().GetProperty("Title").SetValue(obj, baseDto.Title);
            obj.GetType().GetProperty("TitleLang2").SetValue(obj, baseDto.TitleLang2);
            obj.GetType().GetProperty("Note").SetValue(obj, baseDto.Note);
            obj.GetType().GetProperty("Code1").SetValue(obj, baseDto.Note);
            obj.GetType().GetProperty("Code2").SetValue(obj, baseDto.Note);
            obj.GetType().GetProperty("IsAdmin").SetValue(obj, baseDto.IsAdmin);

            return obj;
        }
    }
}
