$(document).ready(function () {
    function displayComments() {
        const comments = JSON.parse(localStorage.getItem('comments')) || [];
        const commentsContainer = $('#comments-container');
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
