using Expenses.API.DTOs;

namespace Expenses.API.Data.Services.Authentication
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginRequest request);
        Task<string> Register(UserRegistrationRequest request);
        Task DeleteAll();

    }

}

