using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopFashion.Common;
using ShopFashion.Common.Models;
using ShopFashion.Model;
using ShopFashion.Model.NotMapping;
using ShopFashion.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Repository.Classes
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(UserRegisterModel userRegisterModel);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> FindUserAsync(string userName);
        ApplicationUser FindUser(string userName);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        ApplicationUser FindUserByEmail(string email);
        Task<IdentityResult> ChangePassword(string userName, string currentPassword, string newPassword);
       
        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        IList<string> GetUserRole(string userId);
        string HashPassword(string password);
        Task<IdentityResult> AccessFailedAsync(Guid userId);
        Task<bool> IsLockedOutAsync(Guid userId);
        Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId);
        Task<ApplicationUser> CheckPasswordAsync(string userName, string password);
        Task<IdentityResult> UpdatePassword(string userName, string password);
        Task<ApplicationUser> FindUserByIdAsync(Guid Id);
        Task<IdentityResult> SetPassword(ApplicationUser user, string password);

        Task<List<Guid>> GetRolesOfUser(Guid userId);
        void ResetLockTime(Guid userId);
        List<ApplicationUser> FindAdmin();
        int CountNumberOfTechnologyProvider();
        Guid FindRoleIdOfTechProvider();
    }

    public class AuthRepository : IAuthRepository, IDisposable
    {
        private UserManager<ApplicationUser, Guid> _userManager;
        private RoleManager<ApplicationRole, Guid> _roleManager;

        public AuthRepository(DbContext context)
        {
            _userManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ShopFashionContext()));
            _roleManager = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new ShopFashionContext()));
        }

        public void CreateUserManager()
        {
            _userManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ShopFashionContext()));
            _roleManager = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new ShopFashionContext()));
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterModel userRegisterModel)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.Email,
                IsActive = false,
                EmailConfirmed = false,
                LockoutEnabled = true,
                LockoutEndDateUtc = DateTime.UtcNow.AddYears(200),
                ExpireDate = DateTime.UtcNow.AddYears(10),
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                // Mappling user and role
                await _userManager.AddToRoleAsync(user.Id, Role.TechnologyProvider);
            }
            return result;
        }

        public string HashPassword(string password)
        {
            return _userManager.PasswordHasher.HashPassword(password);
        }

        public async Task<IdentityResult> AccessFailedAsync(Guid userId)
        {
            return await _userManager.AccessFailedAsync(userId);
        }

        public async Task<bool> IsLockedOutAsync(Guid userId)
        {
            return await _userManager.IsLockedOutAsync(userId);
        }

        public async Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId)
        {
            return await _userManager.ResetAccessFailedCountAsync(userId);
        }

        public async Task<ApplicationUser> CheckPasswordAsync(string userName, string password)
        {
            CreateUserManager();
            var user = await _userManager.FindByEmailAsync(userName);
            var isMatch = await _userManager.CheckPasswordAsync(user, password);
            if (isMatch)
            {
                return user;
            }

            return null;
        }

        public async Task<IdentityResult> UpdatePassword(string userName, string password)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user != null)
            {
                return await SetPassword(user, password);
            }

            return null;
        }

        public async Task<IdentityResult> ChangePassword(string userName, string currentPassword, string newPassword)
        {
            CreateUserManager();
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, currentPassword))
            {
                return await SetPassword(user, newPassword);
            }
            return null;
        }
        

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<ApplicationUser> FindUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            // TungKT: Fix bug related to deleted account still login
            //if (user == null || user.IsDeleted == true)
            //{
            //    // User is removed from the system
            //    return null;
            //}

            return user;
        }

        public ApplicationUser FindUser(string userName)
        {
            var user = _userManager.FindByName(userName);

            return user;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            //if (user == null || user.IsDeleted == true)
            //{
            //    // User is removed from the system
            //    return null;
            //}

            return user;
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            var user = _userManager.FindByEmail(email);

            return user;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            return user;
        }

        public IList<string> GetUserRole(string userId)
        {
            return _userManager.GetRoles(Guid.Parse(userId));
        }
        
        


        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(Guid.Parse(userId), login);

            return result;
        }

        public async void ResetLockTime(Guid userId)
        {
            await _userManager.SetLockoutEndDateAsync(userId, new DateTimeOffset(DateTime.UtcNow.AddSeconds(-1)));
            await _userManager.ResetAccessFailedCountAsync(userId);
        }

        public List<ApplicationUser> FindAdmin()
        {
            var idRoleAdmin = _roleManager.Roles.FirstOrDefault(x => x.Name == Common.Role.Admin);

            if (idRoleAdmin != null && idRoleAdmin.Users != null && idRoleAdmin.Users.Any())
            {
                List<Guid> adminIds = idRoleAdmin.Users.Select(x => x.UserId).ToList();

                var admins = _userManager.Users.Where(x => x.IsDeleted == false && adminIds.Any(y => y == x.Id)).ToList();

                return admins;
            }
            else
            {
                return null;
            }
        }

        
        public int CountNumberOfTechnologyProvider()
        {
            var idRoleTechnologyProvider = _roleManager.Roles.Where(x => x.Name == Role.TechnologyProvider).Select(x => x.Id).ToList();

            if (idRoleTechnologyProvider != null && idRoleTechnologyProvider.Any())
            {
                var numInternalUsers = _userManager.Users.Where(x => !x.IsDeleted
                                                               && x.IsActive
                                                               && x.Roles.Any(y => idRoleTechnologyProvider.Contains(y.RoleId)))
                                                         .Count();

                return numInternalUsers;
            }
            else
            {
                return 0;
            }
        }

        public Guid FindRoleIdOfTechProvider()
        {
            var idRoleTechProvider = _roleManager.Roles.Where(x => x.Name == Role.TechnologyProvider).Select(x => x.Id).FirstOrDefault();

            return idRoleTechProvider;
        }

        #region Role Manager
        public async Task<IdentityResult> CreateRole(ApplicationRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task<IdentityResult> UpdateRole(ApplicationRole role)
        {
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
        public async Task<IdentityResult> DeleteRole(ApplicationRole role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }
        public ResponseBaseModel GetRoles(int skip = 0, int take = 10)
        {
            ResponseBaseModel result = new ResponseBaseModel();
            result.Data.total = _roleManager.Roles.Count();
            result.Data.data = _roleManager.Roles.OrderBy(s => s.Name).Skip(0).Take(10).ToList();
            return result;
        }

        public async Task<List<Guid>> GetRolesOfUser(Guid userId)
        {
            List<Guid> result = new List<Guid>();

            var roles = await _userManager.GetRolesAsync(userId);
            foreach (var role in roles)
            {
                result.Add(_roleManager.Roles.FirstOrDefault(x => x.Name == role).Id);
            }
            return result;
        }
        #endregion

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
        }

        #region private

        public async Task<IdentityResult> SetPassword(ApplicationUser user, string password)
        {
            if (await _userManager.HasPasswordAsync(user.Id))
            {
                await _userManager.RemovePasswordAsync(user.Id);
            }

            var addPwd = await _userManager.AddPasswordAsync(user.Id, password);
            await _userManager.UpdateAsync(user);
            return addPwd;
        }

        #endregion private
    }
}
