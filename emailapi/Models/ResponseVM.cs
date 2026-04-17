namespace emailapi.Models
{
    public class ResponseVM
    {
        public int status { get; set; }
        public string? Message { get; set; }
        public dynamic? data { get; set; }
    }

    public class ContactUsVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
