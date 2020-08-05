using CustomeModule.Services;
using Microsoft.Extensions.DependencyInjection;
using CustomeModule.Interfaces.Services.Interface;
using CustomeModule.Repository;
using CustomeModule.Interfaces.MailHelper.Interfaces;
using CustomeModule.MailHelper;

namespace CustomeModule.API
{
    public static class DICollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection repository)
        {
            //Repository registration
            repository.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return repository;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Interfaces registration 
            services.AddTransient<IGlobalServices, GlobalServices>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IUserRightService, UserRightService>();
			services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<IMailHandler, MailHandler>();
            services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}
