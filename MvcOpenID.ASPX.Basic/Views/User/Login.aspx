<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login</h2>

<%-- The login form will use POST and go to /User/Login. If returnUrl was set we leave it in the url. --%>
<% using (Html.BeginForm("Login", "User", new { returnUrl = Request.QueryString["returnUrl"] }, FormMethod.Post))
{ %>
    <%: Html.Partial("_OpenIdIdentifierField") %>
    <div>Remember me? <%: Html.CheckBox("remeberMe", true) %></div>
    <div><input id="openidlogin" type="submit" value="Login" /></div>
<% } %>

</asp:Content>
