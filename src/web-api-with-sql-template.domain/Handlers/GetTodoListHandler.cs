using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithSqlTemplate.Domain.Contexts;
using WebApiWithSqlTemplate.Domain.Models;
using WebApiWithSqlTemplate.Domain.Queries;
using WebApiWithSqlTemplate.Domain.Results;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Handlers
{
    public sealed class GetTodoListHandler
    {
        private readonly TodoListContext _dbContext;

        public GetTodoListHandler(TodoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TodoList>> Handle(GetTodoList query, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(query, nameof(query));

            try
            {
                var todoList = await _dbContext
                    .Set<TodoList>()
                    .Include(x => x.Items)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

                if (todoList == null)
                {
                    return Result<TodoList>.NotFound();
                }
                
                return Result<TodoList>.From(todoList);
            }
            catch (Exception e)
            {
                return Result<TodoList>.From(e);
            }
        }
    }
}