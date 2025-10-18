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
    
    public override async Task<GetTodoItemResponse> GetTodoItem(GetTodoItemRequest request,
        ServerCallContext context)
    {
        var todoItemEntity = await _dbContext.TodoItems.FindAsync(request.Id);

        if (todoItemEntity == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Todo item not found"));
        }

        return new GetTodoItemResponse()
        {
            Id = todoItemEntity.Id,
            Title = todoItemEntity.Title,
            IsCompleted = todoItemEntity.IsCompleted
        };
    }
    public override async Task<GetAllTodoItemsResponse> GetAllTodoItems(GetAllTodoItemsRequest request,
        ServerCallContext context)
    {
        var response = new GetAllTodoItemsResponse();
        var todoItems = _dbContext.TodoItems.ToList();

        foreach (var item in todoItems)
        {
            response.Items.Add(new GetTodoItemResponse()
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
            });
        }

        return await Task.FromResult(response);
    }
}