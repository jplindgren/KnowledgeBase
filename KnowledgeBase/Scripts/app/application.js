; (function ($, notification) {
    var Application;
    window.Application = Application = {};

    Application.run = function (html) {
        this.html = html;

        Application.config();

        this.searchInput = new Application.SearchInput($("#search"));
        this.tagMenu = new Application.TagMenu($('.pill-link'), $('html,body'));
        this.articleList = new Application.ArticleList($('#origDivKnowledgeList'), $('#divKnowledgeList'));
        this.createArticlePopup = new Application.CreateArticlePopup($('.dropdown'), this.articleList);
    }

    Application.config = function () {
        notification.options.closeButton = true;
        notification.options.progressBar = true;
    }
})(jQuery, toastr);