using Expenses.API.DTOs;

namespace Expenses.API.Data.Services.Authentication
{
    public interface ICurrentUserService
    {
        int UserId { get; }
    }

}

