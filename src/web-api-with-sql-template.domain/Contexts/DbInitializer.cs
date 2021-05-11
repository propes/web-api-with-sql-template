using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSqlTemplate.Domain.Models;

namespace WebApiWithSqlTemplate.Domain.Contexts
{
    public static class DbInitializer
    {
        public static async Task SeedData(this TodoListContext dbContext)
        {
            // Put any database seeding in here.
            
            // TODO: replace this with migrations.
            await dbContext.Database.EnsureCreatedAsync();

            if (dbContext.TodoLists.Any())
            {
                return;
            }

            var todoList = new TodoList(new Guid("616a7545-4e89-4e52-92f9-7f911111d056"));
            todoList.AddItem("Walk the dog");
            todoList.AddItem("Wash the cat");
            dbContext.TodoLists.Add(todoList);

            await dbContext.SaveChangesAsync();
        }
    }
}