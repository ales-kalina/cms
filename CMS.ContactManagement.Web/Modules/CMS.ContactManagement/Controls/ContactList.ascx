<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactList.ascx.cs" Inherits="CMS.ContactManagement.Web.ContactList" %>
<%@ Register Src="~/Modules/CMS.ContactManagement/Controls/ContactListItem.ascx" TagName="ContactListItem" TagPrefix="custom" %>
<asp:Repeater ID="RepeaterContacts" runat="server" DataSource="<%# Model %>">
    <HeaderTemplate>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <custom:ContactListItem runat="server" Model="<%# Container.DataItem %>" />
    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>
