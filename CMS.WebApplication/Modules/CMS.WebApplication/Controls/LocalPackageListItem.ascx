<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalPackageListItem.ascx.cs" Inherits="CMS.WebApplication.Controls.LocalPackageListItem" %>
<div class="well">
<h4><%: Model.Package.Id %> <%: Model.Package.Version %><% if (Model.UpdatePackage != null) { %><span class="label label-info pull-right">New version is available</span><% } %></h4>
<p><%: Model.Package.Description %></p>
<div>
    <a class="btn btn-primary" href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.details", new { id = Model.Package.Id }) %>'>Details</a>
    <% if (!Model.HasDependentPackages && !Model.IsProjectPackage) { %> <a class="btn" href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.remove", new { id = Model.Package.Id }) %>'>Remove</a><% } %>
    <% if (Model.UpdatePackage != null) { %> <a class="btn" href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.updates", new { id = Model.Package.Id }) %>'>Update</a><% } %>
</div>
</div>