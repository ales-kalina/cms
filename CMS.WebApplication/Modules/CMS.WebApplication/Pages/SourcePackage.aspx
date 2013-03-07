<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SourcePackage.aspx.cs" Inherits="CMS.WebApplication.Pages.SourcePackage" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/PackageDetails.ascx" TagName="PackageDetails" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1><%: Model.Package.Id %> <small><%: Model.Package.Version %></small></h1>
    </div>
    <custom:PackageDetails ID="PackageDetails" runat="server" Package="<%# Model.Package %>" />
    <% if (!Model.IsInstalled) { %>
    <a class="btn btn-large btn-primary" href='<%= Page.GetRouteUrl("cms.webapplication.sourcepackage.install", new { id = Model.Package.Id }) %>'>Install</a>
    <% } %>
</asp:Content>
