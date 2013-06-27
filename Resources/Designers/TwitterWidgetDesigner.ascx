<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto;">
    <ol>
        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="MaximumNumberOfTweets" CssClass="sfTxtLbl">Maximum number of tweets</asp:Label>
            <asp:TextBox ID="MaximumNumberOfTweets" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Set the maximum number of tweets to load for this Twitter instance</div>
        </li>

        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="TwitterWidgetRenderMode" CssClass="sfTxtLbl">Render Mode</asp:Label>
            <asp:DropDownList ID="TwitterWidgetRenderMode" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Choose on what type of page this Twitter instance is rendered</div>
        </li>
    </ol>
</div>
