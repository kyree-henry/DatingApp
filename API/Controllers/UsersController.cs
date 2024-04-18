using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;
using DatingApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            IEnumerable<MemberDto> usersToReturn = await _userRepository.GetMembersAsync();
            return Ok(usersToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            AppUser? user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<MemberDto>(user);
        }
        
        [HttpGet("{userName}")]
        public async Task<ActionResult<MemberDto?>> GetUser(string userName)
        {
            return await _userRepository.GetMemberAsync(userName);
        }

    }
} 