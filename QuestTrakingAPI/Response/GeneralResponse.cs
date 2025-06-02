namespace QuestTrakingAPI.Response
{
    public class GeneralResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;

        public static GeneralResponse Success(string message = "OK")
            => new() { Status = 200, Message = message };

        public static GeneralResponse Fail(string message, int status = 400)
            => new() { Status = status, Message = message };

    }
}
