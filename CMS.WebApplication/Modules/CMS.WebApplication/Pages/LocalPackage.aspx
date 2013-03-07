<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="LocalPackage.aspx.cs" Inherits="CMS.WebApplication.Pages.LocalPackage" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/PackageDetails.ascx" TagName="PackageDetails" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1><%: Model.Package.Id %> <small><%: Model.Package.Version %></small></h1>
    </div>
    <custom:PackageDetails ID="PackageDetails" runat="server" Package="<%# Model.Package %>" />
    <% if (Model.UpdatePackage != null) { %>
    <a class="btn btn-large btn-primary" href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.update", new { id = Model.Package.Id, version = Model.UpdatePackage.Version }) %>'>Update</a>
    <% } %>
</asp:Content>
