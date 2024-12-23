namespace Shared
{
    public record Error
    {
        public string Message { get; set; }

        public Error(string message)
        {
            Message = message;
        }

        public static Error NotFound(string message) =>
            new(message);

        public static Error Conflict(string message) =>
            new(message);
    }
}
