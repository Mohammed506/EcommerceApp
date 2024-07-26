
using Ecommerce.Domain.Shared.DTO;
using Ecommerce.Domain.Shared.Entites;

namespace Ecommerce.Domain.Shared.Interfaces;

    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginRequest request);
        Task RegisterAsync(RegisterRequest request);
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(string id, User userIn);
        Task DeleteUserAsync(string id);
    }

