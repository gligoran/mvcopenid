<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcOpenID.ASPX.Basic.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>You are logged in as:</h2>

<div><%: Html.ActionLink("Edit", "Edit") %> | <%: @Html.ActionLink("Delete", "Delete") %></div>

<fieldset>
    <legend>Fields</legend>

    <div class="display-label">Username</div>
    <div class="display-field"><%: Model.Username %></div>

    <div class="display-label">Email</div>
    <div class="display-field"><%: Model.Email %></div>

    <div class="display-label">FullName</div>
    <div class="display-field"><%: Model.FullName %></div>
<div class="display-label">OpenIDs:</div>
    <div class="display-field">
        <ul>
            <% foreach (var openid in Model.OpenIds)
            { %>
                <li>
                    <%: openid.OpenIdUrl %>
                    <% if (Model.OpenIds.Count > 1)
                    { %>
                        <%: Html.ActionLink("Remove", "RemoveOpenID", "User", new { identifier = openid.OpenIdUrl }, null) %>
                    <% } %>
                </li>
            <% } %>
            <li><%: Html.ActionLink("Add a new OpenID", "AddOpenID", "User") %></li>
        </ul>
    </div>
</fieldset>

</asp:Content>

