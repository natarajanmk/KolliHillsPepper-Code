using KH.Pepper.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace KH.Pepper.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRefreshTokenRepository _unitOfWork;

        public AccountController(IJwtService jwtService, IUserRefreshTokenRepository unitOfWork)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Login Controll
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        //[Route("", Name = "UserAuthToken")]
        public async Task<IActionResult> UserAuthentication([FromBody] AuthRequest authRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { IsSuccess = false, Reason = "email (or) phoneNumber and password must be provided." });

            var authResponse = await _jwtService.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString());

            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }

        /// <summary>
        /// RefreshToken controll
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]         
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request,CancellationToken cancellationToken )
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { IsSuccess = false, Reason = "Tokens must be provided" });

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var token = GetJwtToken(request.ExpiredToken);

            var userRefreshToken = _unitOfWork.FirstOrDefault(
                x => x.IsInvalidated == false && x.Token == request.ExpiredToken
                && x.RefreshToken == request.RefreshToken
                && x.IpAddress == ipAddress);

            AuthResponse response = ValidateDetails(token, userRefreshToken);
            if (!response.IsSuccess)
                return BadRequest(response);

            userRefreshToken.IsInvalidated = true;
            _unitOfWork.Update(userRefreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var userName = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var authResponse = await _jwtService.GetRefreshTokenAsync(ipAddress, userRefreshToken.UserId,
                userName);

            return Ok(authResponse);


        }


        #region Token Validation
        private AuthResponse ValidateDetails(JwtSecurityToken token, UserRefreshToken userRefreshToken)
        {
            if (userRefreshToken == null)
                return new AuthResponse { IsSuccess = false, Reason = "Invalid Token Details." };

            if (token.ValidTo > DateTime.UtcNow)
                return new AuthResponse { IsSuccess = false, Reason = "Token not expired." };

            if (!userRefreshToken.IsActive)
                return new AuthResponse { IsSuccess = false, Reason = "Refresh Token Expired" };

            return new AuthResponse { IsSuccess = true };
        }

        private JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
        #endregion
    }
}
