; (function ($) {
    var Application;
    window.Application = Application = {};

    Application.run = function (html) {
        this.html = html;

        this.searchInput = new Application.SearchInput($("#search"));
        this.articleList = new Application.ArticleList($('#origDivKnowledgeList'));
        this.tagMenu = new Application.TagMenu($('.pill-link'), $('html,body'));
    }
})(jQuery);