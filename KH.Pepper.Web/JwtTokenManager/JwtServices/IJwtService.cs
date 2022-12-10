namespace KH.Pepper.Web
{
    public interface IJwtService
    {
        Task<AuthResponse> GetTokenAsync(AuthRequest authRequest,string ipAddress);
        Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName);
        Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
}
