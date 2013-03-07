<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SourcePackageList.ascx.cs" Inherits="CMS.WebApplication.Controls.SourcePackageList" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/SourcePackageListItem.ascx" TagName="SourcePackageListItem" TagPrefix="custom" %>
<asp:Repeater ID="RepeaterSourcePackages" runat="server">
    <ItemTemplate>
        <custom:SourcePackageListItem runat="server" Model="<%# Container.DataItem %>" />
    </ItemTemplate>
</asp:Repeater>