<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add a New OpenID
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

<h2>Login with your OpenID to added it to your account</h2>
<%-- The login form will use POST and go to /User/AddOpenID. --%>
<% using (Html.BeginForm("AddOpenID", "User", FormMethod.Post, new { id = "openid_form" }))
{ %>
	<%: Html.Partial("_OpenIdSelector")%>
<% } %>

</asp:Content>
