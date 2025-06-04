using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using VibLink.Data;
using VibLink.Helpers;
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
            RegisterRepositories(services);
            RegisterServices(services);
            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepositoryImpl<>));
            services.AddScoped<IUserDetailsRepository, UserDetailsRepositoryImpl>();
            services.AddScoped<IConversationRepository, ConversationRepositoryImpl>();
            services.AddScoped<IMessageRepository, MessageRepositoryImpl>();
            services.AddScoped<IFriendshipRepository, FriendshipRepositoryImpl>();
            services.AddScoped<IFileStorageRepository, FileStorageRepositoryImpl>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserDetailsService, UserDetailsServiceImpl>();
            services.AddScoped<IConversationService, ConversationServiceImpl>();
            services.AddScoped<IMessageService, MessageServiceImpl>();
            services.AddScoped<IFriendshipService, FriendshipServiceImpl>();
            services.AddScoped<IAuthService, AuthServiceImpl>();
            services.AddScoped<IFileStorageService, FileStorageServiceImpl>();
        }

        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSetting>(configuration.GetSection(nameof(MongoDbSetting)));

            services.AddSingleton<MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSetting>>().Value;
                return new MongoClient(settings.ConnectionURI);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSetting>>().Value;
                var client = sp.GetRequiredService<MongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddSingleton<GridFSBucket>(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();
                return new GridFSBucket(database);
            });

            services.AddSingleton<VibLinkDbContext>();

            return services;
        }

        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextManager>();
            return services;
        }
    }
}
