Type.registerNamespace("Babaganoush.Sitefinity.Content.Fields");

Babaganoush.Sitefinity.Content.Fields.CodeEditor = function (element) {
    Babaganoush.Sitefinity.Content.Fields.CodeEditor.initializeBase(this, [element]);
    this._element = element;
    this._labelElement = null;
    this._textBoxElement = null;
    this._codeMirror = null;
}

Babaganoush.Sitefinity.Content.Fields.CodeEditor.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */      
        Babaganoush.Sitefinity.Content.Fields.CodeEditor.callBaseMethod(this, "initialize");

        //Determine code type if applicable
        var editorMode = 'htmlmixed';
        if (this._fieldName) {
            var field = this._fieldName.toLowerCase();

            //Determine code type by label
            //TODO: Setting the "DefinitionElement" would be more accurate
            if (field === 'js'
                || field.indexOf('javascript') !== -1
                || field.indexOf('script') !== -1)
                editorMode = 'javascript';
            else if (field === 'css'
                || field.indexOf('style') !== -1)
                editorMode = 'css';
            else if (field === 'xml'
                || field.indexOf('xml') !== -1)
                editorMode = 'xml';
            else if (field === 'json'
                || field.indexOf('option') !== -1)
                editorMode = { name: 'javascript', json: true };
        }

        //Initalize codemirror
        this._codeMirror = CodeMirror.fromTextArea(this._textBoxElement, {
            mode: editorMode,
            lineNumbers: true,
            matchBrackets: true,
            tabMode: 'classic'
        });
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */   
        Babaganoush.Sitefinity.Content.Fields.CodeEditor.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    _getTextValue: function(){
        if (this._textBoxElement) {
            return this._textBoxElement.value;
        }
        return null;
    },

    _clearTextBox: function () {
        if (this._codeMirror) this._codeMirror.setValue("");
        else if (this._textBoxElement != null) this._textBoxElement.value = "";
    },    

    /* --------------------------------- properties -------------------------------------- */
    get_value: function () {    
        return this._codeMirror.getValue();
    },

    set_value: function (value) {
        this._clearTextBox();
        if (value !== undefined && value != null && this._textBoxElement != null) {
            if (this._codeMirror) this._codeMirror.setValue(value);
            else this._textBoxElement.value = value;
        }
        this._value = value;
    },
    
    get_textBoxElement: function () {
        return this._textBoxElement;
    },

    set_textBoxElement: function (value) {
        this._textBoxElement = value;
    }    
};

Babaganoush.Sitefinity.Content.Fields.CodeEditor.registerClass("Babaganoush.Sitefinity.Content.Fields.CodeEditor", Telerik.Sitefinity.Web.UI.Fields.FieldControl);