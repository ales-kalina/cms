<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RemoveLocalPackage.aspx.cs" Inherits="CMS.WebApplication.Pages.RemoveLocalPackage" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/PackageManagerMessageList.ascx" TagName="PackageManagerMessageList" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1><%: Package.Id %> <small><%: Package.Version %></small></h1>
    </div>
    <% if (Messages.Any(x => x.Level == NuGet.MessageLevel.Error || x.Level == NuGet.MessageLevel.Warning)) { %>
    <div class="alert alert-error">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <strong>Error!</strong> Package could not be removed. Please check the log for more information.
    </div>
    <% } else { %>
    <div class="alert alert-success">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <strong>Success!</strong> Package was removed.
    </div>
    <% } %>
    <custom:PackageManagerMessageList ID="PackageManagerMessageList" runat="server" Model="<%# Messages %>" />
    <% if (!Messages.Any(x => x.Level == NuGet.MessageLevel.Error || x.Level == NuGet.MessageLevel.Warning)) { %>
    <p>
        <a href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.list", null) %>' class="btn btn-primary">View packages</a>
    </p>
    <% } %>
</asp:Content>
