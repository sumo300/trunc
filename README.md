# Trunc #

Trunc is a URL shortening application written using C# and MVC 5, targeting .NET 4.5.1.  Data is stored in a SQLite database using the [Biggy](https://github.com/xivsolutions/biggy) library.  It is intended to be as lightweight as possible without the requirements for a big database or lot's of infrastructure.  What this app is **not** is a robust, enterprise-level application for public URL shortening.  It has concurrency limits, but is great for larger businesses that have internal link shortening needs with relatively low utilization.

## Features ##

* Auto-generated or user-chosen shortened URLs
* Three (3) types of expiry (Never, By Created date, and By Last Accessed date)
* Localized - English and Spanish for now (translations welcome)
* Browse and Search - Can't remember a shortened URL or lost it?  Browse or Search for it!
* Hit Counts - Keeps track of every hit to a shortened URL

~~## Demo ##~~

~~A demo is hosted on Azure Web Sites.  Commits pushed here are automatically built and deployed to the demo site.  It typically only takes a few seconds for the new changes to appear.~~

~~http://trunc.azurewebsites.net/~~

## Setup ##

Out of the box, there is ZERO configuration.  For local debugging purposes, ensure you have IIS Express installed and hit F5 in Visual Studio 2013.  For runtime deployment, use the Visual Studio Publish feature and WebDeploy it to an IIS server or, if you have an Azure account, deploy directly to Azure!  VS 2013's publish features can work directly with Azure, provisioning at the time of publish.  Re-publish with ease.

Trunc uses a SQLite database located in the `~/App_Data/` folder.  Ensure the application pool user has read/write access to this folder.  If a database does not exist yet (data.db), one will be created.

By default, ELMAH logs locally to the `~/App_Data/ELMAH` folder as XML files.  Browsing the ELMAH page is as simple as navigating to 'http[s]://[YourURL]/elmah'.  It is highly recommended to read [ELMAH's documentation](https://code.google.com/p/elmah/) if you'd like to change the logging location or to secure this page.

## Roadmap ##

* Database migrations (if there is any need to change the data model)
* Upgrade to .NET Core to support cross-platform deployments

## OSS Shout-Outs ##
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [Biggy](https://github.com/xivsolutions/biggy)
* [Bootstrap](http://getbootstrap.com/)
* [ELMAH](https://github.com/elmah/Elmah)
* [Ninject](https://github.com/ninject/ninject)
* [NUnit](http://www.nunit.org/)
* [SQLite](http://www.sqlite.org/)
* [WebActivator](https://github.com/davidebbo/WebActivator)

## Want to help? ##

* If you've found a bug, please log it in the [Issues](https://github.com/sumo300/trunc/issues) list.
* If you want to fork and fix, please fork then open a branch on your fork specifically for this issue.  Give it a sensible name.
* Make the fix and then in your final commit message, use BitBucket's magic syntax (Closes/Fixes #X) so we can tie your pull request to you and your issue.
* Please verify your bug/issue with a test.  We use NUnit and it's simple to get going.
