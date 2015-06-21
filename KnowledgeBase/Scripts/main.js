$(function () {
  
    var _ = {};

    Application.run(document.body);

    //toastr.options.closeButton = true;
    //toastr.options.progressBar = true;

    //_.OpenWindowInNewTab = function(url){
    //    window.open(url, '_blank', 'toolbar=yes, location=yes, status=yes, menubar=yes, scrollbars=yes');
    //}

    //$('.openAllLinks').on('click', function (evt) {
    //    evt.preventDefault();

    //    var knowledge = evt.target.closest(".knowledge");
    //    var articles = $(knowledge).find('.article');

    //    for (var i = 0; i < articles.length; i++) {
    //        var linkArticle = $(articles[i]).find('.btn-default');

    //        //window.open(linkArticle.attr('href'), '_blank', 'toolbar=yes, location=yes, status=yes, menubar=yes, scrollbars=yes');
    //        _.OpenWindowInNewTab(linkArticle.attr('href'));

    //        /*  it should work when deploy
    //        var popup = window.open("about:blank", "myPopup");
    //        console.log(linkArticle.attr('href'));

    //        //do some ajax calls
    //        $.get(linkArticle.attr('href'), function (result) {
    //            // now write to the popup
    //            console.log(result);
    //            popup.document.write(result.page_content);

    //            // or change the location
    //            // popup.location = 'someOtherPage.html';
    //        });
    //        */
    //    }
    //});


    $(".navbar-fixed-top").autoHidingNavbar();

    //var root = $('html,body');
    //$('.pill-link').on('click', function (event) {
    //    event.preventDefault();
    //    root.animate({ scrollTop: $('[name="' + $.attr(this, 'href').substr(1) + '"]').offset().top - 40 }, 600);
    //});

    // ADD SLIDEDOWN ANIMATION TO DROPDOWN //
    $('.dropdown').on('show.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown(200);
    });

    // ADD SLIDEUP ANIMATION TO DROPDOWN //
    $('.dropdown').on('hide.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
    });

    var addArticleForm = $('#add-articleForm');
    var options = $('#optionsMenu');

    addArticleForm.on('submit', function (evt) {
        evt.preventDefault();
        evt.stopPropagation();
        $.ajax({
            url: "Knowledge/AddArticle",
            type: 'POST',
            data: addArticleForm.serialize(),
            success: function (result) {
                $("#origDivKnowledgeList").html('');
                $("#divKnowledgeList").load('Knowledge/GetKnowledgeList');
                $(addArticleForm)[0].reset();
                $('#addArticleButton').dropdown('toggle');
                toastr.success('Operation Complete', 'Article added with success');
            },
            error: function () {
                toastr.error('Sorry...', 'An error ocorred. Please retry later.');
            }
        });
    });
    
});
