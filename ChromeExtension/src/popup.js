// Copyright (c) 2014 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.
;(function(){
    'use strict';
    var code = 'var meta = document.querySelector("meta[name=\'description\']");' + 
               'if (meta) meta = meta.getAttribute("content");' +
               'else { meta = document.querySelector("meta[name=\'twitter:description\']"); ' +
               'if (meta) meta = meta.getAttribute("content"); ' +
               '}' +
               '({' +
               '    title: document.title,' +
               '    description: meta || ""' +
               '});';

    var settingsServerUrl = '';
    var requestTimeout = 1000 * 3;  // 3 seconds

    // elements
    var loading = document.getElementById('loading');
    var form = document.getElementById('form');

    var save = document.getElementById('save');
    var tagElement = document.getElementById('tag');
    var articleElement = document.getElementById('name');
    var descriptionElement = document.getElementById('description');

    var statusMessage = document.getElementById('status');
    var settings = document.getElementById('settings');
    var goSettings = document.getElementById('goSettings');


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
    * @param {function(jsonObject)} successHandler - function to render progress of operation
     * The callback gets the object, and process it.
     * @param {function(string)} errorHandler - Called when the ajax operation fail.
     *   The callback gets a string that describes the failure reason.
    */
    function createXMLHttpRequest(url, method, successHandler, errorHandler, body) {
        var xhr = new XMLHttpRequest();
        xhr.open(method, url);
        xhr.onreadystatechange = function () {
            if (xhr.readyState != 4)
                return;

            if (xhr.status === 200) {
                var results = '';
                if (xhr.responseText){
                    results = JSON.parse(xhr.responseText);
                }                
                if (successHandler) successHandler(results);
            } else {
                if (errorHandler) errorHandler("Error " + xhr.status);
            }
        };

        xhr.onerror = function(error) {
            if (errorHandler) errorHandler("Error " + error);
        };
        return xhr;
    }

    function makePOST(url, successHandler, errorHandler, bodyObject){
        var xhr = createXMLHttpRequest(url, 'POST', successHandler, errorHandler);
        xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        xhr.send(JSON.stringify(bodyObject));
    }

    function makeGET(url, successHandler, errorHandler){
        var xhr = createXMLHttpRequest(url, 'GET', successHandler, errorHandler);
        xhr.send();
    }

    /**
     * @param {string} article - Javascript Object with info to send to server.
     */
    function sendArticle(article){
        var articleUrl = settingsServerUrl + "/knowledge/AddArticle";
        showLoading();
        var successHandler = function(message) {
            form.style.display = "none";
            hideLoading();
            renderStatus('Article sent with sucess');
            markAsSent(article);
        };
        var errorHandler = function(error){
            hideLoading();
            renderStatus(error);
        };
        makePOST(articleUrl, successHandler, errorHandler, article);
    }

    function markAsSent(article){
        var sentArticles = [];
    }

    function getTags(){
        var tagsUrl = settingsServerUrl + "/tag/index";
        var errorHandler = function(error){
            renderStatus('Error getting tags - ' + error);
        };
        makeGET(tagsUrl, setAutoCompleteTags, errorHandler);
    }

    function setAutoCompleteTags(tags){
        autoComplt.enable(tagElement, {
            // the hintsFetcher is your customized function which searchs the proper autocomplete hints based on the user's input value.
            hintsFetcher : function (v, openList) {
                var hints = [],
                names = tags;

                for (var i = 0; i < names.length; i++) {
                    if (names[i].indexOf(v) >= 0) {
                        hints.push(names[i]);
                    }
                }

                openList(hints);
            }
        });
    }

    function renderStatus(statusText) {
      statusMessage.textContent = statusText;
    }

    function showLoading(){
        loading.className = 'show';
        save.className = save.className.replace( /(?:^|\s)show(?!\S)/g , '' );
        save.className += ' hidden';
    }

    function hideLoading(){
        loading.className = 'hidden';
        save.className = save.className.replace( /(?:^|\s)hidden(?!\S)/g , '' );
        save.className += ' show';
    }

    function extractHostPageData(){
        // to get meta we have to access through a content script?
        chrome.tabs.executeScript({
            code: code
        }, function(results) {
            if (!results) {
                descriptionElement.value = "";
            }
            var result = results[0];
            descriptionElement.value = result.description;
        });
    }

    function submit(e){
        e.preventDefault();
        getCurrentTab(function(url, tab) {
            var tag = tagElement.value;
            var description = descriptionElement.value;
            var name = tab.title;
            
            sendArticle({ tag: tag, name: name, description: description, link: url });
        });    
    }

    function defineVisibiltyBasedOnSettings(hasValidSettingsFunc, hasNotValidSettingsFunc){
        chrome.storage.sync.get("serverUrl", function(items) {
            if (!items.serverUrl){

                hasNotValidSettingsFunc();
                goSettings.addEventListener('click',function() {
                    if (chrome.runtime.openOptionsPage) {
                        // New way to open options pages, if supported (Chrome 42+).
                        chrome.runtime.openOptionsPage();
                    } else {
                        // Reasonable fallback.
                        window.open(chrome.runtime.getURL('options.html'));
                    }
                });
            }else{
                settingsServerUrl = items.serverUrl;
                getCurrentTab(hasValidSettingsFunc);
            }
        });
    }

    function hideForm(){
        form.style.display = "none";
        settings.style.display = "block";
        renderStatus('Server url not defined. Access your options pane and set your server url');
    }

    document.addEventListener('DOMContentLoaded', function() {

        defineVisibiltyBasedOnSettings(function(url, tab) {
            save.addEventListener('click', submit);
            articleElement.value = tab.title;
            extractHostPageData();        
            
            getTags();
        }, hideForm);
        
    });

})();