using System.Text.Json.Serialization;

namespace emailapi.Data
{
    public class SubTasks
    {
        public int Id { get; set; } 
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public string Title { get; set; } 
        public string Description { get; set; } 
        
        public string Status { get; set; } 

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public int Progress { get; set; } 

        public DateTime ModifiedDate { get; set; }
        public string Modifiedby { get; set; }
    }
}
