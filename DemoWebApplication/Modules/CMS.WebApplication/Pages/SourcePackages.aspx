<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SourcePackages.aspx.cs" Inherits="CMS.WebApplication.Pages.SourcePackages" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/SourcePackageList.ascx" TagName="SourcePackageList" TagPrefix="custom" %>
<%@ Register src="~/Modules/CMS.WebApplication/Controls/PackageSearchForm.ascx" tagname="PackageSearchForm" tagprefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1>Available packages</h1>
    </div>
    <custom:PackageSearchForm ID="PackageSearchForm" runat="server" />
    <custom:SourcePackageList ID="SourcePackageList" runat="server" />
</asp:Content>
