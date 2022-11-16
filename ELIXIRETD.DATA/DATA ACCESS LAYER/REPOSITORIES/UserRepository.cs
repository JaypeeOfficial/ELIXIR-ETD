using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Permissions;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.REPOSITORIES
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;

        public UserRepository(StoreContext context) 
        {
            _context = context;
        }

        public async Task<IReadOnlyList<UserDto>> GetAllActiveUsers()
        {
            return await _context.Users.Select(x => new UserDto
            {
                 Id = x.Id,
                 FullName = x.FullName, 
                 UserName = x.UserName, 
                 Password = x.Password,
                 UserRoleId = x.UserRoleId, 
                 UserRole = x.UserRole.RoleName, 
                 Department = x.Department.DepartmentName, 
                 DepartmentId = x.DepartmentId,
                 DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                 IsActive = x.IsActive 
            }).Where(x => x.IsActive == true)
              .ToListAsync();
            
        }

        public async Task<IReadOnlyList<UserDto>> GetAllInActiveUsers()
        {
            return await _context.Users.Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Password = x.Password,
                UserRoleId = x.UserRoleId,
                UserRole = x.UserRole.RoleName,
                Department = x.Department.DepartmentName,
                DepartmentId = x.DepartmentId,
                DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                IsActive = x.IsActive
            }).Where(x => x.IsActive == false)
            .ToListAsync();

        }

        public async Task<bool> AddNewUser(User user)
        {
            await _context.Users.AddAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserInfo(User user)
        {
            var existingUser = await _context.Users.Where(x => x.Id == user.Id)
                                              .FirstOrDefaultAsync();


            existingUser.FullName = user.FullName; 
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.UserRoleId = user.UserRoleId;
            existingUser.DepartmentId = user.DepartmentId;

            return true;

        }

        public async Task<bool> ActivateUser(User user)
        {
            var users = await _context.Users.Where(x => x.Id == user.Id)
                                            .FirstOrDefaultAsync();

            users.IsActive = true;

            return true;

        }

        public async Task<bool> InActiveUser(User user)
        {
            var users = await _context.Users.Where(x => x.Id == user.Id)
                                             .FirstOrDefaultAsync();

            users.IsActive = false;

            return true;
        }


        //--------------DEPARTMENT


        public async Task<IReadOnlyList<DepartmentDto>> GetAllActiveDepartment()
        {
            return await _context.Departments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                DepartmentName = x.DepartmentName,
                AddedBy = x.AddedBy,
                DateAdded = x.DateAdded.ToString("MM/dd/yyyy")

            }).Where(x => x.IsActive == true)
              .ToListAsync();
            
        }

        public async Task<IReadOnlyList<DepartmentDto>> GetAllInActiveDepartment()
        {
            return await _context.Departments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                DepartmentName = x.DepartmentName,
                AddedBy = x.AddedBy,
                DateAdded = x.DateAdded.ToString("MM/dd/yyyy")

            }).Where(x => x.IsActive == false)
           .ToListAsync();
        }

        public async Task<bool> AddNewDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            return true;
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
                var dep = await _context.Departments.Where(x => x.Id == department.Id)
                                                    .FirstOrDefaultAsync();

            dep.DepartmentName = department.DepartmentName;

            return true;

        }

        public async Task<bool> InActiveDepartment(Department department)
        {
           var dep = await _context.Departments.Where(x => x.Id == department.Id)
                                               .FirstOrDefaultAsync();

            dep.IsActive = false;

            return true;
        }

        public async Task<bool> ActivateDepartment(Department department)
        {
            var dep = await _context.Departments.Where(x => x.Id == department.Id)
                                           .FirstOrDefaultAsync();

            dep.IsActive = true;

            return true;
        }
    }
}
