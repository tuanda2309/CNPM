document.addEventListener('DOMContentLoaded', function () {
    const storedImage = localStorage.getItem('adminProfileImage');
    const profileImage = document.querySelector('.profile img');
    
    if (storedImage) {
        profileImage.src = storedImage; 
    } else {
        profileImage.src = './img/profil-logo.jpg'; 
    }
});
