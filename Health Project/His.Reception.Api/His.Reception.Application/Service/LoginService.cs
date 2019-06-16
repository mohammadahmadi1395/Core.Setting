using His.Reception.Application.Interface;
using His.Reception.DAL.Context;
using His.Reception.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Login> _loginRepository;
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _loginRepository= _unitOfWork.Set<Login>();
        }

        public async Task<Login> GetLoginByToken(Guid token)
        {
           return await _loginRepository.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<Login> CheckToken(Guid token)
        {
            Login login =await GetLoginByToken(token);

            if (login is null)
                return null;
            else
            {
                if (login.ExpireDate > DateTime.Now)
                {
                    return login;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
