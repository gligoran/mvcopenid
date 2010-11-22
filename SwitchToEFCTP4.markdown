# OpenID Starter Kit for ASP.NET MVC with Entity Framework CTP4
***Last updated on: 22/11/2010***

## Why Entity Framework CTP4?
Microsoft ADO.NET Entity Framework 4.0 was released as part of .NET 4.0 on 12 April 2010. The CTP4 version is the latest preview version of Entity Framework 4.0. It is an early preview of the Code First Programming Model and Productivity Improvements for Entity Framework 4.

## Installing EFCTP4
There are 2 main ways of including EFCTP4:

* Download and install [Microsoft ADO.NET Entity Framework Feature Community Technology Preview 4 ](http://www.microsoft.com/downloads/en/details.aspx?FamilyID=4e094902-aeff-4ee2-a12d-5881d4b0dd3e). After the installation is complete add a reference to Microsoft.Data.Entity.CTP.
* If you have [NuGet](http://nuget.codeplex.com/) installed you can download and add the required resources using it. NuGet has two ways for adding packages:
 * **Console**: Open you Pacakge Manager Console (View -> Other Windows -> Pacakge Manager Console) and type in 'Install-Package EFCTP4'.
 * **GUI**: You can also do this by right clicking the References folder in Solution Explorer of your project and selecting *Add Library Package Reference...*. In the popup windows choose Online and enter EFCTP4 in the search field. Select EFCTP4 and click the Install button.

## Requirements
Entity Framework CTP4 does require some version of SQL Server. The simplest one to get is the SQL Server Compact Edition (SQLCE). It can be installed via [NuGet](http://nuget.codeplex.com/). The package for this is called *SQLCE.EntityFramework*. The installation goes the same as the installation of EFCTP4 with NuGet described in the previous section.

EFCTP4 also works great with [Microsoft SQL Server 2008 R2 Express Edition](http://www.microsoft.com/sqlserver/2008/en/us/express.aspx). It comes with Microsoft SQL Server Management Studio which is a really great tool for browsing around you database. This is also the only version of SQL Server I've tested this with. If you have any other version of SQL Server on your machine please let me know how it works at [MvcOpenID's CodePlex discussion board](http://mvcopenid.codeplex.com/discussions).

## Replacing the Model
Now you have to replace the ADO.NET Entity Data Model with POCO (Plain Old CLR Object) classes and the context class files. These files are already included in the source, but are not part of the project. Here is the list of the needed files:

* OpenId.cs
* User.cs
* UserContext.cs

To see these files click the *Show All Files* icon at the top of the Solution Explorer and then navigate to the Models folder. You will see these three files but they won't have the nice icons. Now select them (use Ctrl + Left Mouse Click for multi-select), left click on them and from the menu select *Include In Project*.

The build would fail at this point because there are duplicate definitions of *OpenId*, *User* and *UserContext* classes. To fix this we'll have to remove UserDB.edmx from the project. This is done in a similar fashion as we did with the POCO files. Left click on UserDB.edmx and select *Exclude From Project*.

## Modifying the UserRepository
The build still failes at this point. The reason for this is that EF4.0 and EFCTP4 do not use the same base class for UserContext. As UserRepository is using the UserContext the build fails there. So you'll have to modify it a bit.

Open the UserRepository.cs file. In the *AddOpenId* function you'll see two lines of code just before the end. One is commented out and the other isn't. They both have a comment at the end to let you know to which EF version they apply to. Comment out the EF4 line and decomment the EFCTP4 line. You see a similar situation in the *RemoveOpenId* and *RemoveUser* functions. Do the same there.

## Optional Steps

### Setting the Database Initializer
If you never ran any of the MvcOpenID projects using EFCTP4 this (probably) won't affect you. The problem will occur when you change the model. This will break the project at runtime and throw the following exception:

`The model backing the 'UserContext' context has changed since the database was created.  Either manually delete/update the database, or call Database.SetInitializer with an IDatabaseInitializer instance.  For example, the RecreateDatabaseIfModelChanges strategy will automatically delete and recreate the database, and optionally seed it with new data.`

To fix this we must allow EFCTP4 to rebuild out database model. You'll achieve this by opening the Global.asax.cs file located at the root of the project. At the top you'll see a commented out `using` statement. Uncomment that. A bit lower, at the top of the `Application_Start()` function you see another line of code commented out:

`//Database.SetInitializer<UserContext>(new RecreateDatabaseIfModelChanges<UserContext>());`

Uncomment this and run the application again. The model in the database will be updated now. Note that some people say that it's best to run the application in debug mode after you make any data model changes. But you will loose all the data that was stored in there. So make sure you comment this out again when you finalize your data model. Entity Framework CTP5 should bring quite a lot more functionality to this part. I'll do an update as soon as it's out.

### Remove MvcOpenID.mdf
The database inside App_Data is no longer needed. So you can remove it without any worries. You can also just excluded from the project as we did with the entity data model.

### Deleting the UserContext Connection String
There is a connection string inside web.config that is setup by the Entity Framework for the model. This is no longer needed when using EFCTP4 as you can remove it by going to Web.config file at deleting the following connection string:

`<add name="UserContext" connectionString="metadata=res://*/Models.UserDB.csdl|res://*/Models.UserDB.ssdl|res://*/Models.UserDB.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\MvcOpenID.mdf;Integrated Security=True;User Instance=True;MultipleActiveResultSets=True&quot;" providerName="System.Data.EntityClient" />`

You can of course just comment it out using XML comment tags `<!-- Commented text -->`.

## The End
You can now run your application. Everything should work fine. If not please see the next section on how to report the problem.

## Troubleshooting
If anything goes wrong or if it doesn't work even after you've done all the steps here please report this at [MvcOpenID CodePlex Issue Tracker](http://mvcopenid.codeplex.com/workitem/list/basic).