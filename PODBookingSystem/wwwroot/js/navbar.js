document.addEventListener('DOMContentLoaded', function () {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
        document.getElementById("profile-username").textContent = user.username;
        document.getElementById("profile-image").src = user.image;
        document.getElementById("profile-title").textContent = user.title;
        document.getElementById("profile-role").textContent = user.role;
        document.getElementById("profile-balance").textContent = user.balance;
    }

    // Cập nhật thông tin người dùng và lưu vào localStorage
    document.getElementById("profileForm").addEventListener('submit', function (event) {
        event.preventDefault();
        const updatedUser = {
            ...user,
            username: document.getElementById("username").value,
            image: document.getElementById("image").value,
            title: document.getElementById("title").value,
        };
        localStorage.setItem('user', JSON.stringify(updatedUser));
        document.getElementById("profile-username").textContent = updatedUser.username;
        document.getElementById("profile-image").src = updatedUser.image;
        document.getElementById("profile-title").textContent = updatedUser.title;
    });

    // Đăng xuất và xóa thông tin người dùng trong localStorage
    document.getElementById("logout").addEventListener('click', function () {
        localStorage.removeItem('user');
        window.location.href = 'login.html';
    });
});

// Mở/đóng menu khi nhấn vào nút menu
const menuBtn = document.querySelector(".menuBtn");
const navBar = document.querySelector(".navBar");
menuBtn.addEventListener("click", navToggle);

function navToggle() {
    menuBtn.classList.toggle("openmenu");
    navBar.classList.toggle("open");
    if (navBar.classList.contains("open")) {
        navBar.style.maxHeight = navBar.scrollHeight + "px";
    } else {
        navBar.removeAttribute("style");
    }
}

// Lấy URL từ API và thực hiện tìm kiếm
document.getElementById("searchButton").addEventListener('click', function () {
    const searchQuery = document.getElementById("searchInput").value.toLowerCase().trim(); 
    console.log(searchQuery);  

    if (searchQuery) {
        if (searchQuery === "văn phòng trong ngày") {
            window.location.href = '/Room/PhongTrongNgay'; 
        } else if (searchQuery === "phòng họp") {
            window.location.href = '/Room/PhongHop';
        } else if (searchQuery === "vip") {
            window.location.href = '/Room/Vip';  
        } 
        else if (searchQuery === "dịch vụ") {
            window.location.href = '/Service/Service';  
        } else {
            window.location.href = '/Room/404'; 
        }
    }
});


$(document).ready(function () {
    // Hiển thị các bình luận đã lưu
    function displayComments() {
        const comments = JSON.parse(localStorage.getItem('comments')) || [];
        const commentsContainer = $('#commentList');
        commentsContainer.empty();
        comments.forEach(comment => {
            const commentElement = `
                <div class="comment">
                    <strong>${comment.username}</strong>: ${comment.text}
                </div>`;
            commentsContainer.append(commentElement);
        });
    }

    // Lưu bình luận
    $('#commentSubmit').click(function () {
        const user = JSON.parse(localStorage.getItem('user')) || { username: 'you' };

        const commentText = $('#commentText').val();
        if (commentText.trim() === '') {
            alert('Bình luận không được để trống.');
            return;
        }

        const comments = JSON.parse(localStorage.getItem('comments')) || [];
        comments.push({ username: user.username, text: commentText });
        localStorage.setItem('comments', JSON.stringify(comments));
        $('#commentText').val('');
        displayComments();
    });

    displayComments();
});


document.querySelector('.profile-btn').addEventListener('click', function () {
    const dropdown = document.querySelector('.dropdown-content');
    dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
});

window.onclick = function (event) {
    if (!event.target.matches('.profile-btn') && !event.target.matches('#menu-profile-image')) {
        const dropdowns = document.getElementsByClassName("dropdown-content");
        for (let i = 0; i < dropdowns.length; i++) {
            const openDropdown = dropdowns[i];
            if (openDropdown.style.display === "block") {
                openDropdown.style.display = "none";
            }
        }
    }
};

// Thong báo
$(".notification-icon").click(function (e) {
    e.stopPropagation(); 
    fetchNotifications();  
    $("#notificationList").toggle(); 
});


$(document).click(function () {
    $("#notificationList").hide(); 
});


document.querySelector(".notification-icon").addEventListener("click", function (event) {
    event.stopPropagation(); 
    var notificationList = document.getElementById("notificationList");
    notificationList.style.display = notificationList.style.display === "none" ? "block" : "none";
});

// Ẩn thông báo khi nhấn ra ngoài
document.addEventListener("click", function (event) {
    var notificationList = document.getElementById("notificationList");
    var isClickInside = document.querySelector(".notification-icon").contains(event.target) || notificationList.contains(event.target);

    if (!isClickInside) {
        notificationList.style.display = "none";
    }
});

function fetchNotifications() {
    $.ajax({
        url: "/Notification/GetUserNotifications",
        method: "GET",
        success: function (html) {
            console.log("HTML received:", html); 
            const notificationList = $("#notificationItems");
            notificationList.html(html); 
            $("#notificationList").toggle(); 
        },
        error: function (error) {
            console.error("Lỗi khi lấy thông báo:", error);
        }
    });
};

// Xem trước hình ảnh khi chọn
document.getElementById('image').onchange = function (event) {
    const [file] = event.target.files;
    if (file) {
        const preview = document.getElementById('preview');
        preview.src = URL.createObjectURL(file);
        preview.style.display = 'block';
    }
};
