[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6062a5a6560c4229ba25061d495231b6)](https://app.codacy.com/gh/eskil4152/CSharpServer/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

# CSharpServer

REST API written in C#, with .NET ASP. Handles requests from ReactClient.

Connects to PostgreSQL database on Azure.

Fetches by firstname, lastname, fullname and id from database.
Can delete person from database with id
Can log in with username and encrypted password, and remains authenticated with JWT Tokens.
Can check authorization levels with JWT Tokens.

## Todo
- [x] Make repo, service, controller and entity
- [x] Make user entity and roles
      
- [x] Make functioning API
- [x] Add 'New Person' API
- [ ] Add 'Alter Person' API
- [x] Add 'Remove Person' API
- [ ] Make tests for APIs

- [x] Move database to Azure

- [x] Add login with username and password
- [ ] Add login with Google and/or Microsoft
- [ ] Make tests for login

- [x] Add authentication when users log in
- [x] Add authorization for various APIs
- [ ] Make tests for authentication and authorization

- [ ] Host application on Azure

- [ ] Add caching

- [ ] More functionality
