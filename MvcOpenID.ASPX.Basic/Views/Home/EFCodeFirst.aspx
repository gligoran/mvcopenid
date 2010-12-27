<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Switch To Entity Framework 4 CTP5
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>MvcOpenID with Entity Framework 4 Code-First</h1>

<p><strong><em>Last updated on: 12/25/2010</em></strong></p>

<h2>Why Entity Framework Code-First?</h2>

<p>Microsoft ADO.NET Entity Framework 4.0 was released as part of .NET 4.0 on 12 April 2010. Since then Microsoft released 5 preview versions which demonstrate what Entity Framework will bring in the future. The CTP5 version is the latest (released 12/6/2010) of these preview versions. It is a preview of the Code First Programming Model and Productivity Improvements for Entity Framework 4. The final release of Code First is expected in the first quarter of next year (Q1 of 2011).</p>

<h2>Installing EFCodeFirst</h2>

<p>To unlock the new Code-First approach in Entity Framework you'll first need to switch your MvcOpenID to the <strong><em>EFCodeFirst</em></strong> branch. After this all you have to do is to install EFCTP5. There are 2 main ways of doing this:</p>

<ul>
<li>Download and install <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=35adb688-f8a7-4d28-86b1-b6235385389d">Microsoft ADO.NET Entity Framework Feature Community Technology Preview 5</a>. After the installation is complete add a reference to EntityFramework.</li>
<li>If you have <a href="http://nuget.codeplex.com/">NuGet Package Manager</a> installed you can download and add the required resources using it. NuGet has two ways for adding packages:
<ul>
<li><strong>Console</strong>: Open you Package Manager Console (View -> Other Windows -> Pacakge Manager Console) and type in 'Install-Package EFCodeFirst'.</li>
<li><strong>GUI</strong>: You can also do this by right clicking the References folder in Solution Explorer of your project and selecting <em>Add Library Package Reference...</em>. In the popup windows choose Online and enter EFCodeFirst in the search field. Select EFCodeFirst and click the Install button.</li>
</ul></li>
</ul>

<h2>Requirements</h2>

<p>Entity Framework Code-First does require some version of SQL Server. The simplest one to get is the SQL Server Compact Edition (SQLCE). It can be installed via <a href="http://nuget.codeplex.com/">NuGet</a>. The package for this is called <em>EFCodeFirst.SqlServerCompact</em>. The installation is done in a similar manner as the installation of <em>EFCodeFirst</em> with NuGet described in the previous section.</p>

<p>EFCodeFirst also works great with <a href="http://www.microsoft.com/sqlserver/2008/en/us/express.aspx">Microsoft SQL Server 2008 R2 Express Edition</a>. It comes with Microsoft SQL Server Management Studio which is a great tool for browsing around you database. This is also the only version of SQL Server I've tested this with. If you have any other version of SQL Server on your machine please let me know how it works at <a href="http://mvcopenid.codeplex.com/discussions">MvcOpenID's CodePlex discussion board</a>.</p>

<h2>The End</h2>

<p>You can now run your application. Everything should work fine. If not please see the next section on how to report the problem.</p>

<h2>Troubleshooting</h2>

<p>If anything goes wrong or if it doesn't work even after you've done all the steps here please report this at <a href="http://mvcopenid.codeplex.com/workitem/list/basic">MvcOpenID CodePlex Issue Tracker</a>.</p>

</asp:Content>
