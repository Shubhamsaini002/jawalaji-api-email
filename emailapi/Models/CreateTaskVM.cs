namespace emailapi.Models
{
    public class CreateTaskVM
    {

        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Progress { get; set; }

    }
}
