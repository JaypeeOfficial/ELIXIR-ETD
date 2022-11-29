using ELIXIRETD.DATA.CORE.ICONFIGURATION;
using ELIXIRETD.DATA.CORE.INTERFACES.USER_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.REPOSITORIES;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.SERVICES
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreContext _context;

        public IUserRepository Users { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public IModuleRepository Modules { get; private set; }

        public UnitOfWork(StoreContext context)
  
        {
            _context = context;

            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
            Modules = new ModuleRepository(_context);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }

}
