using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class RemoveTodoItem
    {
        public Guid Id { get; set; }
        public Guid TodoListId { get; set; }
    }
}