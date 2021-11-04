using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Chef.Common.Core
{
    public class NotificationHub : Hub
    {
        public async Task LeaveNotification(int leaveRequestId)
        {
            await Clients.All.SendAsync("LeaveNotification", leaveRequestId);
        }
    }
}
