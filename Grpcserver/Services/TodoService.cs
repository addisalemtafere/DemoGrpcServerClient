using Grpc.Core;
using Grpcserver.Data;

namespace Grpcserver.Services;

public class TodoService : TodoItem.TodoItemBase
{
    private AppDbContext _dbContext;

    public TodoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<CreateTodoItemResponse> CreateTodoItem(CreateTodoItemRequest request,
        ServerCallContext context)
    {
        var todoItemEntity = new Model.TodoItemEntities()
        {
            Title = request.Title,
            IsCompleted = request.IsCompleted
        };

        _dbContext.Add(todoItemEntity);
        await _dbContext.SaveChangesAsync();

        return new CreateTodoItemResponse()
        {
            Message = "success",
            Id = todoItemEntity.Id
        };
    }
}