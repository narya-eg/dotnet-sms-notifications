namespace TestSuite.Models
{
    public class SmsModel
    {
        public SmsModel(ICollection<string> to,  string message)
        {
            To = to;
            Message = message;
        }
        public ICollection<string> To { get; set; } = new List<string>();
        public string Message { get; set; }
    }
}
