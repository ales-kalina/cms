<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SourcePackageListItem.ascx.cs" Inherits="CMS.WebApplication.Controls.SourcePackageListItem" %>
<div class="well">
<h4><%: Model.Package.Id %> <%: Model.Package.Version %><% if (Model.IsInstalled) { %><span class="label label-info pull-right">Package is installed</span><% } %></h4>
<p><%: Model.Package.Description %></p>
<div>
    <a class="btn btn-primary" href='<%= Page.GetRouteUrl("cms.webapplication.sourcepackage.details", new { id = Model.Package.Id }) %>'>Details</a>
    <% if (!Model.IsInstalled) { %><a class="btn" href='<%= Page.GetRouteUrl("cms.webapplication.sourcepackage.install", new { id = Model.Package.Id }) %>'>Install</a><% } %>
</div>
</div>