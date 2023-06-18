using Microsoft.AspNetCore.Builder;

namespace AdminApp.API
{
	public static class AppBuilderExtensions
	{
		public static void ConfigureExceptionHandling(this IApplicationBuilder app)
		{
			app.UseMiddleware<ErrorHandlerMiddleware>();

			//app.UseExceptionHandler(appError =>
			//{
			//    appError.Run(async context =>
			//    {
			//        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
			//        var logger = appError.ApplicationServices.GetService<IErrorLogger>();

			//        Exception exception = contextFeature.Error;

			//        logger?.LogError(exception);

			//        context.Response.ContentType = "application/json";
			//        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			//        await context.Response.WriteAsync(exception.Message);
			//    });
			//});
		}
	}
}
