using AutoMapper;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data.Core.Extensions;
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
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            IEnumerable<MemberDto> usersToReturn = await _userRepository.GetMembersAsync();
            return Ok(usersToReturn);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] MemberUpdateDto data)
        {
            AppUser user = await _userRepository.GetByUserNameAsync(User.GetUserName());

            _mapper.Map(data, user);

            _userRepository.Update(user);
            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user!");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            AppUser user = await _userRepository.GetByUserNameAsync(User.GetUserName());
            ImageUploadResult? result = await _photoService.AddPhotoAsync(file);
            if (result.Error is not null) return BadRequest(result.Error.Message);

            Photo photo = new()
            {
                PublicId = result!.PublicId,
                URL = result.SecureUrl!.AbsoluteUri
            };

            if (user.Photos?.Count == 0) photo.IsMain = true;

            user.Photos!.Add(photo);
            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new { userName = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-photo-main/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            AppUser user = await _userRepository.GetByUserNameAsync(User.GetUserName());

            Photo? photo = user.Photos?.FirstOrDefault(a => a.Id == photoId);

            if (photo!.IsMain) return BadRequest("This is already your main photo");

            Photo? currentMain = user.Photos?.FirstOrDefault(a => a.IsMain);
            if (currentMain is not null) currentMain.IsMain = false;

            photo.IsMain = true;

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");
        }
    }
}