using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.SETUP_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.USER_MODEL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT
{
    public  class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> Roles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserRoleModules> RoleModules { get; set; }
        public virtual DbSet<MainMenu> MainMenus { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Uom> Uoms { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DevConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
