using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Helpers;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public UsersController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserForLoginDto userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            var user = await _cityInfoRepository.UserExistAsync(userObj.userName);
            if(!user)
            {
                return NotFound(new {Message = "User Not Found"});
            }
            var pass = await _cityInfoRepository.GetUserAsync(userObj.userName);
            var isValid = PasswordHasher.VerifyPassword(userObj.password, pass.password);
            if (!isValid)
            {
                return NotFound(new { Message = "Wrong Password" });
            }

            return Ok(new {Message = "Login Success!"});



        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _cityInfoRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserWithoutCityDto>(user));

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForCreationDto userObj)
        {
            try
            {
                if (userObj == null)
                {
                    return BadRequest();
                }

                var user = await _cityInfoRepository.UserExistAsync(userObj.userName);
                if (user)
                {
                    return BadRequest(new { Message = "User Name Already Exist" });
                }

                var mail = await _cityInfoRepository.EmailExistAsync(userObj.email);

                if (mail)
                {
                    return BadRequest(new { Message = "Email Already Exist" });
                }
                var pass = _cityInfoRepository.CheckPasswordStrength(userObj.password);

                if (!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new { Message = pass });
                }
                userObj.password = PasswordHasher.HashPassword(userObj.password);

                var finalUSerDetails = _mapper.Map<User>(userObj);

                finalUSerDetails.token = "Test token";

                await _cityInfoRepository.AddNewUserAsync(finalUSerDetails);

                await _cityInfoRepository.SaveChangesAsync();

                return Ok(finalUSerDetails);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, new { Message = "An error occurred while registering the user.", Details = ex.Message });
            }
        }
    }
}
