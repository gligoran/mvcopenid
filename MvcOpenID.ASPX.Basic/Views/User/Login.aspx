<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login or Create New Account
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="<%: Url.Content("~/content/openid/openid.css") %>" rel="stylesheet" type="text/css" />
	<script src="<%: Url.Content("~/scripts/openid/openid-jquery.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/scripts/openid/openid-en.js") %>" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			openid.init('openid_identifier');
		});
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login or Create New Account</h2>
<%-- The login form will use POST and go to /User/Login. If returnUrl was set we leave it in the url. --%>
<% using (Html.BeginForm("Login", "User", new { returnUrl = Request.QueryString["returnUrl"] }, FormMethod.Post, new { id = "openid_form" }))
{ %>
	<%: Html.Partial("_OpenIdSelector")%>
<% } %>

</asp:Content>
