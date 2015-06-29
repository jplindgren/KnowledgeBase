module('Options',{
    setup: function() {
        // prepare something for each following tests
        $('#qunit-fixture').append(
                      "<div id='content'>" + 
                        "<label>Choose your server url:</label>" +
                        "<input type='text' class='control box' id='url'>" + 
                      "</div>" + 
                      "<div id='status'></div>" +
                      "<button class='control btn' id='save'>Save</button>" +
                      "<button class='control btn' id='clear'>Clear</button>)");
        
        window.fakeStorage = {
    		sync: {
    			get: sinon.spy(),
    			set: sinon.spy()
    		}
        };

        window.pageElements = {
            content: document.getElementById('content'),
            status: document.getElementById('status'),
            url: document.getElementById('url'),
            save: document.getElementById('save'),
            clear: document.getElementById('clear')
        };
    },
    teardown: function() {
        // clean up after each test
        options = null;
    }
});

function assignValues(status, url){
    document.getElementById('status').value = status;
    document.getElementById('urlElement').value = url;
}

QUnit.test( "hello test", function( assert ) {
  assert.ok( 1 == "1", "Passed!" );
});

QUnit.test( "should show correct greeting message", function( assert ) {
  assert.ok( "Choose your server url:", document.getElementsByTagName('label')[0].innerHTML );
});

QUnit.test("clear url", function( assert ) {
    var sut = new Options(window.fakeStorage, window, pageElements);
    sut.clearUrl();
    assert.ok( pageElements.url.value == '');
});

QUnit.test("save options", function( assert ) {
    pageElements.url.value = 'http://mycontent.com';

    var sut = new Options(window.fakeStorage, window, pageElements);
    sut.save_options();

    ok(window.fakeStorage.sync.set.called, "set storage was called!");
    deepEqual(window.fakeStorage.sync.set.args[0][0], { serverUrl: pageElements.url.value }, 'url was passed as first parameter');
    deepEqual(window.fakeStorage.sync.set.args[0][1], sut.showSaveTimedMessage, 'correct callback was passed as seconde parameter');
    /*
    deepEqual(window.fakeStorage.sync.set.args[0][1], function() {
            sut.showTimedMessage('Options saved.', 3000);
        }, 'Second parameter');
    */
});

QUnit.test("showTimedMessage", function( assert ) {
    var timeout = 50;
    var status = document.getElementById('status');
    var content = document.getElementById('content');
    var sut = new Options(window.fakeStorage, window, pageElements);
    sut.showTimedMessage('my message', timeout);

    equal(status.textContent, 'my message', "correct message is shown as status");
    ok(content.style.display === 'none', 'content were hidden');
    
    var done = assert.async();  
    setTimeout(function() {
        ok(content.style.display === 'block', 'after a specific time, content is shown again');
        equal(status.style.display, '', 'after a specific time, status is clean again');
        done();
    }, timeout);
});