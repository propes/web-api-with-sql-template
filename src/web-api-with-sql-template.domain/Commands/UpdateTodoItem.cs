using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class UpdateTodoItem
    {
        public Guid Id { get; init; }
        public Guid TodoListId { get; init; }
        public string Description { get; init; }
        public bool IsComplete { get; init; }
    }
}