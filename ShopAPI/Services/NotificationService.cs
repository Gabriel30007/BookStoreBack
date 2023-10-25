using Microsoft.AspNetCore.SignalR;
using ShopAPI.Hubs;
using System.Timers;

namespace ShopAPI.Services;

public class NotificationService : IHostedService
{
    private readonly IHubContext<NotificationHub> _NotificationHubContext;
    private readonly System.Timers.Timer _timer;
    private readonly Random _randomGroup;

    public NotificationService(IHubContext<NotificationHub> feedHubContext)
    {
        _NotificationHubContext = feedHubContext;
        _timer = new System.Timers.Timer(5000);
        _randomGroup = new Random(0);
    }

    private void SenfNotificationTest(object? sender, ElapsedEventArgs e)
    {
        _NotificationHubContext.Clients.All.SendAsync("GetNewUser", "test");
    }

    public void SendNotification(string name)
    {
        _NotificationHubContext.Clients.All.SendAsync("GetNewUser", name);
    }

    public void SendCodeVerifier(string codeVerifier) 
    {
        
       // _NotificationHubContext.Clients.Client()
    }

    public Task StartAsync(
        CancellationToken cancellationToken)
    {
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(
        CancellationToken cancellationToken)
    {
        _timer.Enabled = false;
        _timer.Dispose();
        return Task.CompletedTask;
    }
}