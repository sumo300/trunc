# Trunc #

Trunc is a URL shortening application written using C# and MVC 5.  Data is stored in a SQLite database using the [Biggy](https://github.com/xivsolutions/biggy) library.  It is intended to be as lightweight as possible without the requirements for a big database or lot's of infrastructure.  What this app is **not** is a robust, enterprise-level application for public URL shortening.  It has concurrency limits, but is great for larger businesses that have internal link shortening needs with relatively low utilization.

## Features ##

* Randomly generated or user-chosen shortened URLs
* Three (3) types of expiry (Never, By Created date, and By Last Accessed date)
* Partial localization (App name and tagline for now)
* Browse and Search - Can't remember a shortened URL or lost it?  Browse or Search for it!

## Setup ##

Out of the box, there is ZERO configuration.  For local debugging purposes, ensure you have IIS Express installed and hit F5 in Visual Studio 2013.  For runtime deployment, use the Visual Studio Publish feature and WebDeploy it to an IIS server or, if you have an Azure account, deploy directly to Azure!  VS 2013's publish features can work directly with Azure, provisioning at the time of publish.  Re-publish with ease.

## OSS Shout-Outs ##
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [Biggy](https://github.com/xivsolutions/biggy)
* [Bootstrap](http://getbootstrap.com/)
* [ELMAH](https://code.google.com/p/elmah/)
* [Ninject](https://github.com/ninject/ninject)
* [SQLite](http://www.sqlite.org/)
* [WebActivator](https://github.com/davidebbo/WebActivator)

## Want to help? ##

* If you've found a bug, please log it in the [Issues](https://bitbucket.org/Sumo/trunc/issues?status=new&status=open) list.
* If you want to fork and fix, please fork then open a branch on your fork specifically for this issue.  Give it a sensible name.
* Make the fix and then in your final commit message, use BitBucket's magic syntax (Closes/Fixes #X) so we can tie your pull request to you and your issue.
* Please verify your bug/issue with a test.  We use NUnit and it's simple to get going.