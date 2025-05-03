
using Microsoft.EntityFrameworkCore;
using WebApiATON.Features.DtoModels;
using WebApiATON.Features.ManagersInterfaces;
using WebApiATON.Storage.DataBase;
using WebApiATON.Storage.Models;

namespace WebApiATON.Features.Managers
{
    public class UserManager : IUserManager
    {
        private readonly DataContext _dataContext;

        public UserManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> isReg(string login)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Login == login);
            return user != null;
        }

        public async Task<User> GetUser(string login)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Login == login);
            return user;
        }
        public async Task<User> CreateUser(UserDto userDto, string createdBy)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = userDto.Login,
                Password = userDto.Password,
                Name = userDto.Name,
                Gender = userDto.Gender,
                Birthday = userDto.Birthday,
                Admin = userDto.Admin,
                CreatedOn = DateTime.Now,
                CreatedBy = createdBy,
            };
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UppdateUserInformation(User user, UpdateUserRequest request, string currentLogin)
        {
            user.Name = request.NewName != null ? request.NewName : user.Name;
            user.Gender = request.NewGender.HasValue ? request.NewGender.Value : user.Gender;
            user.Birthday = request.NewBirthday.HasValue ? request.NewBirthday.Value : user.Birthday;
            user.ModifiedBy = currentLogin;
            user.ModifiedOn = DateTime.Now;

            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> ChangePassword(User user, ChangePasswordRequest request, string currentLogin)
        {
            user.Password = request.NewPassword;
            user.ModifiedBy = currentLogin;
            user.ModifiedOn = DateTime.Now;

            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> ChangeLogin(User user, ChangeLoginRequest request, string currentLogin)
        {
            user.Login = request.NewLogin;
            user.ModifiedBy = currentLogin;
            user.ModifiedOn = DateTime.Now;
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dataContext.Users.Where(x => x.RevokedOn == null).OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<UserInfoDto?> GetUserInfoByLoginAsync(string login)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(u => u.Login == login);

            if (user == null) return null;

            return new UserInfoDto
            {
                Name = user.Name,
                Gender = user.Gender,
                Birthday = user.Birthday,
                IsActive = user.RevokedOn == null
            };
        }
        public async Task<UserInfoDto?> GetSelfUserInfoAsync(string login, string password)
        {
            var user = await _dataContext.Users
                .FirstOrDefaultAsync(u => u.Login == login && u.Password == password && u.RevokedOn == null);

            if (user == null) return null;

            return new UserInfoDto
            {
                Name = user.Name,
                Gender = user.Gender,
                Birthday = user.Birthday,
                IsActive = true
            };
        }

        public async Task<IEnumerable<User>> GetUsersOlderThanAsync(int age)
        {
            var cutoffDate = DateTime.Today.AddYears(-age);

            return await _dataContext.Users
                .Where(u => u.Birthday != null && u.Birthday <= cutoffDate)
                .ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(string login, bool softDelete, string revokedBy)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null && user.Admin)
                return false;

            if (softDelete)
            {
                user.RevokedOn = DateTime.UtcNow;
                user.RevokedBy = revokedBy;
            }
            else
            {
                _dataContext.Users.Remove(user);
            }

            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreUserAsync(string login)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
                return false;

            user.RevokedOn = null;
            user.RevokedBy = null;

            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
