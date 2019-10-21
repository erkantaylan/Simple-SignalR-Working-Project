using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
            Debug.WriteLine($"{DateTime.Now.TimeOfDay}|{nameof(ChatHub)} is initialized!");
        }
        
        public async Task SendMessage(string user, string message)
        {
            Debug.WriteLine($"{DateTime.Now.TimeOfDay}|{user}:{message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}