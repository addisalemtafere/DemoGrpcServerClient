using Grpc.Core;
using Grpcserver;
using Grpcserver.Data;

namespace Grpcserver.Services;

public class GreeterService(ILogger<GreeterService> logger, AppDbContext dbContext) : Greeter.GreeterBase
{
    private AppDbContext _dbContext = dbContext;
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}