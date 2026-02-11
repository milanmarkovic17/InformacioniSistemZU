namespace InformacioniSistemZU.ResultPatern
{
    public class Result
    {
        public List<string>? Errors { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(List<string>? errors, bool isSuccess)
        {
            Errors = errors;
            IsSuccess = isSuccess;
        }

        public static Result Success() => new(null, true);
        public static Result Failure(List<string>? errors) => new(errors, false);
    }

        public class Result<T> : Result
        {
           // public string? Message { get; }
            public T? Value { get; }
        
            protected Result(T? value, List<string>? errors, bool isSuccess) : base(errors, isSuccess)
            {
                Value = value;
            }

        public static Result<T> Success(T? value) => new(value, null, true);
        public static new Result<T> Failure(List<string>? errors) => new(default, errors, false);
        public static new Result<T> FailureMessage(string error) => new(default, new List<string> { error }, false);

        }
}
