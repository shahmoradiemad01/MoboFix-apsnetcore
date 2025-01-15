namespace MoboFix.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Status{ get; set; }
        public string? StatusMessage { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; internal set; }

        public void Confirmation(int status,string msg)
        {
            Status = status;
            StatusMessage = msg;
        }
        
    }
}