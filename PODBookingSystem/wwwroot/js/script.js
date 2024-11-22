
document.getElementById("createAccountForm").addEventListener("submit", function(event) {
    event.preventDefault();
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    
    const newUser = { username, email, password };
    let users = JSON.parse(localStorage.getItem("users")) || [];
    users.push(newUser);
    localStorage.setItem("users", JSON.stringify(users));
    alert("Tài khoản mới đã được tạo thành công!");
});


document.addEventListener("DOMContentLoaded", function() {
    const revenue = 50000000; 
    document.getElementById("totalRevenue").innerText = revenue;
});

// Hiển thị danh sách phòng
const rooms = [
    { id: 1, name: "Phòng A", status: "Trống" },
    { id: 2, name: "Phòng B", status: "Đã đặt" },
    { id: 3, name: "Phòng C", status: "Đang dọn dẹp" }
];

const roomList = document.getElementById("rooms");
rooms.forEach(room => {
    const roomItem = document.createElement("li");
    roomItem.innerText = `Phòng: ${room.name}, Tình trạng: ${room.status}`;
    roomList.appendChild(roomItem);
});

// Doanh thu mẫu cho từng tháng
const monthlyRevenueData = [500, 700, 1000, 800, 1200, 1500, 1300, 1600, 1700, 1800, 2000, 2100];
const roomRevenueData = {
    "Phòng Đơn": 4000,
    "Phòng Đôi": 6000,
    "Phòng VIP": 8000,
};

// Biểu đồ cột - Doanh thu theo tháng
const monthlyRevenueCtx = document.getElementById("monthlyRevenueChart").getContext("2d");
new Chart(monthlyRevenueCtx, {
    type: "bar",
    data: {
        labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
        datasets: [{
            label: "Doanh Thu (triệu VND)",
            data: monthlyRevenueData,
            backgroundColor: "rgba(75, 192, 192, 0.5)",
            borderColor: "rgba(75, 192, 192, 1)",
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
                title: {
                    display: true,
                    text: "Doanh Thu (triệu VND)"
                }
            }
        }
    }
});

// Biểu đồ tròn - Tỷ lệ doanh thu theo loại phòng
const roomRevenueCtx = document.getElementById("roomRevenueChart").getContext("2d");
new Chart(roomRevenueCtx, {
    type: "pie",
    data: {
        labels: Object.keys(roomRevenueData),
        datasets: [{
            data: Object.values(roomRevenueData),
            backgroundColor: [
                "rgba(255, 99, 132, 0.6)",
                "rgba(54, 162, 235, 0.6)",
                "rgba(255, 206, 86, 0.6)"
            ]
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
        }
    }
});

// Biểu đồ đường - Doanh thu theo tháng
const monthlyLineChartCtx = document.getElementById("monthlyLineChart").getContext("2d");
new Chart(monthlyLineChartCtx, {
    type: "line",
    data: {
        labels: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
        datasets: [{
            label: "Doanh Thu (triệu VND)",
            data: monthlyRevenueData,
            borderColor: "rgba(153, 102, 255, 1)",
            backgroundColor: "rgba(153, 102, 255, 0.2)",
            fill: true,
            tension: 0.3
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
                title: {
                    display: true,
                    text: "Doanh Thu (triệu VND)"
                }
            }
        },
        plugins: {
            legend: {
                position: 'top',
            },
        }
    }
});
