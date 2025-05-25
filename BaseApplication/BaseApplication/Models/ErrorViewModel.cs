namespace BaseApplication.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } = "test again reset";

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}