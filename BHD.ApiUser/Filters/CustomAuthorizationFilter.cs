using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BHD.ApiUser.Filters
{
    public class CustomAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Console.WriteLine(context.HttpContext.Response.StatusCode);
            //if (context.HttpContext.User.Identity?.IsAuthenticated == false)
            //{
            //    // Formatea la respuesta para 401 Unauthorized
            //    context.Result = new Result(new
            //    {
            //        StatusCode = (int)HttpStatusCode.Unauthorized,
            //        Message = "Unauthorized access - please provide valid credentials."
            //    })
            //    {
            //        StatusCode = (int)HttpStatusCode.Unauthorized
            //    };
            //}
            //else if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            //{
            //    // Formatea la respuesta para 403 Forbidden
            //    context.Result = new JsonResult(new
            //    {
            //        StatusCode = (int)HttpStatusCode.Forbidden,
            //        Message = "You do not have permission to access this resource."
            //    })
            //    {
            //        StatusCode = (int)HttpStatusCode.Forbidden
            //    };
            //}

            await Task.CompletedTask;
        }
    }
}