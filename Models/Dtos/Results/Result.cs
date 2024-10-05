namespace Models.Dtos.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T ValueElement { get; set; }
        public string Msg { get; set; }
        public static Result<T> Success(T value, string msg = "") => new Result<T> { IsSuccess = true, ValueElement = value, Msg = msg };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Msg = error };

    }
}
