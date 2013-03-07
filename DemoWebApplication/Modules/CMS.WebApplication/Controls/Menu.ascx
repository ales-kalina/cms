<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="CMS.WebApplication.Controls.Menu" %>
<%@ Import Namespace="CMS.WebApplication" %>
<div class="well">
    <ul class="nav nav-list">
        <li><a href='<%= Page.GetRouteUrl("cms.webapplication.home", null) %>'>Home</a></li>
        <% foreach (MenuItemGroup group in MenuItemGroups) { %>
            <li class="nav-header"><%: group.Title %></li>
            <% foreach (CMS.WebApplication.MenuItem item in group.MenuItems) { %>
                <% if (CurrentRouteName != null && CurrentRouteName.StartsWith(item.RouteNamePrefix)) { %>
                <li class="active">
                <% } else { %>
                <li>
                <% } %>
                <a href='<%= item.Url() %>'><%: item.Title %></a></li>
            <% } %>
        <% } %>
    </ul>
</div>
