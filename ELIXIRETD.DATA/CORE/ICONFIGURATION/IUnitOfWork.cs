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

        Task CompleteAsync();
    }
}
