<%@ Page Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="UpdateLocalPackage.aspx.cs" Inherits="CMS.WebApplication.Pages.UpdateLocalPackage" %>
<%@ Register Src="~/Modules/CMS.WebApplication/Controls/PackageManagerMessageList.ascx" TagName="PackageManagerMessageList" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1><%: LocalPackage.Id %> <small><%: LocalPackage.Version %></small></h1>
    </div>
    <% if (Messages.Any(x => x.Level == NuGet.MessageLevel.Error || x.Level == NuGet.MessageLevel.Warning)) { %>
    <div class="alert alert-error">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <strong>Error!</strong> Package could not be updated. Please check the log for more information.
    </div>
    <% } else { %>
    <div class="alert alert-success">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <strong>Success!</strong> Package was updated to version <%: SourcePackage.Version %>.
    </div>
    <% } %>
    <custom:PackageManagerMessageList ID="PackageManagerMessageList" runat="server" Model="<%# Messages %>" />
    <% if (!Messages.Any(x => x.Level == NuGet.MessageLevel.Error || x.Level == NuGet.MessageLevel.Warning)) { %>
    <p>
        <a href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.details", new { Id = LocalPackage.Id }) %>' class="btn btn-primary">View package</a>
    </p>
    <% } %>
</asp:Content>
