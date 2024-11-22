document.addEventListener("DOMContentLoaded", function () {
    const notificationIcon = document.querySelector(".notification-icon");
    const notificationList = document.querySelector("#notificationList");

    notificationIcon.addEventListener("click", async function () {
        if (notificationList.style.display === "none" || !notificationList.style.display) {
            notificationList.style.display = "block";

            // Lấy thông báo từ server
            const response = await fetch('/Notification/GetNotifications');
            const notifications = await response.json();

            const notificationItems = document.querySelector("#notificationItems");
            notificationItems.innerHTML = ""; // Xóa thông báo cũ

            if (notifications.length === 0) {
                notificationItems.innerHTML = "<p>Không có thông báo mới.</p>";
            } else {
                notifications.forEach(notification => {
                    const item = document.createElement("div");
                    item.className = "notification-item";
                    item.innerHTML = `
                        <strong>${notification.title}</strong>
                        <p>${notification.message}</p>
                        <small>${new Date(notification.createdAt).toLocaleString()}</small>
                    `;
                    notificationItems.appendChild(item);
                });
            }
        } else {
            notificationList.style.display = "none";
        }
    });
});
