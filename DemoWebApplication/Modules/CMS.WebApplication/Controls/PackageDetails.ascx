<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageDetails.ascx.cs" Inherits="CMS.WebApplication.Controls.PackageDetails" %>
<script runat="server">

    protected string FormatPackageDependency(NuGet.PackageDependency dependency)
    {
        return String.Format("{0} {1}", dependency.Id, dependency.VersionSpec);
    }

</script>
<p><%: Package.Description %></p>
<% if (!String.IsNullOrEmpty(Package.ReleaseNotes)) { %>
<p><b>Release notes:</b> <%: Package.ReleaseNotes %></p>
<% } %>
<dl>
    <dt>Owners</dt>
    <dd>
        <% if (Package.Owners.Any()) { %>
        <%: String.Join(", ", Package.Owners) %>
        <% } else { %>
        <span class="muted">Package has no owners</span>
        <% } %>
    </dd>
    <dt>Authors</dt>
    <dd>
        <% if (Package.Authors.Any()) { %>
        <%: String.Join(", ", Package.Authors) %>
        <% } else { %>
        <span class="muted">Package has no authors</span>
        <% } %>
    </dd>
    <dt>Tags</dt>
    <dd>
        <% if (Package.Tags != null && Package.Tags.Any()) { %>
        <%: String.Join(", ", Package.Tags) %>
        <% } else { %>
        <span class="muted">Package has no tags</span>
        <% } %>
    </dd>
    <dt>Dependencies</dt>
    <dd>
        <% NuGet.PackageDependencySet dependencySet = Package.DependencySets.Where(x => x.TargetFramework == TargetFramework).SingleOrDefault(); %>
        <% if (dependencySet != null && dependencySet.Dependencies.Any()) { %>
        <%= String.Join("<br />", dependencySet.Dependencies.Select(x => HttpUtility.HtmlEncode(FormatPackageDependency(x))))%>
        <% } else { %>
        <span class="muted">No dependencies</span>
        <% } %>
    </dd>
    <dt>Project site</dt>
    <dd>
        <% if (Package.ProjectUrl != null) { %>
        <a href="<%= Package.ProjectUrl %>"><%: Package.ProjectUrl %></a>
        <% } else { %>
        <span class="muted">Project site is not available</span>
        <% } %>
    </dd>
    <dt>License</dt>
    <dd>
        <% if (Package.LicenseUrl != null) { %>
        <a href="<%= Package.LicenseUrl %>"><%: Package.LicenseUrl %></a>
        <% } else { %>
        <span class="muted">No license is available</span>
        <% } %>
    </dd>
</dl>
