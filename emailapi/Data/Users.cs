namespace emailapi.Data
{
    public class Users
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Password { get; set; } 
        public string Country { get; set; } 
        public string VerificationCode { get; set; } 
        public bool Verified { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
