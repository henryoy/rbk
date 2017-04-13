using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de MyHub
/// </summary>
public class MyHub : Hub
{
    public static void Show()
    {
        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
        context.Clients.All.displayStatus();
    }
}