namespace CMLApplication.Common.CustomExceptions
{
    public class CustomDBException : Exception
    {
        public string? ErrorMessage {  get; set; }
        public int HTTPErrorCode { get; set; }

        public CustomDBException(string errorMessage, int httpErrorCode) : base(errorMessage)
        {
            ErrorMessage = errorMessage;
            HTTPErrorCode = httpErrorCode;
        }
    }
}
