; (function ($, pubsub, Application) {
    Application.ArticleList = function ArticleList(container) {
        this.container = container;
        this.subscribe();
    }

    Application.ArticleList.prototype.subscribe = function subscribe() {
        pubsub('filterTrigged').subscribe([this.filter, this]);
    }

    Application.ArticleList.prototype.reload = function reload() {
        
    }

    Application.ArticleList.prototype.filter = function filter(content) {
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
    }
})(jQuery, radio, Application)