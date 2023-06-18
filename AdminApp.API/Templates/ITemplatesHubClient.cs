using System;
using System.Threading.Tasks;

namespace AdminApp.API.Templates
{
    public interface ITemplatesHubClient
    {
        Task TemplateUpdatedAsync(Guid guid, string hash, bool forceUpdate = false);
        Task Echo(string message);
    }
}