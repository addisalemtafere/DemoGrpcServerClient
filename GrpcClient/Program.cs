// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcClient;

Console.WriteLine("Hello, World!");
using var channel = GrpcChannel.ForAddress("http://localhost:5082");
var greeterClient = new Greeter.GreeterClient(channel);
var reply = await greeterClient.SayHelloAsync(new HelloRequest { Name = "GrpcClient" });
Console.WriteLine("Greeting: " + reply.Message);

// create a Todo client and call CreateTodoItem
var todoClient = new TodoItem.TodoItemClient(channel);
var todoResponse = await todoClient.CreateTodoItemAsync(new CreateTodoItemRequest { Title = "Buy milk", IsCompleted = false });
Console.WriteLine("Todo created: " + todoResponse.Message + " with Id: " + todoResponse.Id);
