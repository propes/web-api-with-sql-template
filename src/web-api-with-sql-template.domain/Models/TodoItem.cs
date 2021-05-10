using System;
using WebApiWithSqlTemplate.Domain.Utilities;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public class TodoItem
    {
        public Guid Id { get; }
        public DateTimeOffset DateCreated { get; }
        public DateTimeOffset DateModified { get; private set; }
        public string Description { get; private set; }
        public bool IsComplete { get; private set; }
        public bool IsDeleted { get; private set; }

        public TodoItem()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTimeOffset.UtcNow;
            DateModified = DateTimeOffset.UtcNow;
            IsComplete = false;
            IsDeleted = false;
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
            DateModified = DateTimeOffset.UtcNow;
        }

        public void MarkComplete()
        {
            IsComplete = true;
            DateModified = DateTimeOffset.UtcNow;
        }

        public void MarkIncomplete()
        {
            IsComplete = false;
            DateModified = DateTimeOffset.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            DateModified = DateTimeOffset.UtcNow;
        }
    }
}
