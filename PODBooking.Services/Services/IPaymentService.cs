using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByBookingIdAsync(int bookingId);
        Task UpdatePaymentAsync(Payment payment);
        Task<int> CreatePayment(int bookingId, double amount, string paymentMethod);
        Task UpdatePaymentStatus(int paymentId, string status);
    }
}
