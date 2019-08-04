Type.registerNamespace("Babaganoush.Sitefinity.Content.Fields");

Babaganoush.Sitefinity.Content.Fields.PageSelector = function (element) {
    Babaganoush.Sitefinity.Content.Fields.PageSelector.initializeBase(this, [element]);
    this._dynamicModulesDataServicePath = null;
    this._dynamicModuleType = null;
    this._pageSelector = null;
}

Babaganoush.Sitefinity.Content.Fields.PageSelector.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */      
        Babaganoush.Sitefinity.Content.Fields.PageSelector.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */   
        Babaganoush.Sitefinity.Content.Fields.PageSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /*Gets the value of the field control.*/
    get_value: function ()
    {
        return this.get_pageSelector().get_value();
    },

    /*Sets the value of the text field control.*/
    set_value: function (value)
    {
        var t = this.get_pageSelector();
        if (value)
        {
            t.set_value(value);
        }
    },


    get_dynamicModulesDataServicePath: function ()
    {
        return this._dynamicModulesDataServicePath;
    },
    set_dynamicModulesDataServicePath: function (value)
    {
        this._dynamicModulesDataServicePath = value;
    },
    get_dynamicModuleType: function ()
    {
        return this._dynamicModuleType;
    },
    set_dynamicModuleType: function (value)
    {
        this._dynamicModuleType = value;
    },
    get_pageSelector: function ()
    {
        return this._pageSelector;
    },
    set_pageSelector: function (value)
    {
        this._pageSelector = value;
    }
};

Babaganoush.Sitefinity.Content.Fields.PageSelector.registerClass("Babaganoush.Sitefinity.Content.Fields.PageSelector", Telerik.Sitefinity.Web.UI.Fields.FieldControl);