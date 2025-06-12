namespace QuestTrakingAPI.Response
{
    public class QuestResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static QuestResponse<T> Success(T data, string message = "OK")
            => new() { Status = 200, Message = message, Data = data };

        public static QuestResponse<T> Fail(string message, int status = 400)
            => new() { Status = status, Message = message };
    }
}
