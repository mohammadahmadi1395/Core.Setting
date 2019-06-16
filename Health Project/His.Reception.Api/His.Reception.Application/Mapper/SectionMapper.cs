using His.Reception.DTO;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class SectionMapper
    {
        public static Section Map(SectionDto sectionDto)
        {
            return new Section
            {
                Id = sectionDto.Id,
                Note = sectionDto.Note,
                Title = sectionDto.Title,
                TitleLang2 = sectionDto.TitleLang2,
                Address = sectionDto.Address,
                No = sectionDto.No,
                Phone = sectionDto.Phone,
                SuperVisorPersonelId = sectionDto.SuperVisorPersonelId
            };
        }

        public static SectionDto Map(Section section)
        {
            return new SectionDto
            {
                Id = section.Id,
                Note = section.Note,
                Title = section.Title,
                TitleLang2 = section.TitleLang2,
                Address = section.Address,
                No = section.No,
                Phone = section.Phone,
                SuperVisorPersonelId = section.SuperVisorPersonelId
            };
        }
    }
}
