using System;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public interface IAuditable : IEntity
    {
        public DateTimeOffset DateCreated { get; }

        public DateTimeOffset DateModified { get; }
    }
}