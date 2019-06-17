using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Alsahab.Common;
using FluentValidation;
using Alsahab.Setting.Common.Validation;
using Alsahab.Setting.Common;

namespace Alsahab.Setting.BL
{
    public interface IBaseBusiness : IScopedDependency
    {
        ResponseStatus ResponseStatus { get; set; }
        int? ResultCount { get; set; }
        PagingInfoDTO PagingInfo { get; set; }
        string ErrorMessage { get; set; }
        IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        CultureInfo Culture { get; set; }
        UserInfoDTO User { get; set; }
    }
}
