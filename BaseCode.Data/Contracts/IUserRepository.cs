using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCode.Data.Contracts
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
        User FindById(string id);
        string GetSortKey(string sortBy);
        User FindUser(string UserName);
        ListViewModel FindAllUsers(UserAdminViewModel searchModel);
        IQueryable<User> FindAll();
        bool Create(User user);
        bool Update(UserUpdateViewModel user);
        void Delete(User user);
        void DeleteById(string id);
        Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<User> FindUserAsync(string userName, string password);
    }
}
