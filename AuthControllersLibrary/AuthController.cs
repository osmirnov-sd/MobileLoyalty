
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POCOModels.Messages.Requests;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthControllersLibrary
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;


        public AccountController(
                SignInManager<IdentityUser> signInManager,
                UserManager<IdentityUser> userManager,
                IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [Route("Registration")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Registration([FromBody] AuthRequest model)
        {
            try
            {
                model.PhoneNumber = PhoneConvert(model.PhoneNumber);

                var user = new IdentityUser
                {
                    UserName = model.PhoneNumber,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.PhoneNumber + "@M.ru"
                };

                var exist = await _signInManager.UserManager.FindByNameAsync(model.PhoneNumber);
                if (exist != null)
                {
                    return new NotFoundObjectResult("Пользователь с таким номером телефона уже зарегистрирован");
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded) return new NotFoundObjectResult("Пользователь с таким номером телефона уже зарегистрирован");

                await _signInManager.SignInAsync(user, false);


                if ((await _userManager.GetClaimsAsync(user)).All(x => x.Type != "User"))
                {
                    await _signInManager.UserManager.AddClaimAsync(user, new Claim("User", model.PhoneNumber));
                };
                var token = await GenerateJwtToken(model.PhoneNumber, user);
                return new OkObjectResult(new { Token = token });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Ошибка, обратитесь в тех. поддержку");
            }
        }

        [Route("Authorization")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] AuthRequest model)
        {
            model.PhoneNumber = PhoneConvert(model.PhoneNumber);
            var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, false, false);

            if (!result.Succeeded)
            return new NotFoundObjectResult("Неверный пароль, если не помните свой пароль, воспользуйтесь функцией «Забыл пароль» ");
               
            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.PhoneNumber);


            if ((await _userManager.GetClaimsAsync(appUser)).All(x => x.Type != "User")) return BadRequest("Пользователь не найден");

            var token = await GenerateJwtToken(model.PhoneNumber, appUser);

            return new OkObjectResult(new { Token = token});
        }


        private static string PhoneConvert(string modelPhoneNumber)
        {
            modelPhoneNumber = modelPhoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            if (string.IsNullOrEmpty(modelPhoneNumber)) return "";
            if (modelPhoneNumber[0] == '8') return '7' + modelPhoneNumber.Substring(1);
            if (modelPhoneNumber[0] == '+') return '7' + modelPhoneNumber.Substring(2);
            return modelPhoneNumber;
        }

        private async Task<object> GenerateJwtToken(string telephone, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, telephone),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("MacDocId", (await _userManager.GetClaimsAsync(user)).FirstOrDefault(x=>x.Type=="User")?.Value)
            };
            var a = await _userManager.GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}