using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithSqlTemplate.Domain.Commands;
using WebApiWithSqlTemplate.Domain.Contexts;
using WebApiWithSqlTemplate.Domain.Results;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Handlers
{
    public sealed class RemoveTodoItemHandler
    {
        private readonly TodoListContext _dbContext;

        public RemoveTodoItemHandler(TodoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveTodoItem command, CancellationToken cancellationToken)
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
                    return Result.NotFound();
                }

                todoList.RemoveItem(command.Id);

                await _dbContext.SaveChangesAsync(cancellationToken);
                
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.From(e);
            }
        }
    }
}