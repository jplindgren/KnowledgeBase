(function(){
	Options(chrome.storage, window, {
	    content: document.getElementById('content'),
	    status: document.getElementById('status'),
	    url: document.getElementById('url'),
	    save: document.getElementById('save'),
	    clear: document.getElementById('clear')
	});
})