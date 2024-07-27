# EcommerceApp

## Overview

EcommerceApp is a comprehensive e-commerce solution designed to manage products, users, and transactions. This project is built using ASP.NET Core for the backend and MongoDB for data storage.

## Project Structure

- **EcommerceApp**: Contains the main application code including controllers.
- **Ecommerce.DB**: Contains database configuration and models, including the `Data` folder.
- **Ecommerce.Domain**: Contains business logic, domain models, and services, including the `Services` folder.
- **Ecommerce.Domain.Shared**: Contains shared domain models, interfaces, and DTOs, organized into `Interfaces`, `Entities`, and `DTO` folders.

## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/download/win) (for windows users)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (a recommended IDE to navigate through the code)  


## Setup Instructions


1. **Open Git Bash**: Launch Git Bash on your computer.

2. **Clone the Repository**: Run the following command to clone the repository:
    ```bash
    git clone https://github.com/Mohammed506/EcommerceApp.git
    ```

3. **Navigate to the Project Directory**:
    ```bash
    cd EcommerceApp
    ```

4. **Start Docker Engine**: Ensure that Docker Engine is running on your machine(click on docker icon and wait for the docker engine to start it might take few minutes).

5. **Build and Start the Docker Containers**:
    ```bash
    docker-compose up --build
    ```

   after the build is finished(it might take few minutes) ,
   The application should now be running on [http://localhost:8080](http://localhost:8080).
   
   note: if you open this url you will find nothing since i put no end point for this , please go to swagger end point to find all the end points for the application

### Access Swagger UI

1. Open your web browser and navigate to [http://localhost:8080/swagger](http://localhost:8080/swagger).
2. You will see the Swagger UI, where you can explore and test the available API endpoints.


## Opening the Project in Visual Studio 2022

1. **Open Visual Studio 2022**.

2. **Open the Solution**:
   - Click on **File** in the top menu.
   - Select **Open** and then **Project/Solution...**.
   - Navigate to the cloned project folder `EcommerceApp`.
   - Select the `.sln` file located in the project folder and click **Open**.
   - Visual Studio 2022 will load the solution and all related projects.



## Key Features

- User authentication and authorization
- Product management
- Order processing
- RESTful API endpoints

### Authentication

Purchase end point require authentication. You need to provide a JWT token to access these endpoints. In the Swagger UI, you can enter your JWT token by following these steps:

1. **Obtain the Token**:
   - Use the **Login** endpoint to generate a JWT token.
   - **POST** `/Users/login`
   - **Request Body**: Provide your username and password.
   - **Response**: If successful, you will receive a JWT token in the response.

2. **Use the Token in Swagger**:
   - Open Swagger UI at [http://localhost:8080/swagger](http://localhost:8080/swagger).
   - Click on the **Authorize** button located at the top right of the Swagger UI.
   - Enter the JWT token in the format `Bearer {your_token}` . Make sure to copy the token **without quotation marks**.
   - Click **Authorize** to apply the token to your requests.
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


