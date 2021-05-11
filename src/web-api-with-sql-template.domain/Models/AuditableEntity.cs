using System;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public class AuditableEntity : IAuditable
    {
        public Guid Id { get; }
        public DateTimeOffset DateCreated { get; }
        public DateTimeOffset DateModified { get; private set; }

        protected AuditableEntity() : this(Guid.NewGuid())
        {
        }

        protected AuditableEntity(Guid id)
        {
            Id = id;
            DateCreated = DateTimeOffset.UtcNow;
            DateModified = DateTimeOffset.UtcNow;
        } 
        
        public void UpdateDateModified()
        {
            DateModified = DateTimeOffset.UtcNow;
        }
    }
}