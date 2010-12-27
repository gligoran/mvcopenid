# MvcOpenID with Entity Framework 4 Code-First
***Last updated on: 12/25/2010***

## Why Entity Framework Code-First?
Microsoft ADO.NET Entity Framework 4.0 was released as part of .NET 4.0 on 12 April 2010. Since then Microsoft released 5 preview versions which demonstrate what Entity Framework will bring in the future. The CTP5 version is the latest (released 12/6/2010) of these preview versions. It is a preview of the Code First Programming Model and Productivity Improvements for Entity Framework 4. The final release of Code First is expected in the first quarter of next year (Q1 of 2011).

## Installing EFCodeFirst
To unlock the new Code-First approach in Entity Framework you'll first need to switch your MvcOpenID to the ***EFCodeFirst*** branch. After this all you have to do is to install EFCTP5. There are 2 main ways of doing this:

* Download and install [Microsoft ADO.NET Entity Framework Feature Community Technology Preview 5](http://www.microsoft.com/downloads/en/details.aspx?FamilyID=35adb688-f8a7-4d28-86b1-b6235385389d). After the installation is complete add a reference to EntityFramework.
* If you have [NuGet Package Manager](http://nuget.codeplex.com/) installed you can download and add the required resources using it. NuGet has two ways for adding packages:
 * **Console**: Open you Package Manager Console (View -> Other Windows -> Pacakge Manager Console) and type in 'Install-Package EFCodeFirst'.
 * **GUI**: You can also do this by right clicking the References folder in Solution Explorer of your project and selecting *Add Library Package Reference...*. In the popup windows choose Online and enter EFCodeFirst in the search field. Select EFCodeFirst and click the Install button.

## Requirements
Entity Framework Code-First does require some version of SQL Server. The simplest one to get is the SQL Server Compact Edition (SQLCE). It can be installed via [NuGet](http://nuget.codeplex.com/). The package for this is called *EFCodeFirst.SqlServerCompact*. The installation is done in a similar manner as the installation of *EFCodeFirst* with NuGet described in the previous section.

EFCodeFirst also works great with [Microsoft SQL Server 2008 R2 Express Edition](http://www.microsoft.com/sqlserver/2008/en/us/express.aspx). It comes with Microsoft SQL Server Management Studio which is a great tool for browsing around you database. This is also the only version of SQL Server I've tested this with. If you have any other version of SQL Server on your machine please let me know how it works at [MvcOpenID's CodePlex discussion board](http://mvcopenid.codeplex.com/discussions).

## The End
You can now run your application. Everything should work fine. If not please see the next section on how to report the problem.

## Troubleshooting
If anything goes wrong or if it doesn't work even after you've done all the steps here please report this at [MvcOpenID CodePlex Issue Tracker](http://mvcopenid.codeplex.com/workitem/list/basic).