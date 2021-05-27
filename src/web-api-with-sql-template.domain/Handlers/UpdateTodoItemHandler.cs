using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithSqlTemplate.Domain.Commands;
using WebApiWithSqlTemplate.Domain.Contexts;
using WebApiWithSqlTemplate.Domain.Models;
using WebApiWithSqlTemplate.Domain.Results;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Handlers
{
    public sealed class UpdateTodoItemHandler
    {
        private readonly TodoListContext _dbContext;

        public UpdateTodoItemHandler(TodoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TodoItem>> Handle(UpdateTodoItem command, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(command, nameof(command));

            try
            {
                var todoList = await _dbContext
                    .TodoLists
                    .Include(x => x.Items)
                    .FirstOrDefaultAsync(x => x.Id == command.TodoListId, cancellationToken);

                if (todoList == null)
                {
                    return Result<TodoItem>.NotFound();
                }

                var todoItem = todoList.UpdateItem(command.Id, command.Description, command.IsComplete);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result<TodoItem>.From(todoItem);
            }
            catch (Exception e)
            {
                return Result<TodoItem>.From(e);
            }
        }
    }
}