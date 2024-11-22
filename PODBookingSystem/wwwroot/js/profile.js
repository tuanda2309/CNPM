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

    document.getElementById("logout").addEventListener('click', function () {
        localStorage.removeItem('user');
        window.location.href = 'login.html';
    });
});

// Xem trước hình ảnh khi chọn
document.getElementById('image').onchange = function (event) {
    const [file] = event.target.files;
    if (file) {
        const preview = document.getElementById('preview');
        preview.src = URL.createObjectURL(file);
        preview.style.display = 'block';
    }
};

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
