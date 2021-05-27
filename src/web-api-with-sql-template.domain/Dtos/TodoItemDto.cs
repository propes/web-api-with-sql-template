using System;
using WebApiWithSqlTemplate.Domain.Models;

namespace WebApiWithSqlTemplate.Domain.Dtos
{
    public sealed class TodoItemDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public bool IsComplete { get; init; }

        public static TodoItemDto Map(TodoItem todoItem) => new()
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete
        };
    }
}