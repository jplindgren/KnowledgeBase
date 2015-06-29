var Options = function(storage, window, pageElements){
    'use strict';

    var elements = pageElements;

    function showTimedMessage(message, timeout){
        elements.status.textContent = message;
        hideElement(elements.content);
        setTimeout(function(){
            elements.status.textContent = '';
            showElement(elements.content);
        }, timeout);
    }

    function clearUrl(){
        elements.url.value = '';
    }

    function hideElement(element){
        element.style.display = 'none';
    }

    function showElement(element){
        element.style.display = 'block';
    }

    function showSaveTimedMessage(){
        showTimedMessage('Options saved.', 3000);
    }

    // Saves options to chrome.storage
    function save_options() {
        var url = elements.url.value;
        storage.sync.set({
            serverUrl: url
        }, showSaveTimedMessage);
    }

    // stored in .
    function restore_options() {
        storage.sync.get('serverUrl', function(items) {
            elements.url.value = items.serverUrl || '';
        });
    }

    function clear_options(){
        storage.sync.clear(function() {
            clearUrl();
            showTimedMessage('Options cleaned.', 1500);
        });
    }

    function addListeners(){
        document.addEventListener('DOMContentLoaded', restore_options);
        elements.save.addEventListener('click', save_options);
        elements.clear.addEventListener('click', clear_options);
    }

    addListeners();

    return {
        clearUrl: clearUrl,
        save_options: save_options,
        showTimedMessage: showTimedMessage,
        showSaveTimedMessage: showSaveTimedMessage
    };
};