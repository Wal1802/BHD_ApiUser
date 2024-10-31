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
using System.Text;

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

                if(context.Response.StatusCode > 299)
                {
                    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        throw new UnauthorizedAccessException("No tienes autorización para realizar esta acción");
                    else throw new Exception("Internal server error");
                }

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
    ///// <summary>
    ///// middleware para manejar las respuestas de la API
    ///// </summary>
    //public class ResponseHandlerMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    /// <param name="next"></param>
    //    public ResponseHandlerMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    /// <summary>
    //    /// captura las respuestas de la API y las formatea
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public async Task Invoke(HttpContext context)
    //    {
    //        var originalResponseBodyStream = context.Response.Body;
    //        var memoryStream = new MemoryStream();
    //        try
    //        {
    //            ////para solo aceptar JSON
    //            if (!context.Request.Headers.ContainsKey("Content-Type") || !context.Request.Headers["Content-Type"].Contains("application/json"))

    //                throw new UnsupportedMediaTypeException();

    //            context.Response.Body = memoryStream;

    //            await _next(context);



    //            if (context.Response.StatusCode >= 200 && context.Response.StatusCode <= 299)
    //            {
    //                memoryStream.Seek(0, SeekOrigin.Begin);
    //                var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

    //                var formattedResponse = new
    //                {
    //                    StatusCode = context.Response.StatusCode,
    //                    Data = JsonSerializer.Deserialize<object>(responseBody)
    //                };

    //                context.Response.Body = originalResponseBodyStream;
    //                context.Response.ContentType = "application/json";
    //                context.Response.ContentLength = null; // Evita problemas de longitud

    //                await context.Response.WriteAsync(JsonSerializer.Serialize(formattedResponse), Encoding.UTF8);
    //            }
    //            else
    //            {
    //                memoryStream.Seek(0, SeekOrigin.Begin);
    //                await memoryStream.CopyToAsync(originalResponseBodyStream);
    //                context.Response.Body = originalResponseBodyStream;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            await HandleExceptionAsync(context, ex);
    //        }
    //        finally
    //        {
    //            memoryStream.Dispose();
    //        }
    //    }

    //    /// <summary>
    //    /// captura las excepciones y las formatea
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <param name="exception"></param>
    //    /// <returns></returns>
    //    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    //    {
    //        context.Response.ContentType = "application/json";
    //        //var response = context.Response;
    //        var responseModel = new ResponseModel();
    //        switch (exception)
    //        {
    //            case ApplicationException ex:
    //                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
    //                responseModel.Mensaje = ex.Message;
    //                break;
    //            case FluentValidation.ValidationException ex:
    //                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
    //                responseModel.Mensaje = ex.Message;
    //                break;
    //            case UnsupportedMediaTypeException ex:
    //                context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
    //                responseModel.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
    //                responseModel.Mensaje = ex.Message;
    //                break;
    //            default:
    //                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //                responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
    //                responseModel.Mensaje = "Internal Server Error";
    //                break;
    //        }

    //        var payload = JsonSerializer.Serialize(responseModel);

    //        return context.Response.WriteAsync(payload, Encoding.UTF8);

    //    }
    //}
}