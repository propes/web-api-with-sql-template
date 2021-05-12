using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class UpdateTodoItem
    {
        public Guid Id { get; set; }
        public Guid TodoListId { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}