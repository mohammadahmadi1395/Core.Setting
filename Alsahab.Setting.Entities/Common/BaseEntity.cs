using System;
using Alsahab.Setting.Entities.Common;
using AutoMapper;

namespace Alsahab.Setting.Entities
{
    //این کار باعث می‌شود که هر کلاسی که این اینترفیس را ایمپلمنت می‌کند، بدون نیاز به
    // DbSet
    // برایش به صورت خودکار جدول ساخته شود. به صورت پیشفرض هر کلاسی که بخواهد برایش در دیتابیس جدول ساخته شود نیاز دارد که از ویژگی 
    // DbSet
    // استفاده کند.
    // این کار با رفلکشن انجام می‌شود

    // نکته: برخی کلاس‌ها کلید اصلی‌شان، 
    // Id
    // نیست. مانند تنظیمات که دارای کلید و مقدار هستند یا کلاسهای 
    // Identity    
    // که در آن صورت به جای این که از 
    // BaseEntity
    // ارث بری کنند از این اینترفیس ارث‌بری می‌کنند و علاوه بر آن از مزیت ساخته شدن جدول خودکار نیز بهره‌مند می‌شوند
    public interface IEntity
    {
    }

    // با توجه به توضیحات اینترفیس، هر کلاسی که از 
    // BaseEntity
    // هم ارث‌بری کند، برای ساخته شدن جدول نیازی به 
    // DbSet
    // ندارد.
    public abstract class BaseEntity<TEntity, TDto, TKey> : IEntity, IHaveCustomMapping
    {
        public TKey ID { get; set; }
        public bool IsDeleted {get;set;}
        public DateTime CreateDate {get;set;}        

        private TEntity CastToDerivedClass(BaseEntity<TEntity, TDto, TKey> baseInstance)
        {
            return Mapper.Map<TEntity>(baseInstance);
        }
        
        // یک موجودیت را به دی تی اوی جدید تبدیل می‌کند
        public TDto ToDto()
        {
            return Mapper.Map<TDto>(CastToDerivedClass(this));
        }
        
        // یک دی تی او را به موجودیت موجود تبدیل می‌کند، یعنی خروجی همان ورودی تغییریافته است
        // public TDto ToDto(TDto dto)
        // {
        //     return Mapper.Map(CastToDerivedClass(this), dto);
        // }
        public TEntity ToEntity(TDto dto)
        {
            return Mapper.Map(dto, CastToDerivedClass(this));
        }

        // دی تی او را به موجودیت جدید تبدیل می‌کند
        public static TEntity FromDto(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TEntity, TDto>();
            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that does not contain in destination
            foreach (var property in dtoType.GetProperties())
            {
                if (entityType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }
            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TDto, TEntity> mapping)
        {
        }

    }

    public abstract class BaseEntity<TEntity, TDto> : BaseEntity<TEntity, TDto, long>
    {
    }
}