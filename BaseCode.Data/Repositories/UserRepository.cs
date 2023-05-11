using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> FindAll()
        {
            return GetDbSet<User>();
        }

        //return all users
        public ListViewModel FindAllUsers(UserAdminViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;
            var users = FindAll().OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = users.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = users.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(user => new {
                    id = user.Id,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    email = user.Address,
                    username = user.Username,
                    
                })
                .ToList();
            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };
            return new ListViewModel { Pagination = pagination, Data = results };
        }
        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.User.AdminID):
                    sortKey = "adminID";
                    break;

                case (Constants.User.FirstName):
                    sortKey = "fName";
                    break;

                case (Constants.User.LastName):
                    sortKey = "lName";
                    break;

                case (Constants.User.Email):
                    sortKey = "email";
                    break;

                default:
                    sortKey = "adminID";
                    break;
            }

            return sortKey;
        }

        public User FindByUsername(string username)
        {
            return GetDbSet<User>().Where(x => x.Username.ToLower().Equals(username.ToLower())).AsNoTracking().FirstOrDefault();
        }

        public User FindById(string id)
        {
            return GetDbSet<User>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public User FindUser(string userName)
        {
            var userDB = GetDbSet<User>().Where(x => x.Username.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            return userDB;
        }

        public bool Create(User user)
        {
            try
            {
                GetDbSet<User>().Add(user);
                UnitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        //edit admin/ASI_HR users
        public bool Update(UserUpdateViewModel user)
        {
            bool isUpdate = false;

            var userUpdate = FindById(user.AdminID);
            if (userUpdate != null)
            {
                userUpdate.Username = user.UserName;
                userUpdate.FirstName = user.FirstName;
                userUpdate.LastName = user.LastName;
                userUpdate.Address = user.EmailAddress;
                
                UnitOfWork.SaveChanges();

                isUpdate = true;
            }

            return isUpdate;
        }


        public void Delete(User user)
        {
            GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }
        public void DeleteById(string id)
        {
            var user = FindById(id);
            GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> RegisterUser(string username, string password, string firstName, string lastName, string email, string role)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return result;

            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(role);
            if (checkIfRoleExists)
            {
                var result1 = await _userManager.AddToRoleAsync(user, role);

                if (!result1.Succeeded) return result1;
            }
         
            var userId = user.Id;

            // Insert user details
            var userEntity = new User
            {
                Id = userId,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Address = email,
            };

            Create(userEntity);

            return result;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!checkIfRoleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
                return result;
            }

            return null;
        }        
        //can use in login
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<User> FindUserAsync(string userName, string password)
        {
            var userDB = GetDbSet<User>().Where(x => x.Username.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            var user = await _userManager.FindByNameAsync(userName);
            var isPasswordOK = await _userManager.CheckPasswordAsync(user, password);
            if ((user == null) || (isPasswordOK == false))
            {
                userDB = null;
            }
            return userDB;
        }
    }
}
