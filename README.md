# Authenticate Asp .Net Core Web Application through Json Web Token (Jwt)

## How to

* Clone or download this project.
* Before run update the database with letest migration included inside the project.
* run this project on your local machine.
* Register a new user from here ```http:localhost:{port}/Identity/Account/```.
* Logout yourself from the application.
* Try to access ```http://localhost:{port}/api/access/``` from a get request with content-type support of Json. Instead of responseing data this redirect you to login page for authentication bearer.
* Go ```http://localhost:49966/api/IssueToken``` and send a post request it with content-type support of Json. On the request body, add raw Json with credentials as

```json
{
    "UserName": "{your username}",
    "Password": "{your password}"
}
```
* wait and collect your token from the response body.
* now try again to access ```http://localhost:{port}/api/access/``` by adding authorization in header. the value of authorization should be ```Bearer {your token}```
* Just send the request and now you have access on the index page of ```/api/access/```. Check your response body. you find json data as: 

```json
{"JSON":["Access","From","Authenticated"]}
```

* If you haven't any experience of inspecting browser to handle requests, please use postman.
* To customize the application inside, go appsettings.json and edit the configurations for issuer, audience and key.
