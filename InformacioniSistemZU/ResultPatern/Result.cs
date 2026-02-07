namespace InformacioniSistemZU.ResultPatern
{
    public class Result
    {
        public string Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(string error, bool isSuccess )
        {
            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result Success() => new(null, true);
        public static Result Failure(string error) => new(error, false);
    }
}
