# Quizzer

## Description
### Operation
This web application demonstrates what a simple online quiz taking app could look like. It displays each question, one at a time, together with the corresponding answer options. When the user submits an answer, their choice is saved and the next question loaded. After all questions have been answered, the user's percentage score is displayed.
### Technical Specifications
This is a fullstack ASP.NET Core 3.1 MVC web application. This architecture, which comes with the ASP.NET web apps, helps to distinguish different parts of the app: the data access layer, the user interface, and controllers that handle data requests. I also use View Models, which help in organizing data sent to and from the UI and controllers.  The front end, built with HTML, CSS, and Bootstrap, also uses jQuery for dynamic event handling; the backend is written in C#. For the database access layer, I chose Entity Framework Core 3.1. It offers easy access to database entities using C# objects, as well as keeping both the database and C# data objects synchronized. The project is currently configured to use SQL Server 2019 database.
Because ASP.NET Core is cross platform, this app can run on Windows or Linux.

## Potential improvements
Currently, the quiz only shows the user's score at the end. An improvement would be to additionally show the user a list of which questions they got wrong and right.
Further, since there is no concept of a database user entity in this project, the quiz taker's answer choices are saved in the same table as the question and answer tables themselves. If the concept of a user were added, another table could be created which would reference the question and answer tables, and here instead the user's quiz data could be saved.
The project is currently configured to use SQL Server, which means it cannot run on Mac OS. If SQL Server were replaced with PostgreSQL, the project would be completely cross-platform.

## Local installation instructions (for Windows)
1. Clone the repository from https://github.com/kellanloew/QuizApp.git
1. If you do not already have it installed, download the DotNet SQK at https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.301-windows-x64-installer
1. If you do not already have it installed, download SQL Server 2019 Developer Edition from https://www.microsoft.com/en-us/sql-server/sql-server-downloads. The SQL Server Management Studio may also be helpful.
1. Create credentials for a new SQL user for your database, or confirm your existing ones.
1. Open QuizApplication.Web\Models\quiz_dbContext.cs and go to line 25. Using the credentials above, replace the dummy values in the connection string with those of your SQL server instance.
1. In a command line of your choice, cd to the root directory of the QuizApplication repo.
1. Run database.ps1, giving it as parameters first your database's server, then user, and finally password. If you have Visual Studio and IIS installed, and wish to run the app from there, skip steps 9 & 10
1. Run build.ps1
1. Run run.ps1
1. Open a browser and navigate to http://localhost:5000. Your app will be running!

## URL for running application
Link to running application: quizzer.somee.com
