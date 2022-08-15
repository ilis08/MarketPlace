using FilesService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace FilesService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task StreamingFromServer(ExampleRequest request, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < request.Number; i++)
            {
                await responseStream.WriteAsync(new ExampleResponse { Numbers = i });
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override async Task SendMessage(IAsyncStreamReader<ClientToServerMessage> requestStream, IServerStreamWriter<ServerToClientMessage> responseStream, ServerCallContext context)
        {
            var clientToServerTask = ClientToServerPingHandlingAsync(requestStream, context);
            var serverToClientTask = ServerToClientPingAsync(responseStream, requestStream, context);

            await Task.WhenAll(clientToServerTask, serverToClientTask); 
        }

        private static async Task ServerToClientPingAsync(IServerStreamWriter<ServerToClientMessage> responseStream, IAsyncStreamReader<ClientToServerMessage> requestStream, ServerCallContext context)
        {
            int pingCount = 0;

            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new ServerToClientMessage
                {
                    Text = $"Server said hi {++pingCount} times to {requestStream.Current.Text}",
                    Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
                });
            }
        }

        private async Task ClientToServerPingHandlingAsync(IAsyncStreamReader<ClientToServerMessage> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                var message = requestStream.Current;
                _logger.LogInformation($"The client said: {message.Text}");
            }
            
        }
    }
}