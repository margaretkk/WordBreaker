using System;

namespace WordsBreaker
{
    internal class Result<T>
    {
        private Result result;
        public Result()
        {
            result = new Result();
        }
        public Result(T complete, bool success, Exception exception)
        {
            Complete = complete;
            result = new Result(success, exception);
        }

        public bool Success => result.Success;

        public Exception Exception => result.Exception;

        public static Result<T> GetSuccess(T result) => new Result<T>(result, true, null);

        public static Result<T> GetFailure(Exception ex) => new Result<T>(default(T), false, ex);
        
        public T Complete { get; private set; } 

        public void SetSuccess(T res)
        {
            Complete = res;

            result.SetSuccess();
        }

        public void SetFailure(Exception ex) => result.SetFailure(ex);
    }

    internal class Result
    {
        public Result()
        {
        }

        public Result(bool success, Exception ex)
        {
            Success = success;
            Exception = ex;
        }

        public bool Success { get; private set; }

        public Exception Exception { get; private set; }

        public void SetSuccess()
        {
            Success = true;
        }

        public void SetFailure(Exception ex)
        {
            Success = false;
            Exception = ex;
        }
    }
}
