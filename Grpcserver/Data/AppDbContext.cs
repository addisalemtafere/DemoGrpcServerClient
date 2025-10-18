using Grpcserver.Model;
using Microsoft.EntityFrameworkCore;

namespace Grpcserver.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItemEntities> TodoItems { get; set; }
}