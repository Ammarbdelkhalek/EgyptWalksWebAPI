using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MWalksProject.API.CustomMiddelwarre
{
    public class ExceptionHandlerMiddelWar
    {
        private  readonly ILogger<ExceptionHandlerMiddelWar> Logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddelWar(ILogger<ExceptionHandlerMiddelWar> logger, RequestDelegate next)
        {
            Logger = logger;
            this.next = next;
        }

        public async  Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

            }catch(Exception ex)
            {
                var ErrorId = Guid.NewGuid().ToString();
                Logger.LogError(ex,$"{ErrorId}{ex.Message}");
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var Error = new { id = ErrorId ,ErrorMessage= "Something went wrong!!" };

            await context.Response.WriteAsJsonAsync(Error);
            }
        }
    }
}
