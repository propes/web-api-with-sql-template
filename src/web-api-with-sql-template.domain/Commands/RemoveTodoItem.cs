using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class RemoveTodoItem
    {
        public Guid Id { get; init; }
        public Guid TodoListId { get; init; }
    }
}