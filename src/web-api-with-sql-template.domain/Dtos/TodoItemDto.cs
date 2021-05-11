using System;
using WebApiWithSqlTemplate.Domain.Models;

namespace WebApiWithSqlTemplate.Domain.Dtos
{
    public sealed class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public static TodoItemDto Map(TodoItem todoItem) => new()
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete
        };
    }
}