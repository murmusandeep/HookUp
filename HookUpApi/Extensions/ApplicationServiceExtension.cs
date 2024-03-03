using HookUpApi.Helpers;
using HookUpApi.Interfaces;
using HookUpBLL;
using HookUpBLL.Interfaces;
using HookUpDAL;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace HookUpApi.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("sqlConnection"), builder => builder.MigrationsAssembly("HookUpApi"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddScoped<IUsersBLL, UsersBLL>();
            services.AddScoped<IUsersDAL, UsersDAL>();
            services.AddScoped<IAccountBLL, AccountBLL>();
            services.AddScoped<IAccountDAL, AccountDAL>();
            services.AddScoped<IBuggyBLL, BuggyBLL>();
            services.AddScoped<IBuggyDAL, BuggyDAL>();
            services.AddScoped<IAppUserSeedBLL, AppUserSeedBLL>();
            services.AddScoped<IAppUserSeedDAL, AppUserSeedDAL>();
            services.AddScoped<ITokenHelper, TokenHelper>();

            return services;
        }
    }
}
