//THIS FILE IS STREAMED AND MERGED WITH VARIABLES IN Babaganoush.Sitefinity.Mvc.Routes.RequireJsConfigModule
//THEN ADDED TO PAGE TO CONFIGURE REQUIREJS IN Babaganoush.Sitefinity.Utilities.RegisterClientSideStartup

//REGISTER THE SITEFINITY EMBEDDED RESOURCES
define('jquery', [], function () { return window.jQuery; });
define('kendo', [], function () { return kendo; });

//SPECIFY JQUERY COOKIE PATH IF NOT LOADED
if (!$.cookie) {
    require.config({
        paths: {
            cookie: '{1}'
        }
    });
} else {
    //REGISTER THE CURRENT JQUERY COOKIE PLUGIN
    define('cookie', [], function () { return $.cookie; });
}

//SPECIFY BLOCKUI PATH IF NOT LOADED
if (!$.blockUI) {
    require.config({
        paths: {
            blockui: '{14}'
        }
    });
} else {
    //REGISTER THE CURRENT JQUERY COOKIE PLUGIN
    define('blockui', [], function () { return $.blockUI; });
}
                
//CONFIFURE SHORTCUT ALIASES
require.config({
    baseUrl: '{0}',
    paths: {
        text: '{2}',
        css: '{3}',
        moment: '{4}',
        'moment-timezone': '{5}',
        lodash: '{6}',
        'underscore.string': '{7}',
        glide: '{8}',
        url: '{9}',
        loStorage: '{10}',
        'baba/api': '{11}',
        'baba/helpers': '{12}',
        'baba/alerts': '{13}'
    },
    // The shim config allows us to configure dependencies for
    // scripts that do not call define() to register a module
    shim: {
        url: {
            exports: 'url'
        }
    }
});

//HANDLE UNDERSCORE AND MERGING STRINGS PLUGIN
define('underscore', [
    'lodash',
    'underscore.string'
], function (_, _s) {
    //MERGE STRING PLUGIN TO UNDERSCORE NAMESPACE
    _.mixin(_s.exports());
    return _;
});

//INITIALIZE APP
require([
    'underscore'
], function (_) {

    var init = function () {
        //ADD TITLE TO HTML TAG AS CLASS
        if (document.title)
            $(document.documentElement)
              .addClass(_.slugify(document.title));

        //ON DOC READY
        $(function () {
            //INITIALIZE APP PARTS
            initElements();
        });
    };

    var initElements = function () {

    };

    //INITIALIZE APP
    init();
});