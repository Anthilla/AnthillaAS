using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AnthillaSP {

    public class AnthillaHub : Hub {
        private string serverUrl = "";
        private string line = null;

        public AnthillaHub() {
            //serverUrl = new Anth_GetParam().GetServerUrl();
        }

        public Task UnsubscribeChannel(string collectionName) {
            var hubConnection = new HubConnection(serverUrl);
            var serverHub = hubConnection.CreateHubProxy("AnthillaHub");
            hubConnection.Start().Wait();
            serverHub.Invoke("UnsubscribeChannelSRV", collectionName);

            return Groups.Remove(Context.ConnectionId, collectionName);
        }

        public void Start(string message) {
            var hubConnection = new HubConnection(serverUrl);
            var serverHub = hubConnection.CreateHubProxy("AnthillaHub");
            string collectionName = "subscribedChannel";
            serverHub.On("flush", computedMessage =>
                BroadcastMessage(hubConnection, collectionName, computedMessage)
            );
            hubConnection.Start().Wait();
            serverHub.Invoke("SubscribeChannelSRV", collectionName).Wait();
            Groups.Add(Context.ConnectionId, collectionName).Wait();
            if ((line = message) != null) {
                Console.WriteLine("Il Front End invia: " + line);
                serverHub.Invoke("Publish", collectionName, line).Wait();
            }
        }

        public void BroadcastMessage(HubConnection hubConnection, string collectionName, string computedMessage) {
            Clients.Group(collectionName).flush("Il Front End riceve: " + computedMessage);
            hubConnection.Stop();
            line = null;
        }
    }
}