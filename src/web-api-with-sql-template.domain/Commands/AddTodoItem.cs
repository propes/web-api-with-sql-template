using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class AddTodoItem
    {
        public Guid TodoListId { get; init; }
        public string Description { get; init; }
    }
}