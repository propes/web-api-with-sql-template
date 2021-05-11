using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithSqlTemplate.Domain.Dtos;
using WebApiWithSqlTemplate.Domain.Handlers;
using WebApiWithSqlTemplate.Domain.Queries;

namespace WebApiWithSqlTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/v1/todolists")]
    public class TodoListController : ResultController
    {
        private readonly GetTodoListHandler _handler;

        public TodoListController(GetTodoListHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Gets a todo list by id.
        /// </summary>
        /// <param name="id">The todo list id.</param>
        /// <returns>The todo list or an error code.</returns>
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListDto>> Get(Guid id)
        {
            if (TryGetStubScenario(id, out var stubScenario))
            {
                return await stubScenario();
            }

            var result = await _handler.Handle(new GetTodoList
            {
                Id = id
            });

            return OkFromResult(result, TodoListDto.Map);
        }
    }
}