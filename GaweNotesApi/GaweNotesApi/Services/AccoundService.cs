using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GaweNotesApi.Dtos;
using GaweNotesApi.Models;
using GaweNotesApi.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GaweNotesApi.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthOptions _authOption;
        public AccountService(UserManager<ApplicationUser> userManager, IOptions<AuthOptions> authOption, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _authOption = authOption.Value;
            _signInManager = signInManager;
        }
        public async Task<UserDto> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Username,
                FirstName = signUpDto.Firstname,
                LastName = signUpDto.Lastname
            };
            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded) throw new InvalidOperationException("Error during sign up the user " + string.Join(", ", result.Errors.Select(x => x.Description)));
            var token = GenerateJwtToken(user.Email);
            return new UserDto {Fullname = $"{user.FirstName} {user.LastName}", Id = user.Id, JwtToken = token};
        }
        public async Task<UserDto> SignIn(SignInDto signInDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == signInDto.Username);
            if (user == null) throw new ArgumentException("The entered username or password is wrong");
            var result = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, false);
            if(!result.Succeeded) throw new ArgumentException("The entered username or password is wrong");
            var token = GenerateJwtToken(user.Email);
            return new UserDto{Fullname = $"{user.FirstName} {user.LastName}", Id = user.Id, JwtToken = token };
        }
        private string GenerateJwtToken(string user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user),
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_authOption.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(_authOption.ExpiresHours);
            var token = new JwtSecurityToken(
                _authOption.Issuer,
                _authOption.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
