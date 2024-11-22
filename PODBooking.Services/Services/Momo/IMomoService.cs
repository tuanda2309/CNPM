using Microsoft.AspNetCore.Http;
using PODBooking.Repositories.Models;
using PODBooking.Repositories.Models.Momo;

namespace PODBooking.Services.Services.Momo
{
    public interface IMomoService
    {
        public Task<MomoCreatePaymentResponseModel> CreatePaymentMomo(OrderInfo model);
        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
