using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WindowsAuthenication.Startup))]
namespace WindowsAuthenication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //AutomaticMigrationsEnabled = true;
            //SetSqlGenerator("Devart.Data.SQLite", new System.Data.SQLite.Entity.Migrations.SQLiteEntityMigrationSqlGenerator());
            ConfigureAuth(app);
        }
    }
}
