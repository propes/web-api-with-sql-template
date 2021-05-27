using System.ComponentModel.DataAnnotations;

namespace WebApiWithSqlTemplate.Domain.Dtos
{
    public sealed class AddTodoItemDto
    {
        [Required]
        [StringLength(150)]
        public string Description { get; init; }
    }
}