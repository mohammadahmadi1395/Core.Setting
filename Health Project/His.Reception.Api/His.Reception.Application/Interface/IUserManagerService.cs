using His.Reception.DTO.Message;
using His.Reception.DTO.User;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IUserManagerService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<string> RefreshToken(string RefreshId);
        Task<string> ValidToken(string token);
        Task<Login> GetLoginByToken(Guid token);
        Task<Login> CheckToken(Guid token);
        Task<BaseResponseDto> Logout(Guid token);
        Task<BaseResponseDto> RegisterAsync(UserDto userDto);
    }
}
