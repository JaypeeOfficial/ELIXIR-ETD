using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.REPOSITORIES
{
    public class ModuleRepository : IModuleRepository
    {
        private new readonly StoreContext _context;

        public ModuleRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ModuleDto>> GetAllActiveModules()
        {
            var module = _context.Modules.Where(x => x.IsActive == true)
                                         .Select(x => new ModuleDto
                                         {
                                             Id = x.Id,
                                             MainMenu = x.MainMenu.ModuleName,
                                             MainMenuId = x.MainMenuId,
                                             SubMenuName = x.SubMenuName,
                                             ModuleName = x.ModuleName,
                                             DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                             AddedBy = x.AddedBy,
                                             IsActive = x.MainMenu.IsActive,
                                             Reason = x.Reason
                                         });

            return await module.ToListAsync();

        }

        public async Task<IReadOnlyList<ModuleDto>> GetAllInActiveModules()
        {
            var module = _context.Modules.Where(x => x.IsActive == false)
                                        .Select(x => new ModuleDto
                                        {
                                            Id = x.Id,
                                            MainMenu = x.MainMenu.ModuleName,
                                            MainMenuId = x.MainMenuId,
                                            SubMenuName = x.SubMenuName,
                                            ModuleName = x.ModuleName,
                                            DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                            AddedBy = x.AddedBy,
                                            IsActive = x.MainMenu.IsActive,
                                            Reason = x.Reason
                                        });

            return await module.ToListAsync();

        }


        public async Task<bool> AddNewModule(Module module)
        {
            await _context.Modules.AddAsync(module);

            return true;
        }


        public async Task<bool> UpdateModule(Module module)
        {
            var existingModule = await _context.Modules.Where(x => x.Id == module.Id)
                                                       .FirstOrDefaultAsync();

            existingModule.MainMenuId = module.MainMenuId;
            existingModule.SubMenuName = module.SubMenuName;
            existingModule.ModuleName = module.ModuleName;

            return true;

        }
    }
}
