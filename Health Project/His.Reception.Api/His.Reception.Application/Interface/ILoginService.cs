using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface ILoginService
    {
        Task<Login> GetLoginByToken(Guid token);
        Task<Login> CheckToken(Guid token);
    }
}
