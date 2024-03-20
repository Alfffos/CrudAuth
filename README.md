• THE PROYECT:

This repository is the minimum base needed to integrate JWT Authentication to a project where it is required to have registered users, at the same time it integrates the Administrator Role with extra functionalities.

The purpose of creating this repository is to use it when creating projects that require authentication, thus saving development time in the creation of the main endpoints for the
user management and tool configurations.

An important clarification: i am not going to stop to comment the code where i handle the user logic, my main goal is to prepare all configurations to be able to use the Authentication, so only comments where it is necessary to clarify these code lines will be seen.

Most of the explanations for the configurations are commented in the code of the project.




• DOCS:

In the first instance the API has 4 user endpoints and 1 Authentication endpoint, a simple and basics in order to start building our web application. Let's take a more in depth look at the endpoints.


- CreateUser
This endpoint is public, so anyone can access it, its verb is Put, sends an entity UserCreate and stores it in the database.

- GetAllUser
This endpoint requires Authentication of user, its verb is Get y returns a list of all users that are stored in the database.

- DeleteUser
This endpoint requires Administrator Authorization, which means that only the administrator can access it, its verb is Delete, en via an id and deletes the user with the id that was provided.

- GetByIdUser
This endpoint requires Authentication of user, its verb is Get, it sends the user id and returns a user with the provided id.

- Authenticate
This endpoint is public, anyone can access it. A AuthenticationRequesBody must be sent where in this entity are the UserName and Password properties. This endpoint is responsible for using the logica to search the database for credentials and validate them, addresses to use the logic for the generation of the token.




• ADMIN ROLE:

In order to make use of the protected endpoints with the Administrator Role it is required to be authenticated with it, here are your credentials.

UserName = Adm1n
Password = P@ss0rd
Name = Admin
LastName = Admin




• MORE DATA:

In this case I use Swagger to access the endpoints that are created, if you want to use another tool you will have to manipulate the settings in order to implement.

To clarify that in this project the CORS settings are by default, which means that all endpoints are protected.









