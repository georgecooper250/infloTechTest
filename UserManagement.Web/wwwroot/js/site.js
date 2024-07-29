$(document).ready(function () {
    $('a[data-bs-target="#modal"]').on('click', function() {
        var userId = $(this).data('user-id');
        var contentUrl = $(this).attr('data-content-url') + "?userId=" + userId;
        var title = $(this).attr('data-title');
        $('#modal').find('.modal-title').text(title);
        $('#modalContent').load(contentUrl);
    });

    $('#modal').on('hidden.bs.modal', function (e) {
        if ($('#modalContent').find('.alert-success').length > 0) {
            $("#modalContent").load("/modals/add-user-modal", function() {
                $('.alert-success').remove();
                window.location.reload();
            });
        }
    });

    $('body').on('submit', '.ajax-form', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (result) {
                $('#modalContent').html(result);   
            },
            error: function () {
                alert('An error occurred while processing your request. Please try again.');
            }
        });
    });
});