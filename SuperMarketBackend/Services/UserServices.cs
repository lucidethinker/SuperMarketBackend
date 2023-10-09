using Microsoft.EntityFrameworkCore;
using SuperMarketBackend.Data;
using SuperMarketBackend.Enum;

namespace SuperMarketBackend.Services
{
    public class UserServices
    {
        private readonly SuperMrktDbContext _context;
        public UserServices()
        {
                _context = new SuperMrktDbContext();
        }

        public User? Register(User user)
        {
            if (_context.Users.FirstOrDefault(u => (u.UserName == user.UserName || u.Email == user.Email) && !u.IsDeleted) != null)
                return null;
            user.UserType = (int) UserTypeEnum.Cashier ;
            _context.Users.Add(user);
            _context.SaveChanges();
            return _context.Users.OrderBy(u => u.UserId).Last();
        }

        public User? Login(User user)
        {
            var _user = _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password && !u.IsDeleted && u.IsActive);
            if ( _user != null)
                return _user;
            else 
                return null;
        }

        public async Task<User?> UpdateUser(User user)
        {
            var _user = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if ( _user == null)
                return null;
            if (_context.Users.FirstOrDefault(u => (u.UserName == user.UserName || u.Email == user.Email) && (!u.IsDeleted && u.UserId != _user.UserId)) != null)
                return null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && !u.IsDeleted);

            if (user != null)
            {
                user.IsDeleted = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeStatusUser(int userId,int? status)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && !u.IsDeleted);
            
            if (user != null)
            {
                user.IsActive = status!= null ? !user.IsActive : user.IsActive;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public User? GetUser(int userId = 0,string userName = "")
        {
            if (userId != 0)
                return _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (userName != "")
                return _context.Users.FirstOrDefault(u => u.UserName == userName);
            return null;
        }

        public List<User> GetAllUsers(string? search = "" , bool orderBydesc = false)
        {
            var users = _context.Users.Where(u => !u.IsDeleted && u.UserType != 0).ToList();
            if (search != "")
                users = users.Where(u => EF.Functions.Like(u.UserName, $"%{search}%")).ToList();
            if (orderBydesc)
               return users.OrderByDescending(u => u.UserName).ToList();
            else
               return users.OrderBy(u => u.UserId).ToList();            
        }       
    }
}
