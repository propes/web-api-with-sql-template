using System;

namespace WebApiWithSqlTemplate.Domain.Commands
{
    public sealed class AddTodoItem
    {
        public Guid TodoListId { get; set; }
        public string Description { get; set; }
    }
}