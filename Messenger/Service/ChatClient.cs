using Core.IoC;
using Domain.Protos;
using Grpc.Net.Client;

namespace Messenger.Service
{
    [PutInIoC]
    public class ChatClient
    {
        public Greeter.GreeterClient Client { get; }

        public ChatClient() =>
            Client = new Greeter.GreeterClient(GrpcChannel.ForAddress("https://localhost:5001"));
    }
}