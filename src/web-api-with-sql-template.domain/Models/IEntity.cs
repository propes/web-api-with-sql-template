using System;

namespace WebApiWithSqlTemplate.Domain.Models
{
    public interface IEntity
    {
        public Guid Id { get; }
    }
}