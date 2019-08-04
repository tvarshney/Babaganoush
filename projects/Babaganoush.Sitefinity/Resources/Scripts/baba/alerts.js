define([
	'jquery'
], function ($) {

    return {

        initLoading: function (message, timeout) {
            //TODO
        },

        exitLoading: function () {
            //TODO
        },

        initProgress: function () {
            //TODO
        },

        exitProgress: function () {
            //TODO
        },

        initSpinner: function () {
            //TODO
        },

        exitSpinner: function () {
            //TODO
        },

        success: function (message, title) {
            this.exitLoading();
            //TODO
        },

        info: function (message, title) {
            this.exitLoading();
            //TODO
        },

        warning: function (message, title, icon, timeout) {
            this.exitLoading();
            //TODO
        },

        error: function (message, title) {
            this.exitLoading();
            //TODO
        },

        modalConfirm: function (message, fnUnblock, timeout) {
            //TODO
        },

        modal: function (content, options) {
            var me = this;
            options = options || {};

            /*EXAMPLE USE:
            Alerts.modal('Welcome lorem ipsum.....abc 123', {
                title: 'This is a title test',
                fnSubmit: function (e, el, bodyEl) {
                    bodyEl.append('<br />SUBMIT FORM!!');
                }
            });
            */

            //TODO
        },

        hideModal: function () {
            //TODO
        }
    };
});