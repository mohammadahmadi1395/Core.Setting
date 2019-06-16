using FluentValidation;
using His.Reception.DTO;
using His.Reception.Infrastructure;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Validation
{
    public class PatientValidation : AbstractValidator<PatientDto>,IValidator
    {
        
        public PatientValidation(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            //RuleFor(x => x.Age).NotNull().NotEmpty().GreaterThan((short)0)
            //    .WithMessage(sharedLocalizer["PatientForm.FieldValidation.Age"]);
               
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage(sharedLocalizer["PatientForm.FieldValidation.FirstName"]);
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage(sharedLocalizer["PatientForm.FieldValidation.LastName"]);
            RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Mobile) || !string.IsNullOrEmpty(x.Phone)).WithMessage(sharedLocalizer["PatientForm.FieldValidation.PhoneOrMobile"]);
        }
    }
}
