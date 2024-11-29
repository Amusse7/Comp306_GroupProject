namespace LibraryManagementAPI.DTOs
{
    public class ApiResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
    }
}
