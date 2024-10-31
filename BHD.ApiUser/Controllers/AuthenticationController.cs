using BHD.ApiUser.Middlewares;
using BHD.Application.Dtos.Security;
using BHD.Application.Security.Authentication;
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
        public AuthenticationController(IJwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
        }

        [HttpPost, AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ResponseModel))]
        public IActionResult Login([FromBody] LoginModel model)
        {
            string email = model.Email;
            string password = model.Password;

            //falta logica para validar usuario
            if (!string.IsNullOrEmpty(email)  && !string.IsNullOrEmpty(password))
            {
                var token = _jwtFactory.GenerateEncodedToken(email);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
