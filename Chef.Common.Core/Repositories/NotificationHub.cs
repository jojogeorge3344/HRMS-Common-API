using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chef.Common.Core
{
    public class NotificationHub :Hub
    {
        public async Task LeaveNotification(int leaveRequestId)
        {
            await Clients.All.SendAsync("LeaveNotification", leaveRequestId);
        }
    }
}
