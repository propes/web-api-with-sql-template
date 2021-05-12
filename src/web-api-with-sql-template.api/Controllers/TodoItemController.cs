using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithSqlTemplate.Domain.Commands;
using WebApiWithSqlTemplate.Domain.Dtos;
using WebApiWithSqlTemplate.Domain.Handlers;

namespace WebApiWithSqlTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/v1/todolists/{todoListId}")]
    public class TodoItemController : ResultController
    {
        private readonly AddTodoItemHandler _addHandler;
        private readonly UpdateTodoItemHandler _updateHandler;
        private readonly RemoveTodoItemHandler _removeHandler;

        public TodoItemController(
            AddTodoItemHandler addHandler,
            UpdateTodoItemHandler updateHandler,
            RemoveTodoItemHandler removeHandler)
        {
            _addHandler = addHandler;
            _updateHandler = updateHandler;
            _removeHandler = removeHandler;
        }
        
        /// <summary>
        /// Adds a todo list item.
        /// </summary>
        /// <param name="todoListId">The todo list id.</param>
        /// <param name="dto">The todo item details</param>
        /// <returns>The added todo item or an error code.</returns>
        /// <remarks>
        /// Stub responses:
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0429-123456789abc
        ///
        /// Returns a rate limited response.
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0500-123456789abc
        ///
        /// Returns a server error.
        /// 
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0504-123456789abc
        ///
        /// Blocks for 2 minutes, resulting in a timeout.
        /// </remarks>
        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDto>> Add(Guid todoListId, AddTodoItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _addHandler.Handle(new AddTodoItem
            {
                TodoListId = todoListId,
                Description = dto.Description
            });

            return AcceptedFromResult(result, TodoItemDto.Map);
        }
        
        /// <summary>
        /// Updates a todo list item.
        /// </summary>
        /// <param name="todoListId">The todo list id.</param>
        /// <param name="id">The todo item id.</param>
        /// <param name="dto">The todo item details to update.</param>
        /// <returns>The updated todo item or an error code.</returns>
        /// <remarks>
        /// Stub responses:
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0429-123456789abc
        ///
        /// Returns a rate limited response.
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0500-123456789abc
        ///
        /// Returns a server error.
        /// 
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0504-123456789abc
        ///
        /// Blocks for 2 minutes, resulting in a timeout.
        /// </remarks>
        [HttpPut("items/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDto>> Update(Guid todoListId, Guid id, UpdateTodoItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _updateHandler.Handle(new UpdateTodoItem
            {
                Id = id,
                TodoListId = todoListId,
                Description = dto.Description,
                IsComplete = dto.IsComplete
            });

            return AcceptedFromResult(result, TodoItemDto.Map);
        }

        /// <summary>
        /// Removes a todo list item.
        /// </summary>
        /// <param name="todoListId">The todo list id.</param>
        /// <param name="id">The todo item id.</param>
        /// <returns>No content or an error code.</returns>
        /// <remarks>
        /// Stub responses:
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0429-123456789abc
        ///
        /// Returns a rate limited response.
        ///
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0500-123456789abc
        ///
        /// Returns a server error.
        /// 
        ///     GET /apis/customers/registration/v1/registration/12345678-abc1-def2-0504-123456789abc
        ///
        /// Blocks for 2 minutes, resulting in a timeout.
        /// </remarks>
        [HttpDelete("items/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Remove(Guid todoListId, Guid id)
        {
            var result = await _removeHandler.Handle(new RemoveTodoItem
            {
                Id = id,
                TodoListId = todoListId
            });

            return NoContentFromResult(result);
        }
    }
}
