namespace RapidPayService.Models
{
    public class CustomException:Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }


    }
}
