namespace Common.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ProductListId { get; set; }
        public Product[] ProductList { get; set; }
        public OrderType OrderType { get; set; }
        public OrderState OrderState { get; set; }
    }
}
