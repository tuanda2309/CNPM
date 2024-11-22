function showPaymentDetails(button) {
    var bookingId = button.getAttribute('data-booking-id');

    fetch(`/api/GetBookingTotalPrice?bookingId=${bookingId}`)
        .then(response => response.json())
        .then(data => {
            if (data.TotalPrice) {
                // Tạo mã thanh toán ngẫu nhiên
                var randomPaymentCode = Math.random().toString(36).substring(2, 8);

                var paymentDetails = `
                    <div class="payment-details">
                        <p><strong>Mã thanh toán:</strong> ${randomPaymentCode}</p>
                        <p><strong>Hình thức thanh toán:</strong> Chuyển khoản hoặc thẻ tín dụng</p>
                        <img src="/img/pay.jpg" alt="Ảnh Thanh Toán" style="width: 150px; margin-top: 10px; display: block; margin-left: auto; margin-right: auto;" />
                    </div>
                `;

                document.body.innerHTML += paymentDetails;
            } else {
                alert("Không lấy được thông tin thanh toán.");
            }
        })
        .catch(error => {
            console.error("Lỗi khi lấy thông tin thanh toán:", error);
        });
}
