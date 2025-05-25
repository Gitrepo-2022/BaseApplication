namespace BaseApplication.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } = "test revert file";

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}