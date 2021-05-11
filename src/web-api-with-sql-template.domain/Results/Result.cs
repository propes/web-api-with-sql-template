using System;

namespace WebApiWithSqlTemplate.Domain.Results
{
    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value) : base(true)
        {
            Value = value;
        }

        protected Result(bool isSuccess, string message) : base(isSuccess, message)
        {
            
        }

        public static Result<T> From(T value)
        {
            return new(value);
        }

        public new static Result<T> From(Exception ex)
        {
            return Fail(ex.Message);
        }
        
        public new static Result<T> Fail(string message = null)
        {
            return new(false, message);
        }

        public static NotFoundResult<T> NotFound()
        {
            return new();
        }
    }

    public class NotFoundResult<T> : Result<T>
    {
        public NotFoundResult(string message = null) : base(false, message)
        {
            
        }
    }

    public class Result
    {
        public bool IsSuccess { get; }

        public string Message { get; }
        
        protected Result(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(true);
        }
        
        public static Result Fail(string message = null)
        {
            return new Result(false, message);
        }

        public static Result From(Exception ex)
        {
            return Fail(ex.Message);
        }

        public static Result NotFound(string message = null)
        {
            return new NotFoundResult(message);
        }
    }

    public class NotFoundResult : Result
    {
        public NotFoundResult(string message = null) : base(false, message)
        {
        }
    }
}