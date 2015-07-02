; (function ($, Application) {
    Application.TagMenu = function TagMenu(links, root) {
        this.links = links;
        this.root = root;
        this.addEventListener();
    }

    Application.TagMenu.prototype.addEventListener = function addEventListener() {
        this.links.on('click', this.goTo());
    }

    Application.TagMenu.prototype.goTo = function goTo() {
        var root = this.root;
        return function (event) {
            event.preventDefault();
            root.animate({ scrollTop: $('[id="' + $.attr(this, 'href').substr(1) + '"]').offset().top - 40 }, 600);
        }
    }
})(jQuery, Application);