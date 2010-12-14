<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add a New OpenID
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login with your OpenID to added it to your account</h2>

<%-- The login form will use POST and go to /User/AddOpenID. --%>
<% using (Html.BeginForm("AddOpenID", "User", FormMethod.Post))
{ %>
    <%: Html.Partial("_OpenIdIdentifierField") %>
    <div><input id="openidlogin" type="submit" value="Add" /></div>
<% } %>

</asp:Content>
