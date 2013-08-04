FeatureWise
============

*Innovation Accounting for your .NET Apps*

FeatureWise can deliver the data to help answer the following questions:
- What features are your users using the most?
- Are there any features that your users do not use at all?
- In which features do users spend most of their time?
- Are there usage patterns that will help us allocate resources?
- What is the most important thing that we should work on next?
 
Whether you're managing a large enterprise application or just have the seeds of a startup lean thinking is driving
more and more decisions.  Plain and simple the projects who act on the best data will emerge as champions of markets.  

The good news is that data is fairly trivial to collect.

The bad news is that it is difficult to find the time to write the data collection code and the dashboard with all your
regular demands.

The worst news is that vendors on the market can be prohibitively expensive for startups or projects with constrained 
budgets.

FeatureWise was built to target some specific needs for software innovation accounting.  Currently FeatureWise provides 
some of the more fundamental metrics such as usage counts and durations.  It is not a robust tool like you'd find with a big vendor but hey - it's free!

Getting Started
===============
FeatureWise is an ASP.NET MVC4 application (with a dash of WebAPI).  So you can host it right in IIS, or on Azure.  To get started you're going to need Visual Studio with .NET 4.5 and SQLServer.

There is also a client library which you can embed in your application.  The client provides a simple API for you to send 
usage events off to the server.  Configuration of the client is fairly simple, just define the hostname of your server
in the FeatureWise.Hostname configuration property of your App.Config.  There is a singleton called FeatureWise that 
you will use to track usage events in your application.  For example:

`FeatureWise.Tick("feature1", DateTime.UtcNow)`

`FeatureWise.Start("feature2", DateTime.UtcNow)`

`FeatureWise.Stop("feature2", DateTime.UtcNow)`


Tracking Usage
===============
The simplest way to track usage is with Ticks.  Any time a user triggers a particular feature you can send a Tick event
off.  This will show up in any of the usage charts.

The other way to track usage is through Elapsed Time.  To use elapsed time you can send a Start event when the user
triggers a feature and a Stop when the feature is done.  This can help you identify tasks that might be taking longer
than others.

Generating Reports
==================
To generate a report simply click the "Generate" button on the report page.  Currently reports are generated within the web request.  I suspect we will quickly outgrow this and have to move to some kind of ETL process.

Development
===========
- Create SQLServer database called 'FeatureWiseDev'
- Fetch NuGet packages
- Create database using EntityFramework's migration tool
- Hack. Send a pull request.  Tests are highly encouraged.

Help!
=====
- I'm not a UI/UX guy and could really use some help in making this thing sexy.  Please lend a helping hand if you have such skills.
- .NET is painful.  Any sweetness to the build process would be appreciated.  For example, punching out properties from web.config so we don't have to hardcode db names.  Or MsBuild targets to run all tests.
- Advice on additional data to collect, reports, etc.
