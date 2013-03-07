<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackageManagerMessageList.ascx.cs" Inherits="CMS.WebApplication.Controls.PackageManagerMessageList" %>
<script runat="server">

    protected string GetMessageClassName(CMS.Core.PackageManagerMessage message)
    {
        switch (message.Level)
        {
            case NuGet.MessageLevel.Debug: return "muted";
            case NuGet.MessageLevel.Info: return "text-info";
            case NuGet.MessageLevel.Warning: return "text-warning";
            case NuGet.MessageLevel.Error: return "text-error";
            default: throw new ApplicationException("Unknown message level");
        }
    }

</script>
<p>
    <% foreach (CMS.Core.PackageManagerMessage message in Model) { %>
    <span class="<%= GetMessageClassName(message) %>"><%: message.Text %></span><br />
    <% } %>
</p>