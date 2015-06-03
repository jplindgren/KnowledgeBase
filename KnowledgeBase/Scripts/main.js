$(function () {
    var _ = {};

    _.OpenWindowInNewTab = function(url){
        window.open(url, '_blank', 'toolbar=yes, location=yes, status=yes, menubar=yes, scrollbars=yes');
    }

    $('.openAllLinks').on('click', function (evt) {
        evt.preventDefault();

        var knowledge = evt.target.closest(".knowledge");
        var articles = $(knowledge).find('.article');

        for (var i = 0; i < articles.length; i++) {
            var linkArticle = $(articles[i]).find('.btn-default');

            //window.open(linkArticle.attr('href'), '_blank', 'toolbar=yes, location=yes, status=yes, menubar=yes, scrollbars=yes');
            _.OpenWindowInNewTab(linkArticle.attr('href'));

            /*  it should work when deploy
            var popup = window.open("about:blank", "myPopup");
            console.log(linkArticle.attr('href'));

            //do some ajax calls
            $.get(linkArticle.attr('href'), function (result) {
                // now write to the popup
                console.log(result);
                popup.document.write(result.page_content);

                // or change the location
                // popup.location = 'someOtherPage.html';
            });
            */
        }
    });
});
