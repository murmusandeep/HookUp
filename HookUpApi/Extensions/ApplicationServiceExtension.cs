using CloudinaryDotNet;
using Entities.Models;
using HookUpApi.Helpers;
using HookUpApi.Interfaces;
using HookUpApi.Middleware;
using HookUpBLL;
using HookUpBLL.Interfaces;
using HookUpDAL;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            services.AddScoped<IPhotoBLL, PhotoBLL>();
            services.AddScoped<IPhotoDAL, PhotoDAL>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
                return new Cloudinary(account);
            });

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<LogUserActivity>();

            return services;
        }
    }
}
