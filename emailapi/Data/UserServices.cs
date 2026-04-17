namespace emailapi.Data
{
    public class UserServices
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; } 
        public string Type { get; set; } 
        public decimal Amount { get; set; }

        public string Status { get; set; }

        public DateTime CreateDate { get; set; } 
        public DateTime Modified { get; set; } 
        public string ModifiedBy { get; set; } 
    }
}
