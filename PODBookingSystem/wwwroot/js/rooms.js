document.addEventListener('DOMContentLoaded', function () {
    const addRoomBtn = document.getElementById('add-room-btn');
    const roomList = document.querySelector('.room-list');

    // Danh sách phòng
    const rooms = [
        { name: 'Phòng VIP1', description: 'Phòng đơn, đầy đủ tiện nghi', image: '/img/vip-room1.jpg', link: '/Room/Phong1' },
        { name: 'Phòng VIP2', description: 'Phòng đôi, có cửa sổ', image: '/img/vip-room2.jpg', link: '/Room/Phong2' },
        { name: 'Phòng VIP3', description: 'Phòng VIP, view đẹp', image: '/img/vip-room3.jpg', link: '/Room/Phong3' },
        { name: 'Phòng VIP4', description: 'Phòng VIP, view đẹp', image: '/img/vip-room4.jpg', link: '/Room/Phong4' },
    ];

    // Render danh sách phòng
    function renderRooms() {
        roomList.innerHTML = '';
        rooms.forEach((room, index) => {
            const roomCard = document.createElement('div');
            roomCard.classList.add('room-card');
            roomCard.innerHTML = `
                <a href="${room.link}">
                    <img src="${room.image}" alt="${room.name}">
                </a>
                <div class="info">
                    <h3>${room.name}</h3>
                    <p>${room.description}</p>
                    <button class="btn edit-btn" data-index="${index}">Sửa</button>
                </div>
            `;
            roomList.appendChild(roomCard);
        });

        attachEventListeners();
    }

    // Gắn sự kiện cho nút Sửa
    function attachEventListeners() {
        document.querySelectorAll('.edit-btn').forEach(button => {
            button.addEventListener('click', handleEdit);
        });
    }

    // Xử lý Sửa
    function handleEdit(event) {
        const roomIndex = event.target.dataset.index;
        const room = rooms[roomIndex];

        const newImage = prompt('Nhập đường dẫn ảnh mới:', room.image);
        const newDescription = prompt('Nhập mô tả mới:', room.description);

        if (newImage) room.image = newImage;
        if (newDescription) room.description = newDescription;

        renderRooms(); 
    }

    
    addRoomBtn.addEventListener('click', function () {
        alert('Mở form để thêm phòng');
    });

    renderRooms();
});
