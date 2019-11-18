var editorGetHtml = function () {
    return tinymce.activeEditor.getContent();
}

var htmlContent;

var editorSetHtml = function (html) {
    if (tinymce.activeEditor) {
        tinymce.activeEditor.setContent(html);
    }
    else {
        // If not initialized yet, we need to delay it
        htmlContent = html;
    }
}

tinymce.init({
    selector: '#txtContent',
    plugins: [
        "advlist autolink lists link image charmap print preview anchor codesample toc help spoiler",
        "searchreplace visualblocks code fullscreen imagetools",
        "insertdatetime media table paste filemanager wordcount"
    ],
    codesample_languages: [
        { text: 'custom', value: 'custom' },
        { text: 'HTML/XML', value: 'markup' },
        { text: 'JavaScript', value: 'javascript' },
        { text: 'CSS', value: 'css' },
        { text: 'PHP', value: 'php' },
        { text: 'Ruby', value: 'ruby' },
        { text: 'Python', value: 'python' },
        { text: 'Java', value: 'java' },
        { text: 'C', value: 'c' },
        { text: 'C#', value: 'csharp' },
        { text: 'C++', value: 'cpp' }
    ],
    fontsize_formats: "6pt 7pt 8pt 9pt 10pt 11pt 12pt 13pt 14pt 15pt 16pt 17pt 18pt 19pt 20pt 24pt 36pt 40pt",
    toolbar: "undo redo | fontselect | fontsizeselect | styleselect | bold underline italic | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media sh4tinymce | fullscreen code | filemanager | codesample | spoiler-add spoiler-remove",
    autosave_ask_before_unload: false,
    max_height: 1000,
    min_height: 300,
    height: 500,
    menubar: 'edit insert view format table',
    relative_urls: false,
    browser_spellcheck: true,
    paste_data_images: true,
    setup: function (editor) {
        editor.on('init', function (e) {
            if (htmlContent) {
                editor.setContent(htmlContent);
            }
        });
    }
});
