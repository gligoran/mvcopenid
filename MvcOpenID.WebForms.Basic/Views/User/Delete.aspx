<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcOpenID.WebForms.Basic.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Fields</legend>

    <div class="display-label">Username</div>
    <div class="display-field"><%: Model.Username %></div>

    <div class="display-label">Email</div>
    <div class="display-field"><%: Model.Email %></div>

    <div class="display-label">FullName</div>
    <div class="display-field"><%: Model.FullName %></div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <%: Html.HiddenFor(model => model.UserId) %>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back", "Index") %>
    </p>
<% } %>

</asp:Content>

