// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcClient;

Console.WriteLine("Hello, World!");
using var channel = GrpcChannel.ForAddress("http://localhost:5081");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GrpcClient" });
Console.WriteLine("Greeting: " + reply.Message);
