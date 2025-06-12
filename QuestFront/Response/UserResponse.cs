namespace QuestTrakingAPI.Response
{
    public class UserResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static UserResponse<T> Success(T data, string message = "OK")
            => new() { Status = 200, Message = message, Data = data };

        public static UserResponse<T> Fail(string message, int status = 400)
            => new() { Status = status, Message = message };
    }
}
