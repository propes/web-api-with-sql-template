using System;
using System.Collections.Generic;
using System.Linq;
using WebApiWithSqlTemplate.Domain.Models;

namespace WebApiWithSqlTemplate.Domain.Dtos
{
    public sealed class TodoListDto
    {
        public Guid Id { get; init; }
        public IReadOnlyCollection<TodoItemDto> Items { get; init; }

        public static TodoListDto Map(TodoList todoList) => new()
        {
            Id = todoList.Id,
            Items = todoList.Items
                .Select(TodoItemDto.Map)
                .ToList()
                .AsReadOnly()
        };
    }
}