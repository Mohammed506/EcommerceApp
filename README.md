# EcommerceApp

## Overview

EcommerceApp is a comprehensive e-commerce solution designed to manage products, users, and transactions. This project is built using ASP.NET Core for the backend and MongoDB for data storage.

## Project Structure

- **EcommerceApp**: Main application code.
- **Ecommerce.DB**: Database configuration and models.
- **Ecommerce.Domain**: Business logic and domain models.
- **Ecommerce.Domain.Shared**: Shared domain models and interfaces.

## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) (for version control and repository management, optional but recommended)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (a recommended IDE to navigate through the code)  


## Docker Setup Instructions

1. Clone the repository:
    ```bash
    git clone https://github.com/Mohammed506/EcommerceApp.git
    cd EcommerceApp
    ```

2. Build the Docker images and start the containers using Docker Compose:
    ```bash
    docker-compose up --build
    ```

   The application should now be running on [http://localhost:8080/swagger](http://localhost:8080/swagger).



## Key Features

- User authentication and authorization
- Product management
- Order processing
- RESTful API endpoints

### Authentication

Purchase end point require authentication. You need to provide a JWT token to access these endpoints. In the Swagger UI, you can enter your JWT token by following these steps:

1. Click on the "Authorize" button in the Swagger UI.
2. Enter the token in the format `Bearer {your_token}`.
3. Click "Authorize" to apply the token to your requests.


## API Endpoints

### User Endpoints

- **Get All Users**
  - **GET** `/Users`
  - **Description**: Returns all users in the database.

- **Get User By ID**
  - **GET** `/Users/{id}`
  - **Description**: Returns a specific user by their ID.
  - **Path Parameter**: `id` (User ID)

- **Register User**
  - **POST** `/Users/register`
  - **Description**: Registers a new user.
  - **Request Body**: Registration details (username, password, etc.)
  - **Responses**:
    - **201 Created**: If registration is successful, returns the created user.
    - **409 Conflict**: If there is a conflict (e.g., user already exists).
    - **400 Bad Request**: If the request body is invalid or other errors occur.

- **Login User**
  - **POST** `/Users/login`
  - **Description**: Authenticates a user and returns a JWT token.
  - **Request Body**: Login details (username, password)
  - **Responses**:
    - **200 OK**: If authentication is successful, returns the JWT token.
    - **401 Unauthorized**: If authentication fails.
    - **400 Bad Request**: If the request body is invalid or other errors occur.

- **Update User**
  - **PUT** `/Users/{id}`
  - **Description**: Updates an existing user by their ID.
  - **Path Parameter**: `id` (User ID)
  - **Request Body**: Updated user details
  - **Responses**:
    - **204 No Content**: If the update is successful.
    - **404 Not Found**: If the user does not exist.
    - **400 Bad Request**: If the request body is invalid or other errors occur.

- **Delete User**
  - **DELETE** `/Users/{id}`
  - **Description**: Deletes a user by their ID.
  - **Path Parameter**: `id` (User ID)
  - **Responses**:
    - **204 No Content**: If the deletion is successful.
    - **404 Not Found**: If the user does not exist.
    - **400 Bad Request**: If other errors occur.

### Product Endpoints

- **Get All Products**
  - **GET** `/Products`
  - **Description**: Returns all products in the database.

- **Get Product By ID**
  - **GET** `/Products/{id}`
  - **Description**: Returns a specific product by its ID.
  - **Path Parameter**: `id` (Product ID)

- **Create Product**
  - **POST** `/Products`
  - **Description**: Creates a new product.
  - **Request Body**: Product details (name, price, etc.)

- **Update Product**
  - **PUT** `/Products/{id}`
  - **Description**: Updates an existing product by its ID.
  - **Path Parameter**: `id` (Product ID)
  - **Request Body**: Updated product details

- **Delete Product**
  - **DELETE** `/Products/{id}`
  - **Description**: Deletes a product by its ID.
  - **Path Parameter**: `id` (Product ID)

- **Purchase Product**
  - **POST** `/Products/{id}/purchase`
  - **Description**: Simulates the purchase of a single product by reducing its quantity. Requires authorization.
  - **Path Parameter**: `id` (Product ID)
  - **Returns**: 
    - **200 OK**: If the purchase is successful, returns a success message and the product ID.
    - **400 Bad Request**: If the product is out of stock or does not exist.


