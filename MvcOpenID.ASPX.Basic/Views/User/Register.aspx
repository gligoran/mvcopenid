<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcOpenID.ASPX.Basic.ViewModels.RegistrationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Registration</h2>

<div>To complete the registration for <strong><%: Model.Username %></strong> with OpenID <strong><%: Model.OpenIdUrl %></strong> please fill out the following data:</div>
<% using (Html.BeginForm("Register", "User", new { returnUrl = Model.ReturnUrl }, FormMethod.Post))
{ %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Username) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Username) %>
            <%: Html.ValidationMessageFor(model => model.Username) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Email) %>
            <%: Html.ValidationMessageFor(model => model.Email) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FullName) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FullName) %>
            <%: Html.ValidationMessageFor(model => model.FullName) %>
        </div>
        
        <input type="hidden" name="identifier" value="<%: Model.OpenIdUrl %>" />
        <input type="hidden" name="rememberMe" value="<%: Model.RememberMe %>" />

        <p>
            <input type="submit" value="Register" />
        </p>
    </fieldset>
<% } %>


</asp:Content>

