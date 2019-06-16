using FluentValidation;
using His.Reception.DTO;
using His.Reception.Infrastructure;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Validation
{
    public class BaseValidation : AbstractValidator<BaseDto>
    {
        public BaseValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage(sharedLocalizer["GlobalForm.FieldValidation.Title"]);
        }
    }
}
