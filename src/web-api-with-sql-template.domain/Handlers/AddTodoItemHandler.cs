using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithSqlTemplate.Domain.Commands;
using WebApiWithSqlTemplate.Domain.Contexts;
using WebApiWithSqlTemplate.Domain.Models;
using WebApiWithSqlTemplate.Domain.Results;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Handlers
{
    public sealed class AddTodoItemHandler
    {
        private readonly TodoListContext _dbContext;

        public AddTodoItemHandler(TodoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TodoItem>> Handle(AddTodoItem command)
        {
            Guard.IsNotNull(command, nameof(command));

            try
            {
                var todoList = await _dbContext
                    .TodoLists
                    .Include(x => x.Items)
                    .FirstOrDefaultAsync(x => x.Id == command.TodoListId);
                
                if (todoList == null)
                {
                    return Result<TodoItem>.NotFound();
                }

                var todoItem = todoList.AddItem(command.Description);

                await _dbContext.SaveChangesAsync();

                return Result<TodoItem>.From(todoItem);
            }
            catch (Exception e)
            {
                return Result<TodoItem>.From(e);
            }
        }
    }
}