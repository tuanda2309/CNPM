using Microsoft.EntityFrameworkCore;
using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByBookingIdAsync(int bookingId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CreatePayment(int bookingId, double amount, string paymentMethod)
        {
            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending",
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment.PaymentId;
        }
        public async Task UpdatePaymentStatus(int paymentId, string status)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                payment.PaymentStatus = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
