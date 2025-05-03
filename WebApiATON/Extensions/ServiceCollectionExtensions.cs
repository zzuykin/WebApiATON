

using WebApiATON.Features.Managers;
using WebApiATON.Features.ManagersInterfaces;

namespace WebApiATON.Extensions
{
    public static class  ServiceCollectionExtensions
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();

        }
    }
}
