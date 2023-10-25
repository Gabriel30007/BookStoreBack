using Microsoft.AspNetCore.SignalR;

namespace ShopAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub() { }
        public async Task RegisterForNotification(string name)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, name); 
        }

        public async Task SendNotificationNewUser(string name)
        {
            await this.Clients.All.SendAsync("GetNewUser", name);
        }
    }
}
