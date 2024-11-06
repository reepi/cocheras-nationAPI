using Common.DTOs.UserDTOs;

namespace Services.AuthService
{
    public interface IAuthService
    {
        string? Authenticate(UserForAuthDto userForAuth);
        string? Register(UserForRegistrationDto userForRegistration);
    }
}
