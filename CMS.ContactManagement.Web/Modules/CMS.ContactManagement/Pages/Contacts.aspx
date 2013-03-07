<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/CMS.WebApplication/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="CMS.ContactManagement.Web.Contacts" %>
<%@ Register Src="~/Modules/CMS.ContactManagement/Controls/ContactList.ascx" TagName="ContactList" TagPrefix="custom" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="page-header">
        <h1>Contacts</h1>
    </div>
    <div class="input-append">
    <input class="span6" id="appendedInputButtons" type="text">
        <button class="btn" type="button">Search</button>
    </div>
    <br />
    <custom:ContactList ID="ContactList" runat="server" Model="<%# Model %>" />
</asp:Content>
