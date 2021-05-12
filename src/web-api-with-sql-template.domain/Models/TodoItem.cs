using System;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public sealed class TodoItem : AuditableEntity
    {
        public string Description { get; private set; }
        public bool IsComplete { get; private set; }
        
        public TodoItem() : this(Guid.NewGuid())
        {
        }

        private TodoItem(Guid id) : base(id)
        {
            IsComplete = false;
        }
        
        public TodoItem(string description) : this()
        {
            UpdateDescription(description);
        }

        public void UpdateDescription(string description)
        {
            Guard.IsNotNullOrWhiteSpace(description, nameof(description));
            Guard.IsNotLongerThan(description, 150, nameof(description));

            Description = description.Trim();
            UpdateDateModified();
        }

        
        public void UpdateIsComplete(bool isComplete)
        {
            IsComplete = isComplete;
            UpdateDateModified();
        }
        
        public void MarkComplete()
        {
            IsComplete = true;
            UpdateDateModified();
        }

        public void MarkIncomplete()
        {
            IsComplete = false;
            UpdateDateModified();
        }
    }
}
