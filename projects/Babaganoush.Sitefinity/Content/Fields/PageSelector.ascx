<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>
<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.core.css" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.dialog.css" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.theme.sitefinity.css" />
</sf:ResourceLinks>

<sf:SitefinityLabel ID="titleLabel" runat="server" CssClass="sfTxtLbl" />
<sf:PageField ID="PageSelector" runat="server" WebServiceUrl="~/Sitefinity/Services/Pages/PagesService.svc/" DisplayMode="Write" />
<sf:SitefinityLabel ID="descriptionLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfDescription" />
<sf:SitefinityLabel ID="exampleLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfExample" />