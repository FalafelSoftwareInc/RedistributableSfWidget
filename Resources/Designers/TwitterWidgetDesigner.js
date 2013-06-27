Type.registerNamespace("Falafel.Sitefinity.Modules.Twitter.Resources.Designers");

Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner = function (element) {
    this._maximumNumberOfTweets = null;
    this._twitterWidgetRenderMode = null;

    Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner.initializeBase(this, [element]);
}

Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI MaximumNumberOfTweets */
        jQuery(this.get_maximumNumberOfTweets()).val(controlData.MaximumNumberOfTweets);
        /* RefreshUI TwitterWidgetRenderMode */
        jQuery(this.get_twitterWidgetRenderMode()).val(controlData.TwitterWidgetRenderMode);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        controlData.MaximumNumberOfTweets = jQuery(this.get_maximumNumberOfTweets()).val();
        controlData.TwitterWidgetRenderMode = jQuery(this.get_twitterWidgetRenderMode()).val();
    },


    /* --------------------------------- properties -------------------------------------- */

    /* MaximumNumberOfTweets properties */
    get_maximumNumberOfTweets: function () { return this._maximumNumberOfTweets; }, 
    set_maximumNumberOfTweets: function (value) { this._maximumNumberOfTweets = value; },

    /* TwitterWidgetRenderMode properties */
    get_twitterWidgetRenderMode: function () { return this._twitterWidgetRenderMode; }, 
    set_twitterWidgetRenderMode: function (value) { this._twitterWidgetRenderMode = value; },
}

Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner.registerClass('Falafel.Sitefinity.Modules.Twitter.Resources.Designers.TwitterWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

