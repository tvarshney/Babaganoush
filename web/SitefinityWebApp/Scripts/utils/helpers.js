define([
    'underscore'
], function (_) {
    
    return {
        runTest: function (value) {
            console.log(_.ltrim(value, 'a'));
        }
    }
});