using AutoMapper;
using VibLink.Models.Entities;
using VibLink.Repositories;

namespace VibLink.Services.Internal.Implementors
{
    public class UserDetailsServiceImpl : IUserDetailsService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;

        public UserDetailsServiceImpl(
            IUserDetailsRepository userDetailsRepository,
            IMapper mapper
            )
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDetails>> GetAllUserDetailsAsync()
        {
            return await _userDetailsRepository.FindAllAsync();
        }
    }
}
