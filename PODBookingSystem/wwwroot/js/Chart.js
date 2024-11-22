
if (revenueData && roomRevenueData) {
    console.log("Revenue Data:", revenueData);
    console.log("Room Revenue Data:", roomRevenueData);

    // Vẽ biểu đồ đường - Doanh thu theo tháng
    var ctx = document.getElementById('monthlyLineChart').getContext('2d');
    var monthlyLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: revenueData.map(item => item.Month + '/' + item.Year),
            datasets: [{
                label: 'Doanh Thu',
                data: revenueData.map(item => item.TotalRevenue),
                borderColor: 'rgba(75, 192, 192, 1)',
                fill: false
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Vẽ biểu đồ hình tròn - Doanh thu theo loại phòng
    var ctx2 = document.getElementById('roomRevenueChart').getContext('2d');
    var roomRevenueChart = new Chart(ctx2, {
        type: 'pie',
        data: {
            labels: roomRevenueData.map(item => "Phòng " + item.RoomId),
            datasets: [{
                label: 'Doanh Thu Theo Loại Phòng',
                data: roomRevenueData.map(item => item.TotalRevenue),
                backgroundColor: ['rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)'],
                borderColor: ['rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true
        }
    });

    // Vẽ biểu đồ cột - Doanh thu theo tháng
    var ctx3 = document.getElementById('monthlyBarChart').getContext('2d');
    var monthlyBarChart = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: revenueData.map(item => item.Month + '/' + item.Year),
            datasets: [{
                label: 'Doanh Thu',
                data: revenueData.map(item => item.TotalRevenue),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
} else {
    console.error("Dữ liệu không hợp lệ");
}
