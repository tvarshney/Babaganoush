//EXTEND REQUIREJS CONFIG
require.config({
    paths: {
        exampleplugin: 'libs/example/exampleplugin.v1.2.3.min'
    },
    shim: {
        exampleplugin: ['jquery']
    }
});

//INITIALIZE MY APP
require([
    'jquery',
    'underscore',
    'utils/helpers',
    'exampleplugin'
], function ($, _, Helpers) {

    var init = function () {
        console.log(_.slugify(document.title));

        //INITIALIZE APP PARTS
        initElements();
    };

    var initElements = function () {
        //ON DOC READY
        $(function () {
            Helpers.runTest('aasfssvrs');
        });
    };

    //INITIALIZE APP
    init();
});