$(function () {
    $('.btn-like').one('click', function() {
        like(this);
    });

    function like(likeButton) {
        console.log('LIKE()');
        var song = $(likeButton).closest('.song');
        var songId = song.find('#SongId').val();
        console.log('clicked like for ' + songId);
        var loadingImage = $('<img />')
            .attr({ width: 24, src: "/Content/Images/loading.gif" })
            .addClass('img-loading');
        song.find('.likes-number').after(loadingImage);
        $.ajax({
            type: 'POST',
            url: '/Song/Like/' + songId,
            data: { songId: songId },
            success: function(data, status) {
                if (!data.error) {
                    var likes = data.likes;
                    console.log('returned likes: ' + likes);
                    song.find('.img-loading').remove();
                    song.find('.likes-number').text(likes).addClass('panel');
                    $(likeButton).one('click', function() {
                        like(likeButton);
                    });
                }
            },
            error: function(data, status) {
                console.log('error');
                console.log('data:');
                console.log(data);
                console.log('status: ' + status);
            }
        });
    }
});