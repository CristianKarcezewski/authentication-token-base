namespace CMLApplication.Common.CustomExceptions
{
    public class CustomServiceException : Exception
    {
        public string? ErrorMessage { get; set; }
        public int HTTPErrorCode { get; set; }

        public CustomServiceException(string errorMessage, int httpErrorCode) : base(errorMessage)
        {
            ErrorMessage = errorMessage;
            HTTPErrorCode = httpErrorCode;
        }
    }
}
