using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            //builder.Services.AddSingleton<IUserRepository, UserRepository>();
            //builder.AddProfileService<CustomProfileService>();
            //builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
