using VibLink.Models.Entities;

namespace VibLink.Services.Internal
{
    public interface IUserDetailsService
    {
        Task<IEnumerable<UserDetails>> GetAllUserDetailsAsync();
    }
}
