using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.REPOSITORIES
{
    public class RoleRepository : IRoleRepository
    {
        private new readonly StoreContext _context;
        public RoleRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<RoleDto>> GetAllActiveRoles()
        {
            var roles = _context.Roles.Where(x => x.IsActive == true)
                                      .Select(x => new RoleDto
                                      {
                                          Id = x.Id,
                                          RoleName = x.RoleName,
                                          AddedBy = x.AddedBy,
                                          DateAdded = x.DateAdded.ToString("MM/dd/yyyy")

                                      });
            return await roles.ToListAsync();

        }

        public async Task<IReadOnlyList<RoleDto>> GetAllInActiveRoles()
        {
            var roles = _context.Roles.Where(x => x.IsActive == false)
                                      .Select(x => new RoleDto
                                      {
                                          Id = x.Id,
                                          RoleName = x.RoleName,
                                          AddedBy = x.AddedBy,
                                          DateAdded = x.DateAdded.ToString("MM/dd/yyyy")

                                      });
            return await roles.ToListAsync();
        }



        public async Task<bool> AddNewRole(UserRole role)
        {
            await _context.Roles.AddAsync(role);
            return true;
        }

        public async Task<bool> UpdateRoleInfo(UserRole role)
        {
            var existingRole = await _context.Roles.Where(x => x.Id == role.Id)
                                                   .FirstOrDefaultAsync();

            existingRole.RoleName = role.RoleName;

            return true;

        }


        public async Task<bool> ActivateRole(UserRole role)
        {
            var roles = await _context.Roles.Where(x => x.Id == role.Id)
                                            .FirstOrDefaultAsync();

            roles.IsActive = true;

            return true;

        }

        public async Task<bool> InActiveRole(UserRole role)
        {
            var roles = await _context.Roles.Where(x => x.Id == role.Id)
                                          .FirstOrDefaultAsync();

            roles.IsActive = false;

            return true;
        }

        public async Task<bool> ValidateRoleExist(string role)
        {
            return await _context.Roles.AnyAsync(x => x.RoleName == role);
        }
    }
}
