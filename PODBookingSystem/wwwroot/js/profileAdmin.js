document.addEventListener('DOMContentLoaded', function () {
    const storedImage = localStorage.getItem('adminProfileImage');
    const profileImage = document.getElementById('profile-image');
    const uploadProfileImageInput = document.getElementById('upload-profile-image');
    const changeProfileImageBtn = document.getElementById('change-profile-image-btn');
    const saveProfileImageBtn = document.getElementById('save-profile-image-btn');
    let newImageSrc = null; 
    
    if (storedImage) {
        profileImage.src = storedImage; 
    }

    changeProfileImageBtn.addEventListener('click', () => {
        uploadProfileImageInput.click();  
    });

    uploadProfileImageInput.addEventListener('change', (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                newImageSrc = e.target.result;
                profileImage.src = newImageSrc;  
                saveProfileImageBtn.style.display = 'inline-block';  
            };
            reader.readAsDataURL(file);
        }
    });

    saveProfileImageBtn.addEventListener('click', () => {
        if (newImageSrc) {
            localStorage.setItem('adminProfileImage', newImageSrc);  
            alert("Ảnh hồ sơ đã được lưu thành công!");
            saveProfileImageBtn.style.display = 'none';  
        }
    });
});
