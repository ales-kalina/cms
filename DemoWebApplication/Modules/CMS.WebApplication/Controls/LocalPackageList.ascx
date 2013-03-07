<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalPackageList.ascx.cs" Inherits="CMS.WebApplication.Controls.LocalPackageList" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/LocalPackageListItem.ascx" TagName="LocalPackageListItem" TagPrefix="custom" %>
<asp:Repeater ID="RepeaterLocalPackages" runat="server">
    <ItemTemplate>
        <custom:LocalPackageListItem runat="server" Model="<%# Container.DataItem %>" />
    </ItemTemplate>
</asp:Repeater>