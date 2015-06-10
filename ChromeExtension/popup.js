// Copyright (c) 2014 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

/**
 * Get the current URL.
 *
 * @param {function(string)} callback - called when the URL of the current tab
 *   is found.
 */
function getCurrentTab(callback) {
  // Query filter to be passed to chrome.tabs.query - see
  // https://developer.chrome.com/extensions/tabs#method-query
  var queryInfo = {
    active: true,
    currentWindow: true
  };

  chrome.tabs.query(queryInfo, function(tabs) {
    // chrome.tabs.query invokes the callback with a list of tabs that match the
    // query. When the popup is opened, there is certainly a window and at least
    // one tab, so we can safely assume that |tabs| is a non-empty array.
    // A window can only have one active tab at a time, so the array consists of
    // exactly one tab.
    var tab = tabs[0];

    // A tab is a plain object that provides information about the tab.
    // See https://developer.chrome.com/extensions/tabs#type-Tab
    var url = tab.url;

    // tab.url is only available if the "activeTab" permission is declared.
    // If you want to see the URL of other tabs (e.g. after removing active:true
    // from |queryInfo|), then the "tabs" permission is required to see their
    // "url" properties.
    console.assert(typeof url == 'string', 'tab.url should be a string');

    callback(url, tab);
  });

  // Most methods of the Chrome extension APIs are asynchronous. This means that
  // you CANNOT do something like this:
  //
  // var url;
  // chrome.tabs.query(queryInfo, function(tabs) {
  //   url = tabs[0].url;
  // });
  // alert(url); // Shows "undefined", because chrome.tabs.query is async.
}

/**
 * @param {string} knowledge - Javascript Object with info to send to server.
 * @param {function(string)} callback - function to render progress of operation
 * The callback gets the message, and display in a div.
 * @param {function(string)} errorCallback - Called when the ajax operation fail.
 *   The callback gets a string that describes the failure reason.
 */
function sendArticle(knowledge, callback, errorCallback){
    var serverUrl = "http://localhost:62431/knowledge/AddArticle"
    var x = new XMLHttpRequest();
    x.open('POST', serverUrl);
    x.setRequestHeader('Content-type','application/json; charset=utf-8');
    // The Google image search API responds with JSON, so let Chrome parse it.
    x.responseType = 'json';
    x.onload = function() {
        // Parse and process the response from Google Image Search.
        var response = x.response;        
        callback('sucesso!!');
    };
    x.onerror = function(e) {
        errorCallback(e);
    };
    x.send(JSON.stringify(knowledge));    
}

function renderStatus(statusText) {
  document.getElementById('status').textContent = statusText;
}

function submit(e){
    e.preventDefault();
    getCurrentTab(function(url, tab) {
        //var test = tab.getElementByTagName('H1')[0];
        
        /*
        var tag = document.getElementById('tag').value;
        var description = document.getElementById('description').value;
        var name = document.getElementById('name').value;
        renderStatus('URL: ' + url + '\n Tag: ' + tag + '\nName: ' + name + '\ndescription: ' + description);
        
        sendArticle( { tag: tag, name: name, description: description, link: url }, 
            function(successMessage) {
                renderStatus(successMessage);
            },
            function(errorMessage) {
                renderStatus(errorMessage);
            }
        );
           */ 
    });    
}

document.addEventListener('DOMContentLoaded', function() {
    getCurrentTab(function(url, tab) {
        renderStatus(tab.title);
        document.getElementById('name').value = tab.title;
    });
    
    document.getElementById('save').addEventListener('click', submit);
});
