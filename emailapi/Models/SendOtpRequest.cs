namespace emailapi.Models
{
    public class SendOtpRequest
    {
        public bool type { get; set; }
        public string Email { get; set; }
    }

    public class SendOtpRequestVM
    {
        public bool type { get; set; }
        public string Email { get; set; }

        public string tocken { get; set; }
    }
}
