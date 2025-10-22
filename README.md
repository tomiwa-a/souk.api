# Warehouse Management with ASP.NET Core, Using the DDD Architecture

Souk is a simple inventory management application built with ASP.NET Core API (backend) and jQuery/Bootstrap (frontend), following Domain-Driven Design (DDD) principles.

## Prerequisites

- .NET 8 SDK
- MySQL Server
- Entity Framework Core CLI (install via `dotnet tool install --global dotnet-ef`)

## Database Setup

1. Install and start MySQL Server.
2. Create a database named `Souk`.
3. Update the connection string in `Souk.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=Souk;User=root;Password=yourpassword;"
     }
   }
   ```
   Replace `yourpassword` with your MySQL root password.

## Running Migrations

To create and update the database schema:

1. Navigate to the `Souk.Api` directory:
   ```bash
   cd Souk.Api
   ```
2. Run the migrations:
   ```bash
   dotnet ef database update
   ```
   This will apply all pending migrations to the `Souk` database.

## Running the API

1. From the `Souk.Api` directory:
   ```bash
   dotnet run
   ```
2. The API will start on `http://localhost:5156`.

## Accessing the Frontend

Once the API is running, open `presentation/index.html` in a web browser. This will load the jQuery-based frontend, which communicates with the API.

## Features

- Manage suppliers, warehouses, products, and purchase orders.
- Dashboard with counts.
- Detail views with actions (e.g., adjust quantities, fulfil orders).

## Notes

- Ensure the API is running before using the frontend.
- The frontend uses plain HTML/JS and does not require a separate server.

## Author

**Amole Oluwatomiwa Joseph**  
Email: [tomiwamole@gmail.com](mailto:tomiwamole@gmail.com)  
CV: [View CV](https://docs.google.com/document/d/1TomOXhWMLlG3aWA5sJcHlpPU5Hcso5q1nqlFQeaO4NA/edit?usp=sharing)

//This project is submitted as an application for the Senior Backend Engineering role.
