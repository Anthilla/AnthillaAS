using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace AnthillaFirewall {

    public class FirewallHub : Hub {

        public void SubscribeChannelSRV(string collectionName) {
            Groups.Add(Context.ConnectionId, collectionName).Wait();
        }

        public Task UnsubscribeChannelSRV(string collectionName) {
            return Groups.Remove(Context.ConnectionId, collectionName);
        }

        public void Publish(string collectionName, string message) {
            string computedMessage = "Il Back End riceve: " + message;
            Clients.Group(collectionName).flush(computedMessage);
            Console.WriteLine(computedMessage);
        }
    }
}