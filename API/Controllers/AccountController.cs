using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, ITokenService tokenService,IMapper mapper )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;

        }


        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
    
            var users = await _userManager.Users.ToListAsync();

            // Map AppUser entities to UserDto (you might need AutoMapper or a manual mapping here)
            var userDtos = users.Select(user => new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                //Role = user.Role,
        
            }).ToList();

            return Ok(userDtos);
}



        [HttpGet]
        [Authorize]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            
            var  user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                
            };
        }

        [HttpGet("emailexists")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            

            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(User);
            return _mapper.Map<Address, AddressDto>(user.Address);


        }

        [Authorize]
        [HttpPut("address")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(HttpContext.User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }

        [HttpPost("login")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                //Role = user.Role

            };

        }

       
        
        [HttpPost("register")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new [] {"Email address is in use"}});
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Role = "Korisnik"
                

            };

            var result = await _userManager.CreateAsync(user,registerDto.Password);

            if(!result.Succeeded) return BadRequest(new ApiResponse(400));


            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                Role = user.Role
            };
        }

        
    }
}