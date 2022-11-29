using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.USER_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE
{
    public interface IModuleRepository
    {
        Task<IReadOnlyList<ModuleDto>> GetAllActiveModules();
        Task<IReadOnlyList<ModuleDto>> GetAllInActiveModules();
        Task<bool> AddNewModule(Module module);
        Task<bool> UpdateModule(Module module);



    }


}
