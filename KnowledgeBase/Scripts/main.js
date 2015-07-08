$(function () {
  
    var _ = {};

    Application.run(document.body);

    $(".navbar-fixed-top").autoHidingNavbar();

    //// ADD SLIDEDOWN ANIMATION TO DROPDOWN //
    //$('.dropdown').on('show.bs.dropdown', function (e) {
    //    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(200);
    //});

    //// ADD SLIDEUP ANIMATION TO DROPDOWN //
    //$('.dropdown').on('hide.bs.dropdown', function (e) {
    //    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
    //});

    //var addArticleForm = $('#add-articleForm');
    var options = $('#optionsMenu');

    //addArticleForm.on('submit', function (evt) {
    //    evt.preventDefault();
    //    evt.stopPropagation();
    //    $.ajax({
    //        url: "Knowledge/AddArticle",
    //        type: 'POST',
    //        data: addArticleForm.serialize(),
    //        success: function (result) {
    //            $("#origDivKnowledgeList").html('');
    //            $("#divKnowledgeList").load('Knowledge/GetKnowledgeList');
    //            $(addArticleForm)[0].reset();
    //            $('#addArticleButton').dropdown('toggle');
    //            toastr.success('Operation Complete', 'Article added with success');
    //        },
    //        error: function () {
    //            toastr.error('Sorry...', 'An error ocorred. Please retry later.');
    //        }
    //    });
    //});
    
});
