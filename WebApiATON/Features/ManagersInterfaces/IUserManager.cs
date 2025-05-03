

using WebApiATON.Features.DtoModels;
using WebApiATON.Storage.Models;

namespace WebApiATON.Features.ManagersInterfaces
{
    public interface IUserManager
    {
        public  Task<bool> isReg(string login);

        public Task<User> CreateUser(UserDto userDto, string createdBy);

        public Task<User> GetUser(string login);

        public Task<User> UppdateUserInformation(User user, UpdateUserRequest request, string currentLogin);

        public Task<User> ChangePassword(User user, ChangePasswordRequest request, string currentLogin);

        public Task<User> ChangeLogin(User user, ChangeLoginRequest request, string currentLogin);
        public  Task<IEnumerable<User>> GetActiveUsersAsync();

        public Task<UserInfoDto?> GetUserInfoByLoginAsync(string login);

        public Task<UserInfoDto?> GetSelfUserInfoAsync(string login, string password);

        public Task<IEnumerable<User>> GetUsersOlderThanAsync(int age);

        public Task<bool> DeleteUserAsync(string login, bool softDelete, string revokedBy);

        public Task<bool> RestoreUserAsync(string login);
    }
}
