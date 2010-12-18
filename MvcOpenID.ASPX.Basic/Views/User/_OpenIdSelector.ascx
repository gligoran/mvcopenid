<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<input type="hidden" name="action" value="verify" />
<div id="openid_choice">
	<p>Do you already have an account on one of these sites? Click the logo to <b>log in</b> with it here: </p>
	<div id="openid_btns">
	</div>
</div>
<div id="openid_input_area">
	<input id="openid_identifier" name="openid_identifier" type="text" value="http://" />
	<input id="openid_submit" type="submit" value="Login" />
</div>
<noscript>
	<p>OpenID is service that allows you to log-on to many different websites using a single indentity. Find out <a href="http://openid.net/what/">more about OpenID</a> and <a href="http://openid.net/get/">how to get an OpenID enabled account</a>.</p>
</noscript>
