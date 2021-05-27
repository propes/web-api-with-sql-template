using System.ComponentModel.DataAnnotations;

namespace WebApiWithSqlTemplate.Domain.Dtos
{
    public sealed class UpdateTodoItemDto
    {
        [Required]
        [StringLength(150)]
        public string Description { get; init; }
        
        [Required]
        public bool IsComplete { get; init; }
    }
}