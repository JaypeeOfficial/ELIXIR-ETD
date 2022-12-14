using ELIXIRETD.DATA.CORE.INTERFACES.SETUP_INTERFACE;
using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.CORE.ICONFIGURATION
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IModuleRepository Modules { get; }

        IUomRepository Uoms { get; }

        IMaterialRepository Materials { get; }

        ISupplierRepository Suppliers { get; }
        

        Task CompleteAsync();
    }
}
