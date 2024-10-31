using BHD.ApiUser.Middlewares;
using BHD.Application.Dtos.User;
using BHD.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;

namespace BHD.ApiUser.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(CreatedUserDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ResponseModel))]


        public IActionResult Post([FromBody] UserDto model)
        {
            
            CreatedUserDto createdUser = _userService.Create(model);

            return Created("api/user", createdUser);

            
            //return BadRequest(ex.Message);
        }
    }
}
