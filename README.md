Hi,
In the 01-Presentation/TaskShare.API/Properties/launchSettings.json, in profiles, you have to change the applicationurl to the application work without any issues.
In the 01-Presentation/TaskShare.API/appsettings.json, you have to change the Data Source.
When you execute the program, you have to login with jwt bearer and for that, in login, put the username "user" and password "user123" and copy the token. In Authorize you put "Bearer and your token" and its done. To change the username and password to login, go to 01-Presentation/TaskShare.API/Controllers/AuthenticationController and change the line "if (loginRequest.UserName == "user" && loginRequest.Password == "user123")".
Enjoy :D
