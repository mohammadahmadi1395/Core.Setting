using His.Reception.Api.Infrastructure;
using His.Reception.Application.Infrastructure;
using His.Reception.Application.Interface;
using His.Reception.Application.Validation;
using His.Reception.DAL.Context;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using His.Reception.DTO.User;
using His.Reception.Entities.Models;
using His.Reception.Infrastructure;
using His.Reception.Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace His.Reception.Application.Service
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IOptions<JwtConfigDto> _jwtConfig;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Login> _loginRepository;
        private readonly DbSet<Users> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;


        public UserManagerService(IOptions<JwtConfigDto> jwtConfig, 
            IRedisCacheService redisCacheService,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResource> sharedLocalizer)
            
        {
            _jwtConfig = jwtConfig;
            _redisCacheService = redisCacheService;
            _unitOfWork = unitOfWork;
            _loginRepository = _unitOfWork.Set<Login>();
            _userRepository = _unitOfWork.Set<Users>();
            _httpContextAccessor = httpContextAccessor;
            _sharedLocalizer = sharedLocalizer;
        }

        //[ValidateFilterAttribute]
        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            BaseResponseDto loginResponce = new BaseResponseDto();

            var resultVaild=CheckValidate.Vaild<LoginDto>(new LoginValidation(_sharedLocalizer),loginDto);

            if (resultVaild.Status==ResponseStatus.Fail)
            {
                return new LoginResponseDto
                {
                    Status= resultVaild.Status,
                    Message =resultVaild.Message,
                };

            }

            //login
            var hashPassowrd = Utilities.ComputeHash(loginDto.Password, new SHA256CryptoServiceProvider()).Replace("-","");
            var curUser=await _userRepository
                .Include(u=>u.UserPermission).ThenInclude(up=>up.Permission)
                .Include(u=>u.UserPermission).ThenInclude(up=>up.Section)
                .Where(u=>u.UserName== loginDto.UserName && u.Password== hashPassowrd)
                .FirstOrDefaultAsync();
          
            if (curUser is null)
            {
                //loginResponce.
                return new LoginResponseDto
                {
                    Status=ResponseStatus.Fail,
                    Message="User Not Found"
                };
            }

            var lstPermission = curUser.UserPermission.Select(g => new PermissionDto
            {
                PermissionName = g.Permission.ModuleName,
                PageAdress = g.Permission.PageAdress,
                PermissionId=g.PermissionId,
                SectionId=g.SectionId

            });

            var lstPermissionForCache = curUser.UserPermission.Select(g => new PermissionDto
            {
                PermissionName = g.Permission.Title,
                PermissionId = g.PermissionId,
                SectionId = g.SectionId,
                UserId=g.UserId,
                PageAdress=g.Permission.PageAdress
            });
            var refreshId = Guid.NewGuid();
            // loginResponce.Token = CreateToken(loginResponce?.ResponseDto.MemberUserName);

            // await _redisCacheService.AddAsync(refreshId.ToString(), JsonConvert.SerializeObject(loginResponce));
            var token = Guid.NewGuid();

            RefreshIdDto refreshIdDto = new RefreshIdDto();
            refreshIdDto.ExpDate= DateTime.Now.AddHours(5);
            refreshIdDto.RefreshID = Guid.NewGuid().ToString();
            await _redisCacheService.AddAsync(token.ToString(), JsonConvert.SerializeObject(lstPermissionForCache));

            #region add login

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo = uaParser.Parse(userAgent);

            
            await _loginRepository.AddAsync(new Entities.Models.Login
            {
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddHours(5),
                Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                Browser = clientInfo.UserAgent.Family,
                Token = token,
                UserId=curUser.Id,
                Language=loginDto.Language
            });

            await _unitOfWork.SaveChangesAsync();

            #endregion

            return new LoginResponseDto
            {
                Status = ResponseStatus.Success,
                Token = token.ToString(),
                Data=lstPermission
            }; 
        }
        
        public async Task<string> RefreshToken(string refreshId)
        {
            //var resultJson =await _redisCacheService.GetAsync(refreshId);
            //var loginResponce = JsonConvert.DeserializeObject<LoginResponceDto>(resultJson);
            //OldLoginDto loginDto = new OldLoginDto(); 
            //loginDto.ActionType = 5;
            //loginDto.RequestDto.MemberPassword = loginResponce.ResponseDto.MemberPassword;
            //loginDto.RequestDto.MemberUserName = loginResponce.ResponseDto.MemberUserName;
            //loginDto.RequestDto.GroupRoleType = 6;
            ////var resultLogin= await Login(loginDto);

            //  return resultLogin.Token;
            return null;
            //await _redisCacheService.AddAsync(loginResponce.ResponseDto.MemberUserName, resultJson);

            //var newRefreshId = Guid.NewGuid();
            //loginResponce.Token = CreateToken(loginResponce?.ResponseDto.MemberUserName);
            //loginResponce.RefreshId = newRefreshId;
            //loginResponce.ExpDate = DateTime.Now.AddHours(5);
            //await _redisCacheService.AddAsync(refreshId.ToString(), resultJson);
        }

        public async Task<string> ValidToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            string strResutl = string.Empty;

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                if (principal.Identity.IsAuthenticated)
                {
                    strResutl = principal.Identity.Name;
                }
            }
            catch (Exception ex)
            {

            }

            return strResutl;
        }


        public async Task<Login> GetLoginByToken(Guid token)
        {
            return await _loginRepository.FirstOrDefaultAsync(x => x.Token == token);
        }


        public async Task<BaseResponseDto> Logout(Guid token)
        {
            var curLogin=await _loginRepository.FirstOrDefaultAsync(x => x.Token == token);
            curLogin.ExpireDate = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponseDto
            {
                Status=ResponseStatus.Success,
                Message = "Logout User"
            };
        }

        public async Task<Login> CheckToken(Guid token)
        {
            Login login = await GetLoginByToken(token);

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

        #region Method

        private TokenValidationParameters GetValidationParameters()
        {
            //validationParameters.ValidIssuer = "zarinpal.com";
            //validationParameters.ValidAudience = "zarinpal.com";
            //validationParameters.IssuerSigningKey = key;
            //validationParameters.ValidateIssuerSigningKey = true;
            //validationParameters.ValidateAudience = true;
            return new TokenValidationParameters()
            {
                ValidIssuer = _jwtConfig.Value.JwtIssuer,
                ValidAudience = _jwtConfig.Value.JwtIssuer,
                IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.JwtKey)),
                RequireExpirationTime = true
            };
        }

        private string CreateToken(string userName)
        {
            var clm = new[] { new Claim(ClaimTypes.Name, userName) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.JwtKey));
            var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                issuer: _jwtConfig.Value.JwtIssuer,
                audience: _jwtConfig.Value.JwtIssuer,
                
                expires: DateTime.Now.AddMinutes(1),
                claims: clm,
                signingCredentials: signingCredential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponseDto> RegisterAsync(UserDto userDto)
        {
            var hashPassword = Utilities.ComputeHash(userDto.Password, new SHA256CryptoServiceProvider());

            userDto.Password = hashPassword;
            var user = Mapper.UserMapper.Map(userDto);
           // user.IsActive = true;
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "Success Register User"
            };
        }

       
        #endregion
    }
}
