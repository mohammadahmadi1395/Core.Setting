using FluentValidation;
using FluentValidation.Results;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace His.Reception.Application.Validation
{
    public class  CheckValidate 
    {
        public static BaseResponseDto Vaild<T>(IValidator av ,T t)
        {
            var resultVaild = av.Validate(t);

            if (!resultVaild.IsValid)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Message = resultVaild.Errors.FirstOrDefault().ErrorMessage
                };
            }
            else
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success,
                };
            }
        }
    }
}
