using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithSqlTemplate.Domain.Results;

namespace WebApiWithSqlTemplate.Api.Controllers
{
    public abstract class ResultController : ControllerBase
    {
        private readonly IDictionary<Guid, Func<Task<ActionResult>>> _stubScenarios;

        protected ResultController()
        {
            _stubScenarios = new Dictionary<Guid, Func<Task<ActionResult>>>
            {
                {Guid.Parse("12345678-abc1-def2-0429-123456789abc"), RateLimited},
                {Guid.Parse("12345678-abc1-def2-0500-123456789abc"), ServerError},
                {Guid.Parse("12345678-abc1-def2-0504-123456789abc"), Timeout}
            };
        }

        protected ActionResult<TDto> AcceptedFromResult<T, TDto>(Result<T> result, Func<T, TDto> map)
        {
            if (!result.IsSuccess)
            {
                // TODO: add logging
                return Problem(result.Message, statusCode: 400);
            }

            return Accepted(map(result.Value));
        }

        protected ActionResult<TDto> OkFromResult<T, TDto>(Result<T> result, Func<T, TDto> map)
        {
            if (!result.IsSuccess)
            {
                // TODO: add logging
                return Problem(result.Message, statusCode: 400);
            }

            return Ok(map(result.Value));
        }

        protected ActionResult NoContentFromResult(Result result)
        {
            if (!result.IsSuccess)
            {
                // TODO: add logging
                return Problem(result.Message, statusCode: 400);
            }

            return NoContent();
        }

        protected bool TryGetStubScenario(Guid id, out Func<Task<ActionResult>> stubScenario) =>
            _stubScenarios.TryGetValue(id, out stubScenario);

        private async Task<ActionResult> RateLimited()
        {
            Response.Headers.Add("Retry-After", "60");

            return await Task.FromResult(StatusCode(StatusCodes.Status429TooManyRequests));
        }

        private async Task<ActionResult> ServerError()
        {
            return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError));
        }

        private async Task<ActionResult> Timeout()
        {
            await Task.Delay(TimeSpan.FromMinutes(2));
            return StatusCode(StatusCodes.Status504GatewayTimeout);
        }
    }
}