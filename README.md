# Quizzer

## Description
This simple web application is a demo multiple choice quiz taking application. It displays each question, one at a time, together with the corresponding answer options.
This is a fullstack app. The questions and corresponding answers are stored in a SQL database, 
Reasoning behind your technical choices, including architectural

## Potential improvements
Trade-offs you might have made, anything you left out, or what you might do differently if you were to spend additional time on the project

Currently, the quiz only shows the user's score at the end. An improvement would be to additionally show the user a list of which questions they got wrong and right.
Since there is no concept of a database user entity in this project, the quiz taker's answer choices are saved in the same table as the questions and answers themselves. If the concept of a user were added, another table could be created which woudl reference the question and answer tables, and here instead the user's quiz data could be kept.

## Local installation instructions
Clone the repository from https://github.com/kellanloew/QuizApp.git
If you do not already have it installed, download the DotNet SQK at https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.301-windows-x64-installer
If you do not already have it installed, download SQL Server 2019 Developer Edition from https://www.microsoft.com/en-us/sql-server/sql-server-downloads. The SQL Server Management Studio may also be helpful.
Create or confirm the credentials for a new SQL user.
Open QuizApplication.Web\Models\quiz_dbContext.cs and go to line 25. Using the credentials above, replace the dummy values in the connection string with those of your SQL server instance.
In a command line of your choice, cd to the root directory of the QuizApplication repo.
Run setup.ps1, giving it as parameters your database's server, user, and password
Run run.ps1
Open a browser and navigate to http://localhost:5000. Your app will be running!

## URL for running application
Link to running application: quizzer.somee.com
