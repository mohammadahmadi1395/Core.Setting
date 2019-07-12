using System;

namespace Alsahab.Common
{
    public interface IBaseDTO
    {
        DateTime? CreateDate { get; set; }

        long? ID { get; set; }
        bool? IsDeleted { get; set; }

    }
}