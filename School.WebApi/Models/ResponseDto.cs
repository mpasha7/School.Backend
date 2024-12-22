namespace School.WebApi.Models
{
    public class ResponseDto
    {
        public string Message { get; set; } = "Success";
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; } = null;

        public ResponseDto Success(string message)
        {
            this.Message = message;
            this.IsSuccess = true;
            return this;
        }
    }
}
