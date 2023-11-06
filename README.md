# MoviesAPI Project

Welcome to the MoviesAPI project! This API project is designed to manage and provide information about movies, including features like authentication, registration, login, movie management, and genre management.

## Table of Contents
- [Project Overview](#project-overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Dependencies](#dependencies)

## Project Overview

The MoviesAPI project is a RESTful API designed to handle various aspects of movie management, including user registration, authentication, and movie and genre management. It incorporates essential features like AutoMapper, dependency injection, JWT token-based authentication, and multiple controllers for handling different aspects of the project.

## Features

1. **AutoMapper:** AutoMapper is used to simplify object-to-object mapping and data transformation within the project. This streamlines the handling of data between different layers of the application.

2. **Dependency Injection:** The project implements dependency injection to manage and resolve dependencies, promoting a more modular and maintainable codebase.

3. **JWT Token Authentication:** JSON Web Tokens (JWT) are used for secure user authentication and authorization. This ensures that only authenticated users can access certain API endpoints and perform authorized actions.

4. **Account Controller:** The Account Controller is responsible for user registration and login functionalities, allowing users to create accounts and obtain JWT tokens for authenticated access.

5. **Movies Controller:** The Movies Controller handles the management of movies. Users can add new movies and retrieve information about existing movies.

6. **Genre Controller:** The Genre Controller is dedicated to managing movie genres, providing endpoints for creating, retrieving, and updating genre information.

## Getting Started

To get started with the MoviesAPI project, follow these steps:

1. Clone or download the project repository to your local machine.

2. Open the project in your preferred development environment.

3. Configure the project settings, including database connection strings, JWT token settings, and other environment-specific configurations.

4. Run the project and access the API endpoints to register users, authenticate, add movies, and manage genres.

## API Endpoints

The project offers the following API endpoints for various functionalities:

- `/api/account/register`: User registration endpoint.
- `/api/account/login`: User login endpoint for obtaining JWT tokens.
- `/api/movies`: Endpoints for managing movies, including adding and retrieving movies.
- `/api/genre`: Endpoints for managing movie genres, including creating, retrieving, and updating genres.

Detailed API documentation and usage examples can be found in the project's API documentation or by accessing the API endpoints directly through a tool like Postman or Swagger.

## Dependencies

The project relies on several dependencies and libraries to function. Some of the key dependencies include:
- AutoMapper
- Entity Framework
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity
- Microsoft.Extensions.DependencyInjection

Make sure to install and configure these dependencies to ensure the project runs smoothly.
