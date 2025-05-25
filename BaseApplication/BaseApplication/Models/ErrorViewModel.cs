namespace BaseApplication.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } = "test main merge branch yesy";

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}