<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>

<sf:ResourceLinks ID="resourcesLinks1" runat="server" UseEmbeddedThemes="true" Theme="Default">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.css" Static="True" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.CodeMirror.Theme.default.css" Static="True" />
</sf:ResourceLinks>

<!-- TODO: MOVE TO EMBEDDED STYLESHEET IF POSSIBLE -->
<style scoped>
    .CodeMirror {
        width: 553px;
    }

    .CodeMirror-scroll {
        background-color: #FFFFFF;
        border: 1px solid #ccc;
    }
</style>

<asp:Label ID="titleLabel" runat="server" CssClass="sfTxtLbl" />
<asp:TextBox ID="fieldBox" runat="server" CssClass="sfTxt" TextMode="MultiLine" />
<sf:SitefinityLabel id="descriptionLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfDescription" />
<sf:SitefinityLabel id="exampleLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfExample" />

