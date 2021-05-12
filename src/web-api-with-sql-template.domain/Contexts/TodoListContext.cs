using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiWithSqlTemplate.Domain.Models;

namespace WebApiWithSqlTemplate.Domain.Contexts
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Put any custom entity configuration in here.

            modelBuilder
                .Entity<TodoList>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<TodoList>()
                .HasMany(x => x.Items)
                .WithOne();

            modelBuilder
                .Entity<TodoItem>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<TodoItem>()
                .Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}