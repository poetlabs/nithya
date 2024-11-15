namespace canoodleapi.DataObjects
{
    public class ApiResponseModel
    {
        public ResultResponseModel Result { get; set; }
    }
    public class ResultResponseModel
    {
        public string Action { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object Data { get; set; }
        public object StackTrace { get; set; }
        public List<ErrorInfo> Errors { get; set; }
    }
}
