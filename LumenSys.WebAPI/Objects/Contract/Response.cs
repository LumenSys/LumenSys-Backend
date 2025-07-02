namespace LumenSys.WebAPI.Objects.Contract
{
    public class Response
    {
        public ResponseEnum Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}

