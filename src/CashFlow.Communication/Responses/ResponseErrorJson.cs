namespace CashFlow.Communication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessage { get; set; }
        public ResponseErrorJson(string errorMessage) 
        {
            ErrorMessage = [errorMessage];
        }

        public ResponseErrorJson(List<string> errorMessage) 
        {
            ErrorMessage = errorMessage;
        }
    }
}
