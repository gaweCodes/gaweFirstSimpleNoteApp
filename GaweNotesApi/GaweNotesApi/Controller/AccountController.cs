using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GaweNotesApi.Dtos;
using GaweNotesApi.Services;

namespace GaweNotesApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService) =>_accountService = accountService;
        [HttpPost("signin")]
        public async Task<ActionResult<UserDto>> SignIn(SignInDto signInDto) => await _accountService.SignIn(signInDto);
        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> SignUp(SignUpDto dtoUser) => await _accountService.SignUp(dtoUser);
    }
}
