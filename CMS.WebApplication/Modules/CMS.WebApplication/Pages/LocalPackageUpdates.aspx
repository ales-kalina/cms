<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="LocalPackageUpdates.aspx.cs" Inherits="CMS.WebApplication.Pages.LocalPackageUpdates" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/LocalPackageUpdateList.ascx" TagName="LocalPackageUpdateList" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1>Available updates</h1>
    </div>
    <custom:LocalPackageUpdateList ID="LocalPackageUpdateList" runat="server" />
</asp:Content>
