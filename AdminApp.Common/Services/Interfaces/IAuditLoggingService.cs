using System.Threading.Tasks;

namespace AdminApp.Common.Services.Interfaces
{
    public interface IAuditLoggingService
    {
        /// <summary>
        /// Logs a JSON string containing the details of the change.
        /// </summary>
        /// <param name="change">The JSON string containing the details of the change.</param>
        /// <param name="user">The current user</param>
        /// <returns></returns>
        ValueTask LogAsync(string change, string user);

        /// <summary>
        /// Logs a JSON string containing the details of the change.
        /// </summary>
        /// <param name="change">The JSON string containing the details of the change.</param>
        /// <returns></returns>
        ValueTask LogAsync(string change);
    }
}
