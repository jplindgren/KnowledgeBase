//http://stackoverflow.com/a/22420377/2497411
/*
@@Example
    $('form.login').on('submit', function(e) {
        e.preventDefault();

        var formData = $(this).serializeObject();
        console.log(formData);
        $('.datahere').html(formData);
    });
*/
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};