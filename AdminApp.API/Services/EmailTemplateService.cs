using AdminApp.API.Options;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly AuthenticationOptions _authenticationOptions;

        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public EmailTemplateService(
            IOptionsSnapshot<AuthenticationOptions> authenticationOptions,
            IRazorViewEngine razorViewEngine,
            IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider)
        {
            _authenticationOptions = authenticationOptions?.Value;
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async ValueTask BuildActivationEmailMessageContent(MimeMessage mailMessage, ApplicationUser user, string encodedToken)
        {
            await BuildMessageBody(mailMessage, user, encodedToken, "ActivationEmailTemplate.cshtml", "activateuser");
        }

        public async ValueTask BuildResetEmailMessageContent(MimeMessage mailMessage, ApplicationUser user, string encodedToken)
        {
            await BuildMessageBody(mailMessage, user, encodedToken, "ResetEmailTemplate.cshtml", "resetpassword");
        }

        public async ValueTask BuildMessageBody(MimeMessage message, ApplicationUser user, string encodedToken, string template, string path)
        {
            var callbackUrl = new UriBuilder(_authenticationOptions.Issuer)
            {
                Path = path,
                Query = $"id={user.Id}&token={encodedToken}"
            };

            EmailTemplateModel emailTemplateDetails = new()
            {
                Username = MailboxAddress.Parse(user.UserName),
                Url = callbackUrl.ToString()
            };


            BodyBuilder bodyBuilder = new();
            EmbeddedFileProvider embeddedProvider = new(Assembly.GetExecutingAssembly());
            ContentType jpegContentType = ContentType.Parse(System.Net.Mime.MediaTypeNames.Image.Jpeg);

            string imagePath = @"Resources\Image1.png";

            using (var stream = embeddedProvider.GetFileInfo(imagePath).CreateReadStream())
            {
                MimeEntity mimeEntity = bodyBuilder.LinkedResources.Add(imagePath, stream, jpegContentType);

                mimeEntity.ContentId = MimeUtils.GenerateMessageId();

                emailTemplateDetails.Image1ID = mimeEntity.ContentId;
            }

            bodyBuilder.HtmlBody = await GetTemplateHtmlAsStringAsync(template, emailTemplateDetails);
            bodyBuilder.TextBody = GetEmailPlainTextBody(emailTemplateDetails);

            message.Body = bodyBuilder.ToMessageBody();
        }

        private async Task<string> GetTemplateHtmlAsStringAsync<T>(string template, T model)
        {
            var viewResult = _razorViewEngine.GetView("~/", $"~/Resources/Templates/{template}", false);

            if (!viewResult.Success)
            {
                throw new InvalidOperationException($"{template} cound not be found.");
            }

            DefaultHttpContext httpContext = new()
            {
                RequestServices = _serviceProvider
            };

            ActionContext actionContext = new(httpContext, new RouteData(), new ActionDescriptor());

            ViewDataDictionary viewDataDictionary = new(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            using StringWriter writer = new();

            ViewContext viewContext = new(
                actionContext,
                viewResult.View,
                viewDataDictionary,
                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return writer.ToString();
        }

        private static string GetEmailPlainTextBody(EmailTemplateModel emailTemplateDetails)
        {
            StringBuilder bodyBuilder = new();

            bodyBuilder.Append("Welcome,\r\n\r\n");
            bodyBuilder.AppendFormat("An account has been created for you with the following username: {0}\r\n\r\n", emailTemplateDetails.Username.Address);
            bodyBuilder.Append("Please use the link below to activate your account:\r\n\r\n");
            bodyBuilder.AppendFormat("{0}\r\n\r\n", emailTemplateDetails.Url);
            bodyBuilder.Append("Please provide a password of\r\n\r\n");
            bodyBuilder.Append("- at least 5 characters\r\n\r\n");
            bodyBuilder.Append("- a combination of upper and lower case characters\r\n\r\n");
            bodyBuilder.Append("- one or more numbers\r\n\r\n");
            bodyBuilder.Append("Thank you\r\n\r\n");

            return bodyBuilder.ToString();
        }
    }
}
