function showPaymentDetails(button) {
    // Tạo mã thanh toán ngẫu nhiên
    var randomPaymentCode = Math.random().toString(36).substring(2, 8);

    // Hiển thị thông tin thanh toán
    var paymentDetails = `
        <div class="payment-details">
            <p><strong>Mã thanh toán:</strong> ${randomPaymentCode}</p>
            <p><strong>Hình thức thanh toán:</strong> Chuyển khoản hoặc thẻ tín dụng</p>
            <img src="/img/pay.jpg" alt="Ảnh Thanh Toán" style="width: 150px; margin-top: 10px; display: block; margin-left: auto; margin-right: auto;" />
        </div>
    `;
    button.insertAdjacentHTML('afterend', paymentDetails);
}
