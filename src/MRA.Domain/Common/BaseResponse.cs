namespace MRA.Domain.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
        public T Data { get; set; }

        public BaseResponse()
        {
            this.Success = true;
        }

        public BaseResponse(string message)
        {
            this.Success = true;
            this.Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            this.Message = message;
            this.Success = success;
        }

        public BaseResponse(T data, string message = null)
        {
            this.Success = true;
            this.Message = message;
            this.Data = data;
        }
    }
}
