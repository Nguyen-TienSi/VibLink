using Microsoft.EntityFrameworkCore;
using VibLink.Data;
using VibLink.Models.Settings;
using VibLink.Repositories;
using VibLink.Repositories.Implementors;
using VibLink.Services.Internal;
using VibLink.Services.Internal.Implementors;

namespace VibLink.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories DI
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepositoryImpl<>));
            services.AddScoped<IUserDetailsRepository, UserDetailsRepositoryImpl>();
            services.AddScoped<IConversationRepository, ConversationRepositoryImpl>();
            services.AddScoped<IMessageRepository, MessageRepositoryImpl>();
            services.AddScoped<IFriendshipRepository, FriendshipRepositoryImpl>();
            // Service DI
            services.AddScoped<IUserDetailsService, UserDetailsServiceImpl>();
            services.AddScoped<IConversationService, ConversationServiceImpl>();
            services.AddScoped<IMessageService, MessageServiceImpl>();
            services.AddScoped<IFriendshipService, FriendshipServiceImpl>();
            return services;
        }

        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<VibLinkDbContext>();
            services.Configure<MongoDbSetting>(configuration.GetSection(nameof(MongoDbSetting)));
            return services;
        }
    }
}
