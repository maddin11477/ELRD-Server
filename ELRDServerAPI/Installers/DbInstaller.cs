using ELRDDataAccessLibrary.DataAccess;
using ELRDServerAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELRDServerAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Add Database Context for accessing the Database
            services.AddDbContext<ELRDContext>(options =>
            {
#if DEBUG
                options.UseSqlServer(configuration.GetConnectionString("Local"));
#else
                options.UseSqlServer(configuration.GetConnectionString("Default"));
#endif
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ELRDContext>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
