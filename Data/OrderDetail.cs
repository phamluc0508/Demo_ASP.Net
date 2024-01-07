namespace Demo.Data
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        //Relationship
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
