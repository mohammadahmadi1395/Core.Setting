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
    public abstract class BaseEntity<TDto, TKey> : IEntity, IHaveCustomMapping
    {
        public TKey Id {get;set;}

        public void CreateMappings(Profile profile)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseEntity<TDto> : BaseEntity<TDto, long>
    {
    }
}