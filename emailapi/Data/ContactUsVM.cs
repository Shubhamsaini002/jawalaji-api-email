namespace emailapi.Data
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string Subject { get; set; } 
        public string Message { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; } 

    }
}
