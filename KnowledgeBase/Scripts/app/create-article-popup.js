; (function ($, pubsub, Application) {
    Application.CreateArticlePopup = function CreateArticlePopup(popupContainer, articleList) {
        this.popupContainer = popupContainer;
        this.openButton = this.popupContainer.find('#openAddArticlePopup');
        this.form = $(this.popupContainer.find('form')[0]);
        this.action = "/Knowledge/AddArticle";
        this.articleList = articleList;

        this.init();
    }

    Application.CreateArticlePopup.prototype.init = function init() {
        this.popupContainer.on('show.bs.dropdown', function (e) {
            $(this).find('.dropdown-menu').first().stop(true, true).slideDown(200);
        });

        // ADD SLIDEUP ANIMATION TO DROPDOWN //
        this.popupContainer.on('hide.bs.dropdown', function (e) {
            $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
        });

        this.addEventListeners();
    }

    Application.CreateArticlePopup.prototype.addEventListeners = function addEventListeners() {
        this.form.on('submit', this.addArticle.bind(this));
    }

    Application.CreateArticlePopup.prototype.addArticle = function (event) {        
        var that = this;
        event.preventDefault();
        event.stopPropagation();
        $.ajax({
            url: this.action,
            type: 'POST',
            data: this.form.serialize(),
            success: function (result) {
                that.articleList.reload();
                that.clear();
                toastr.success('Operation Complete', 'Article added with success');
            },
            error: function () {
                toastr.error('Sorry...', 'An error ocorred. Please retry later.');
            }
        });
    }

    Application.CreateArticlePopup.prototype.clear = function () {
        this.form[0].reset();
        //this.togglePopup();
    }
})(jQuery, radio, Application);