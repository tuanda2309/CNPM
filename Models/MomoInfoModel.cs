
namespace PODBooking.Repositories.Models
{
    public class MomoInfoModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string OrderInfo { get; set; }
        public string FullName { get; set; }
        public double Amount { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
