; (function ($, pubsub, Application) {
    Application.ArticleList = function ArticleList(container) {
        this.container = container;
        this.subscribe();
        this.addEventListener();
    }

    Application.ArticleList.prototype.subscribe = function subscribe() {
        pubsub('filterTrigged').subscribe([this.filter, this]);
    }

    Application.ArticleList.prototype.addEventListener = function addEventListener() {
        var openAllLinks = this.container.find('.openAllLinks');
        openAllLinks.on('click', function (evt) {
            evt.preventDefault();
            evt.stopPropagation();

            var knowledge = evt.target.closest(".knowledge");
            var articles = $(knowledge).find('.article');

            for (var i = 0; i < articles.length; i++) {
                var linkArticle = $(articles[i]).find('.btn-default');
                this.openWindowInNewTab(linkArticle.attr('href'));
            }
        }.bind(this));
    }

    Application.ArticleList.prototype.openWindowInNewTab = function openWindowInNewTab(url) {
        window.open(url, '_blank', 'toolbar=yes, location=yes, status=yes, menubar=yes, scrollbars=yes');
    }

    Application.ArticleList.prototype.reload = function reload() {
        
    }

    Application.ArticleList.prototype.filter = function filter(content) {
        if (!event.target.value) {
            $('.article').each(function (index) {
                $(this.closest('div')).show();
            });
        } else {
            var filteredArticles = $('.article[data-name*="' + event.target.value.toLowerCase() + '"]');
            var notFilteredArticles = $('.article:not([data-name*="' + event.target.value.toLowerCase() + '"])');

            $(filteredArticles).each(function (index) {
                $(this.closest('div')).show();
            });
            $(notFilteredArticles).each(function (index) {
                $(this.closest('div')).hide();
            });
        }
    }
})(jQuery, radio, Application)