namespace TvShow.Inventory.Application.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }

        public T Entity { get; set; }

        public string Message { get; set; }

        public ServiceResponse(T entity)
        {
            Entity = entity;
            Success = true;
        }

        public ServiceResponse(string errorMessage)
        {
            Message = errorMessage;
            Success = false;
        }

        public ServiceResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
