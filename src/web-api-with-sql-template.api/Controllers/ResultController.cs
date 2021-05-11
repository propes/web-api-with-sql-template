using System;
using Microsoft.AspNetCore.Mvc;
using WebApiWithSqlTemplate.Domain.Results;

namespace WebApiWithSqlTemplate.Api.Controllers
{
    public abstract class ResultController : ControllerBase
    {
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

        protected ActionResult NotFoundFromResult(Result result)
        {
            if (!result.IsSuccess)
            {
                // TODO: add logging
                return Problem(result.Message, statusCode: 400);
            }
            
            return NotFound();
        }
    }
}