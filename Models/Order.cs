namespace KhumaloCraftWebApp.Models
{
    public class Order
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string Username { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }
    }
}