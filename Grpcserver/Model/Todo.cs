namespace Grpcserver.Model;

public class TodoItemEntities
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}