using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.HELPERS;
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

        public async Task<PagedList<RoleDto>> GetAllRoleWithPagination(bool status, UserParams userParams)
        {
            var role = _context.Roles.Where(x => x.IsActive == status)
                                     .Select(x => new RoleDto
                                     {
 
                                        RoleName = x.RoleName,
                                        AddedBy = x.AddedBy, 
                                        DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                        IsActive = x.IsActive

                                     });

            return await PagedList<RoleDto>.CreateAsync(role, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<RoleDto>> GetAllRoleWithPaginationOrig(UserParams userParams, bool status, string search)
        {
            var role = _context.Roles.Where(x => x.IsActive == status)
                                   .Select(x => new RoleDto
                                   {

                                       RoleName = x.RoleName,
                                       AddedBy = x.AddedBy,
                                       DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                       IsActive = x.IsActive

                                   }).Where(x => x.RoleName.ToLower()
                                     .Contains(search.Trim().ToLower()));

            return await PagedList<RoleDto>.CreateAsync(role, userParams.PageNumber, userParams.PageSize);
        }
    }
}
