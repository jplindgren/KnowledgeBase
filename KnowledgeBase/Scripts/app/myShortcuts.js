$(function () {
    shortcut.add("Ctrl+A", 	function() { 
        $('#openAddArticlePopup').dropdown('toggle');
        var focusControl = $('#name');
        if (focusControl.is(':visible'))
            focusControl.focus();

    }, { 'type': 'keydown', 'propagate': false, 'target': document });

    shortcut.add("Ctrl+S", function (){
        $('#search').focus();
    }, { 'type': 'keydown', 'disable_in_input:': true, 'target': document });
});