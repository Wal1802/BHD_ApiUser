using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Net;
using System.IO;
using System.Linq;
using BHD.ApiUser.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BHD.ApiUser.Middlewares
{
    public class ResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseHandlerMiddleware> _logger;

        public ResponseHandlerMiddleware(RequestDelegate next, ILogger<ResponseHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            

            try
            {
                if (!context.Request.Headers.ContainsKey("Content-Type") || !context.Request.Headers["Content-Type"].Contains("application/json"))
                    throw new UnsupportedMediaTypeException();

                await _next(context);

                

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var responseModel = new ResponseModel();

            switch (exception)
            {
                case ApplicationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Mensaje = ex.Message;
                    break;
                case FluentValidation.ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Mensaje = ex.Message;
                    break;
                case UnsupportedMediaTypeException ex:
                    response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    responseModel.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    responseModel.Mensaje = ex.Message;
                    break;
                case UnauthorizedAccessException ex:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseModel.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseModel.Mensaje = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Mensaje = "Internal server error!";
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(responseModel);
            await context.Response.WriteAsync(result);
        }
    }
}