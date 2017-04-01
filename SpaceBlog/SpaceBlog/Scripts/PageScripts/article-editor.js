
function tinyMceInit(textAreaId) {
    var editor;

    tinymce.init({
        selector: '#' + textAreaId,
        plugins: "lists",
        setup: editor => {
            editor = editor;
        }
    });
}