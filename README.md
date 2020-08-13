Text-Em-All Back End Coding Challenge

.Net Core 3.1 Web API project to solve 4 Challenges presented in this repo
https://github.com/callemall/tea-c-sharp-challenge

The application will run at the URL
http://localhost:52332/CodingChallenge

SQL file for Challenge 3 can be found in the root folder of the git repo named Challenge3.sql

Set up and Model creation.
I set up a basic .NET Core Web API using Visual Studio's prebuild template with example REST API controllers to have a good start.
Then I set up Logging Services and structured my libraries with a mix of recommendations and how I feel is most comfortable/makes sense.
Since there was a Database already defined with a nice create/import script, I decided to use the EF Database First model and context creation.
This prevented any issues with talking and saving to the database and let me save lots of time.

I was able to complete all of the REST operations in the browser and using Postman, successfully checking error conditions.

I was able to just run the code by setting the WebApp as the startup project and running in Visual Studio.


Configuration
The DB connection string is configurable in the "\Text-Em-All Back End Coding Challenge\appsettings.json" file as the 
 "sqlconnection": {
    "connectionString": "server=localhost;user id=sa;password=Abcd1234;database=School;"
  }

Logging is by default saved at 
c:/Logs/CodingChallenge/Project/logs/internallog.txt AND
c:/Logs/CodingChallenge/Project/logs/${shortdate}_logfile.txt

This can be configured in C"\Text-Em-All Back End Coding Challenge\nlog.config"
