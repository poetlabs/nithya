namespace canoodleapi.DataObjects
{
    public class ErrorInfo
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public ErrorDetail ErrorDetail { get; set; }
    }

    public class ErrorDetail
    {
        public string Field { get; set; }
        public string Summary { get; set; }
    }
}
