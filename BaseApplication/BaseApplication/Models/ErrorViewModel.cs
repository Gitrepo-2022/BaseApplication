namespace BaseApplication.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } = "test again revert";

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}