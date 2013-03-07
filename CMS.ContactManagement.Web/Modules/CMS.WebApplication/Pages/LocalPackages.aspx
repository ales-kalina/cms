<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="LocalPackages.aspx.cs" Inherits="CMS.WebApplication.Pages.LocalPackages" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/LocalPackageList.ascx" TagName="LocalPackageList" TagPrefix="custom" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/PackageSearchForm.ascx" TagName="PackageSearchForm" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1>Installed packages</h1>
    </div>
    <custom:PackageSearchForm ID="PackageSearchForm" runat="server" />
    <custom:LocalPackageList ID="LocalPackageList" runat="server" />
</asp:Content>
