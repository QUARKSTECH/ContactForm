using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactForm.API.Data;
using ContactForm.API.Dtos;
using ContactForm.API.Helpers.SMS;
using ContactForm.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ContactForm.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;

        public AuthController(IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ISmsService smsService)
        {
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _signInManager = signInManager;
            _smsService = smsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                var user = _mapper.Map<User>(userForRegisterDto);

                var result = await _userManager.CreateAsync(user, "Password@123");

                var userToReturn = _mapper.Map<UserDetailDto>(user);

                if (result.Succeeded)
                {
                    // return CreatedAtRoute("GetUser",
                    //     new { controller = "Users", id = user.Id }, userToReturn);
                    return StatusCode(201);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            //var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
            if (user != null && userForLoginDto.Password == null)
            {
                var random = new Random();
                var rndDigits = random.Next(1000, 9999);
                var enquiryDto = new EnquiryDto();
                enquiryDto.IsLogin = true;
                enquiryDto.ExtraProps.Add("otp",rndDigits);
                enquiryDto.ExtraProps.Add("mobile",userForLoginDto.UserName);
                var result = await _smsService.ReadAndModifyXMLFile(enquiryDto);
                user.OTP = rndDigits;
                await _userManager.UpdateAsync(user);
                return Ok(user);
            }
            if (user != null && userForLoginDto.Password != null && user.OTP.ToString() == userForLoginDto.Password)
            {
                return Ok(new {data = "success"});
            }

            // if (result.Succeeded)
            // {
            //     var appUser = await _userManager.Users.
            //         FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

            //     return Ok(new
            //     {
            //         token = GenerateJwtToken(appUser)
            //     });
            // }

            return Ok(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserName.ToLower() == "admin" ? "admin" : "user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}