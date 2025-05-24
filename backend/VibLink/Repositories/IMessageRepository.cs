using VibLink.Models.Entities;

namespace VibLink.Repositories
{
    public interface IMessageRepository : IMongoRepository<Message>
    {
    }
}
