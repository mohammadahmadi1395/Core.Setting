using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Alsahab.Common;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.MyAPI
{
    public class BaseRequest<TDto, TFilterDto>
    where TDto : BaseDTO
    where TFilterDto : TDto
    {
        /// <summary>
        /// مشخصات کاربر
        /// </summary>
        /// <value></value>
        [DataMember]
        public UserInfoDTO User { get; set; }
        /// <summary>
        /// شناسه درخواست
        /// </summary>
        /// <value></value>
        [DataMember]
        public long? RequestID { get; set; }
        /// <summary>
        /// نوع عملیات
        /// </summary>
        /// <value></value>
        [DataMember]
        public Alsahab.Common.ActionType ActionType { get; set; }
        /// <summary>
        /// شیء درخواست
        /// </summary>
        /// <value></value>
        [DataMember]
        public TDto RequestDto { get; set; }
        /// <summary>
        /// فیلترهای درخواست گزارش
        /// </summary>
        /// <value></value>
        [DataMember]
        public TFilterDto RequestFilterDto { get; set; }
        /// <summary>
        /// اشیای درخواست لیستی
        /// </summary>
        /// <value></value>
        [DataMember]
        public List<TDto> RequestDtoList { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        /// <value></value>
        [DataMember]
        public Language? Language { get; set; }
        /// <summary>
        /// اطلاعات صفحه‌بندی
        /// </summary>
        /// <value></value>
        [DataMember]
        public PagingInfoDTO PagingInfo { get; set; }
    }

    public class BaseRequest<TDto> : BaseRequest<TDto, TDto>
    where TDto : BaseDTO
    {
    }
}