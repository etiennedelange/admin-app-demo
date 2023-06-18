using System;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public interface ITemplateService
    {
        /// <summary>
        /// Notifies connected clients that it needs to download the template with the specified GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="contentHash"></param>
        /// <returns></returns>
        Task TemplateUpdatedAsync(Guid guid, string contentHash, bool forceUpdate = false);
    }
}
