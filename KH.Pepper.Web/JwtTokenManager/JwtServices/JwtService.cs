using KH.Pepper.Core.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KH.Pepper.Web
{
    public class JwtService : IJwtService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        
        private readonly IConfiguration _configuration;

        public JwtService(IUserRepository userRepository,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(userName);
            return await SaveTokenDetails(ipAddress, userId, accessToken, refreshToken);
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest authRequest,string ipAddress)
        {
            var user = _userRepository.FirstOrDefault(x => 
                                                                        (x.EmailAddress.Equals(authRequest.UserName) || 
                                                                        x.PhoneNumber.Equals(authRequest.UserName))
                                                                        && x.Password.Equals(authRequest.Password));
            if (user == null)
                return await Task.FromResult<AuthResponse>(null);

            string tokenString = GenerateToken(authRequest.UserName);
            string refreshToken = GenerateRefreshToken();
            return await SaveTokenDetails(ipAddress, user.Id, tokenString, refreshToken);

        }

        private async Task<AuthResponse> SaveTokenDetails(string ipAddress, int userId, string tokenString, string refreshToken)
        {
            var userRefreshToken = new UserRefreshToken
            {
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(15),
                IpAddress = ipAddress,
                IsInvalidated = false,
                RefreshToken = refreshToken,
                Token = tokenString,
                UserId = userId
            };
            await _userRefreshTokenRepository.AddAsync(userRefreshToken);
            //await _userRefreshTokenRepository.SaveChangesAsync(cancellationToken);

            //Remove Old token from tables
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@UserId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = userId
                        } };
                         

            //var parameters = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            await _userRefreshTokenRepository.ExecuteSqlNonQuery("dbo.sp_DeleteToken @UserId", param);
            //await dbcontext.Database.ExecuteSqlRawAsync("dbo.sp_DeleteToken @UserId", new [] { userIdParameter}, cancellationToken);

            return new AuthResponse { Token = tokenString, RefreshToken = refreshToken,IsSuccess=true };
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randonNumberGenerate = RandomNumberGenerator.Create();
            randonNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

            //var byteArray = new byte[64];
            //using(var cryptoProvider = new RNGCryptoServiceProvider())
            //{
            //    cryptoProvider.GetBytes(byteArray);

            //    return Convert.ToBase64String(byteArray);
            //}
        }

        private string GenerateToken(string userName)
        {
            var jwtKey = _configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)

                }),
                Expires = DateTime.UtcNow.AddMinutes(10),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
               SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public async Task<bool> IsTokenValid(string accessToken, string ipAddress)
        {
            var isValid = _userRefreshTokenRepository.FirstOrDefault(x => 
                                                                    x.Token == accessToken
                                                                    && x.IpAddress == ipAddress) != null;
            return await Task.FromResult(isValid);
        }
    }
}
