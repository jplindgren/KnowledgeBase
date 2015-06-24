// Saves options to chrome.storage
function save_options() {
    var url = document.getElementById('url').value;
    chrome.storage.sync.set({
        serverUrl: url
    }, function() {
        // Update status to let user know options were saved.
        var status = document.getElementById('status');
        document.getElementById('content').style.display = 'none';
        status.textContent = 'Options saved.';
        setTimeout(function() {
          status.textContent = '';
          document.getElementById('content').style.display = 'block';
        }, 1500);
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
        var status = document.getElementById('status');
        document.getElementById('content').style.display = 'none';
        status.textContent = 'Options cleaned.';
        document.getElementById('url').value = '';
        setTimeout(function() {
          status.textContent = '';
          document.getElementById('content').style.display = 'block';
        }, 1500);
    });
}

document.addEventListener('DOMContentLoaded', restore_options);
document.getElementById('save').addEventListener('click', save_options);
document.getElementById('clear').addEventListener('click', clear_options);