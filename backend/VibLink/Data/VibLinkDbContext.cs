using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using VibLink.Models.Entities;
using VibLink.Models.Enums;
using VibLink.Models.Settings;

namespace VibLink.Data
{
    public class VibLinkDbContext(IMongoDatabase database)
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            database.GetCollection<T>(collectionName);

        public void SeedUserDetails()
        {
            var collection = GetCollection<UserDetails>("user_details");
            var count = collection.CountDocuments(FilterDefinition<UserDetails>.Empty);
            if (count == 0)
            {
                var users = new List<UserDetails>
                {
                    new() {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = "hashedpassword1",
                        PictureUrl = "https://example.com/john.jpg",
                        UserRoles = [UserRole.USER],
                        LastLogin = DateTime.UtcNow,
                        Friends = [],
                        BlockedUsers = []
                    },
                    new() {
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        PasswordHash = "hashedpassword2",
                        PictureUrl = "https://example.com/jane.jpg",
                        UserRoles = [UserRole.ADMIN],
                        LastLogin = DateTime.UtcNow,
                        Friends = [],
                        BlockedUsers = []
                    }
                };
                collection.InsertMany(users);
            }
        }
    }
}
