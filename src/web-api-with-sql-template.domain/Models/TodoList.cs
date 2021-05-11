using System;
using System.Collections.Generic;
using WebApiWithSqlTemplate.Domain.Exceptions;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public sealed class TodoList : AuditableEntity
    {
        private readonly List<TodoItem> _items = new();

        public IReadOnlyCollection<TodoItem> Items => _items.AsReadOnly();

        private TodoList()
        {
        }

        public TodoList(Guid id) : base(id)
        {
        }
        
        public TodoItem AddItem(string description)
        {
            var todoItem = new TodoItem(description);
            _items.Add(todoItem);
            
            UpdateDateModified();

            return todoItem;
        }

        public TodoItem UpdateItem(Guid id, string description, bool isComplete)
        {
            var todoItem = GetItem(id);
            todoItem.UpdateDescription(description);
            todoItem.UpdateIsComplete(isComplete);
            
            UpdateDateModified();

            return todoItem;
        }
        
        public void RemoveItem(Guid id)
        {
            var item = GetItem(id);
            item.Delete();
            
            UpdateDateModified();
        }

        private TodoItem GetItem(Guid id)
        {
            Guard.IsNotDefault(id, nameof(id));
            
            var item = _items.Find(i => i.Id == id);

            if (item is null)
            {
                throw new DomainException($"Item with id '{id}' does not exist.");
            }

            return item;
        }
    }
}