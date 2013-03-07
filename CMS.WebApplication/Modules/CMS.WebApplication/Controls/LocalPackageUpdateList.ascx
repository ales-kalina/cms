<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalPackageUpdateList.ascx.cs" Inherits="CMS.WebApplication.Controls.LocalPackageUpdateList" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/LocalPackageUpdateListItem.ascx" TagName="LocalPackageUpdateListItem" TagPrefix="custom" %>
<asp:Repeater ID="RepeaterLocalPackageUpdates" runat="server">
    <ItemTemplate>
        <custom:LocalPackageUpdateListItem runat="server" Model="<%# Container.DataItem %>" />
    </ItemTemplate>
</asp:Repeater>