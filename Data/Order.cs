namespace Demo.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime? DateShip { get; set; }
        public Status Status { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }

    public enum Status
    {
        New = 0,
        Payment = 1,
        Complete = 2,
        Cancel = -1
    }
}
