document.addEventListener('DOMContentLoaded', function () {
    const createAccountForm = document.getElementById('createAccountForm');
    
    createAccountForm.addEventListener('submit', function (event) {
        event.preventDefault(); 

        const username = document.getElementById('username').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const role = document.getElementById('role').value;

        if (!username || !email || !password || !role) {
            alert('Vui lòng điền đầy đủ thông tin!');
            return;
        }

        const newAccount = {
            username,
            email,
            password,
            role
        };

        let accounts = JSON.parse(localStorage.getItem('accounts')) || [];
        accounts.push(newAccount);
        localStorage.setItem('accounts', JSON.stringify(accounts));
        alert('Tạo tài khoản thành công!');
        createAccountForm.reset();
    });
});
