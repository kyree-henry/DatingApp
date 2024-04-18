using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;
using DatingApp.API.Services.Interfaces;

namespace DatingApp.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.Users.Include(a => a.Photos).ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id) ?? new AppUser();
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            return await _context.Users.Include(a => a.Photos).SingleOrDefaultAsync(x => x.UserName == userName) ?? new AppUser();
        }

        public async Task<MemberDto?> GetMemberAsync(string userName)
        {
            return await _context.Users.Where(a => a.UserName == userName)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
