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


    $(".navbar-fixed-top").autoHidingNavbar();
    var root = $('html,body');
    //$('a[href*=#]:not([href=#])').on('click', function (event) {
    //    event.preventDefault();
    //    root.animate({ scrollTop: $('[name="' + $.attr(this, 'href').substr(1) + '"]').offset().top - 40 }, 600);
    //});

    $("#search").on('keyup', function (event) {
        event.preventDefault();
        event.stopPropagation();
        if (!event.target.value) {
            $('.article').each(function (index) {
                $(this.closest('div')).show();
            });
        } else {
            var filteredArticles = $('.article[data-name*="' + event.target.value + '"]');
            var notFilteredArticles = $('.article:not([data-name*="' + event.target.value + '"])');

            $(filteredArticles).each(function (index) {
                $(this.closest('div')).show();
            });
            $(notFilteredArticles).each(function (index) {
                $(this.closest('div')).hide();
            });
        }
    });

    $('.pill-link').on('click', function (event) {
        event.preventDefault();
        root.animate({ scrollTop: $('[name="' + $.attr(this, 'href').substr(1) + '"]').offset().top - 40 }, 600);
    });

    var addArticleForm = $('#add-articleForm');

    addArticleForm.on('submit', function (evt) {
        evt.preventDefault();
        $.ajax({
            url: "Knowledge/AddArticle",
            type: 'POST',
            data: addArticleForm.serialize(),
            success: function (result) {
                $("#origDivKnowledgeList").html('');
                $("#divKnowledgeList").load('Knowledge/GetKnowledgeList');
                $(addArticleForm)[0].reset();
                $('#addArticleButton').dropdown('toggle');
                
            },
            error: function () {
                alert("Bad submit");
            }
        });
    });

    
});
