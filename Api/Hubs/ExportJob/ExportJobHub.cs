using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs.ExportJob;

public class ExportJobHub : Hub
{
    private static readonly Dictionary<string, int> _connectionDomains = new();

    public override async Task OnConnectedAsync()
    {
        _connectionDomains[Context.ConnectionId] = 1;

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionDomains.Remove(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task NotifyDomain(int domainId, int jobId, string status)
    {
        var matchingConnections = _connectionDomains
            .Where(kvp => kvp.Value == domainId)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach(var connectionId in matchingConnections)
        {
            await Clients.Client(connectionId).SendAsync("JobUpdate", jobId, status);
        }
    }

    public static IEnumerable<string> GetConnectionsForDomain(int domainId)
    {
        return _connectionDomains
            .Where(kvp => kvp.Value == domainId)
            .Select(kvp => kvp.Key);
    }
}
