
namespace WebApp.Models
{
    public class Result
    {
        public bool Success { get; private set; }
        public bool Failure { get { return !Success; } }
        public string Message { get; private set; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, "OK");
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, "OK");
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        protected internal Result(T value, bool success, string message)
            : base(success, message)
        {
            Value = value;
        }
    }
}