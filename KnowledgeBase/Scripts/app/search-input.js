; (function (pubsub, Application) {
    "use strict";

    Application.SearchInput = function Search(input) {
        this.input = input;

        this.addEventListener();
    }

    Application.SearchInput.prototype.addEventListener = function addEventListener() {
        this.input.on('keyup', this.onKeyDown);
    };

    Application.SearchInput.prototype.onKeyDown = function onKeyDown(event) {
        event.preventDefault();
        event.stopPropagation();

        pubsub('filterTrigged').broadcast(event.target.value);        
    };
})(radio, Application);