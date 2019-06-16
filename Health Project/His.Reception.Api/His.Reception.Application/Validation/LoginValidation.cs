using FluentValidation;
using His.Reception.DTO.User;
using His.Reception.Entities.Models;
using His.Reception.Infrastructure;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Validation
{

    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            RuleFor(x => x.Language).NotNull().NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.Language"]);
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.Password"]);
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage(sharedLocalizer["LoginForm.FieldValidation.UserName"]);
        }
    }

}
