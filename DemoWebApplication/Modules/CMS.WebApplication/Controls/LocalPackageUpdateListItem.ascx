<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalPackageUpdateListItem.ascx.cs" Inherits="CMS.WebApplication.Controls.LocalPackageUpdateListItem" %>
<div class="well">
<h4><%: Model.UpdatePackage.Id %> <%: Model.UpdatePackage.Version %></h4>
<% if (!String.IsNullOrEmpty(Model.UpdatePackage.ReleaseNotes)) { %>
<p><%: Model.Package.ReleaseNotes %></p>
<% } %>
<div>
    <a class="btn btn-primary" href='<%= Page.GetRouteUrl("cms.webapplication.localpackage.update", new { id = Model.Package.Id, version = Model.UpdatePackage.Version }) %>'>Update</a>
</div>
</div>