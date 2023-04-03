$('body').on('click', 'btn-add-donhang', function (e) {
    e.preventDefault();
    const id = $(this).data('MaGH');
    alert(id);
})