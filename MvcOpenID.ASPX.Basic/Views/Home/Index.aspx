<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Overview</h2>

<p>MvcOpenID is an OpenID starter kit for ASP.NET MVC. It is first and foremost a learning tool. I've learned a lot about OpenID while developing this and commented the code a lot. Running the application and stepping through the code is the best and probably the fastest way to learn. Apart from it's educational value you can also use it as a starter kit for your ASP.NET MVC based web applications.</p>

<h2>Status and future plans</h2>

<p>This project has just started so I will be adding more to it. Right now there is only the basic support for OpenID. </p>

<p>In the future I hope to implement:</p>

<ul>
<li>Pop up window for signing in to you OpenID provider.</li>
<li>AJAX pop up for signing in to you OpenID provider.</li>
<li>Make all versions available for Razor and ASPX view engines.</li>
</ul>

<h2>Request to users</h2>

<p>Currently the DotNetOpenAuth library's ASP.NET MVC HtmlHelper extensions do not support Razor view engine. This is proving to be quite a road block. I've posted <a href="http://mvcopenid.codeplex.com/goo.gl/OaAGE">the idea</a> to fix this on DotNetOpenAuth's UserVoice. I would very much appreciate your votes on this matter.</p>

<h2>License</h2>

<p>This project is licensed under <a href="http://opensource.org/licenses/apache2.0.php">Apache License, Version 2.0</a>. See the <a href="http://mvcopenid.codeplex.com">License tab</a> on CodePlex or LICENSE.txt in the source for more info.</p>

<h2>Community</h2>

<p>At Codeplex: <a href="http://mvcopenid.codeplex.com/discussions">http://mvcopenid.codeplex.com/discussions</a></p>

<h2>Requirements</h2>

<ul>
<li>.NET Framework 4</li>
<li>Entity Framework 4.0</li>
<li>ASP.NET MVC 3 (Release Candidate 2) - <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=955d593e-cbd1-4ed1-88eb-02ff79dd74d8&amp;displaylang=en">Download here</a></li>
<li>Code Contracts - <a href="http://msdn.microsoft.com/en-us/devlabs/dd491992.aspx">Download here</a></li>
<li>SQL Server 2008 R2 Express <a href="https://www.microsoft.com/express/Database/Default.aspx">https://www.microsoft.com/express/Database/Default.aspx</a></li>
</ul>

<h2>Resources</h2>

<p>This project uses (but is not affiliated to):</p>

<ul>
<li><a href="http://www.dotnetopenauth.net/">DotNetOpenAuth</a> developed by <a href="http://blog.nerdbank.net/">Andrew Arnott</a></li>
<li><a href="http://code.google.com/p/openid-selector/">openid-selector</a></li>
</ul>

<h2>Source</h2>

<h3>Hosting</h3>

<p>The source of this projects is located in two places:</p>

<ul>
<li>GitHub: <a href="https://github.com/gligoran/mvcopenid">https://github.com/gligoran/mvcopenid</a></li>
<li>Codeplex: <a href="http://mvcopenid.codeplex.com/">http://mvcopenid.codeplex.com/</a></li>
</ul>

<h3>Download</h3>

<p>To get the source clone to a local repository. You can use either:</p>

<ul>
<li>Git: <code>git clone git://github.com/gligoran/mvcopenid</code>.</li>
<li>Mercurial (Hg): <code>hg clone https://hg01.codeplex.com/mvcopenid</code>.</li>
</ul>

<h2>Credits</h2>

<p>This project was created by me, <a href="http://www.gorangligorin.com">Goran Gligorin</a>, as a starting point for my projects that employ OpenID as a login system.</p>

<p>If you like this project tell people about it. A backlink to one or more of these locations would also be much appreciated:</p>

<ul>
<li><a href="http://www.gorangligorin.com">http://www.gorangligorin.com</a> - my personal homepage</li>
<li><a href="http://www.24projects.com">http://www.24projects.com</a> - my future website that will host my projects (W.I.P.)</li>
<li><a href="https://github.com/gligoran/mvcopenid">https://github.com/gligoran/mvcopenid</a> - project's home on GitHub</li>
<li><a href="http://mvcopenid.codeplex.com/">http://mvcopenid.codeplex.com/</a> - project's home at CodePlex</li>
</ul>

<h2>Thanks &amp; Recognition</h2>

<p>I would like to thank these people:</p>

<ul>
<li><a href="http://blog.nerdbank.net/">Andrew Arnott</a>: For making DotNetOpenAuth without which this project would not exist. Also thanks to Andrew for helping me with choosing a license on StackOverflow.</li>
<li><a href="http://programmers.stackexchange.com/users/2344/david-thornley">David Thornley</a>: Also for helping me choose a license on Programmers at StackExchenge.</li>
</ul>


</asp:Content>
