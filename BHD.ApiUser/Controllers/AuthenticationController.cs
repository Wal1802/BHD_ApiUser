using BHD.ApiUser.Middlewares;
using BHD.Application.Dtos.Security;
using BHD.Application.Security.Authentication;
using BHD.Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BHD.ApiUser.Controllers
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IUserService _userService;
        public AuthenticationController(
            IJwtFactory jwtFactory,
            IUserService userService)
        {
            _jwtFactory = jwtFactory;
            _userService = userService;
        }

        [HttpPost, AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(string))]
        public IActionResult Login([FromBody] LoginModel model)
        {
            bool isValid = _userService.Login(model);

            if(!isValid)
                return Unauthorized("Usuario o contraseña incorrectos");

            string token = _jwtFactory.GenerateEncodedToken(model.Email);
            return Ok(token);
            
        }

        [HttpPut(Name ="ByPassLogin"), AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseModel))]
        public IActionResult ByPassLogin([FromBody] LoginModel model)
        {

            string token = _jwtFactory.GenerateEncodedToken("string");
            return Ok(token);

        }
    }
}
