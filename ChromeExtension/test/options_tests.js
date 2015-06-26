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

QUnit.test( "clear url", function( assert ) {
  assert.ok( document.getElementById('url').value == '', clearUrl() );
});