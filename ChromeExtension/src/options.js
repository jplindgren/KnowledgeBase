//page elements
:(function(){
    var contentElement = document.getElementById('content');
    var statusElement = document.getElementById('status');
    var urlElement = document.getElementById('url');

    function showTimedMessage(message, timeout){
        statusElement.textContent = message;
        setTimeout(function(){
            statusElement.textContent = '';
            contentElement.style.display = 'block';
        }, timeout);
    }

    function clearUrl(){
        urlElement.value = '';
    }

    // Saves options to chrome.storage
    function save_options() {
        var url = document.getElementById('url').value;
        chrome.storage.sync.set({
            serverUrl: url
        }, function() {
            showTimedMessage('Options saved.');
        });
    }

    // stored in chrome.storage.
    function restore_options() {
        chrome.storage.sync.get('serverUrl', function(items) {
            document.getElementById('url').value = items.serverUrl || '';
        });
    }

    function clear_options(){
        chrome.storage.sync.clear(function() {
            clearUrl();
            showTimedMessage('Options cleaned.');
        });
    }

    document.addEventListener('DOMContentLoaded', restore_options);
    document.getElementById('save').addEventListener('click', save_options);
    document.getElementById('clear').addEventListener('click', clear_options);
})();