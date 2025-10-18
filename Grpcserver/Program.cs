using Grpcserver.Data;
using Grpcserver.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
// Add EF Core SQLite DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=todo.db";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
    // suppress the PendingModelChangesWarning so Migrate() does not throw at runtime
    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});
// Add services to the container.
builder.Services.AddGrpc();

// ensure the server listens on the port the client uses (5082)
builder.WebHost.UseUrls("http://localhost:5082");

var app = builder.Build();

// apply pending EF Core migrations at startup using a scoped service provider
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<TodoService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();