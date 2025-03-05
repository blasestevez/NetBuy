# NetBuy

This was my first web project, created as part of a school assignment. During its development, I learned the fundamentals of web development using modern technologies and scalable architecture approaches.

---

## Features

This project offers the following features:

- **User Management**:
  - Secure registration using ASP.NET Identity.
  - Login and credential validation.
  - User classification as "seller" or "buyer".
- **Publication Management**:
  - Create, list, and view product listings.
  - Associate publications with a seller user.
  - Data validation to ensure consistency and quality.
- **Scalable Architecture**:
  - Implements patterns like Repository and DTOs.
  - Configured AutoMapper to simplify handling of models and DTOs.

---

## Technologies Used

This project was built using the following technologies:

- **ASP.NET Core 8**: For building the web application.
- **Entity Framework Core**: To manage database interactions using an ORM.
- **SQL Server**: For storing data persistently.
- **ASP.NET Identity**: To handle user authentication and authorization.
- **AutoMapper**: To map domain models to DTOs and vice versa.
- **C#**: As the primary programming language for the backend.
- **Git**: For version control and collaboration.

---

## Getting started

To get started with this project, clone the repository and set up your development environment. Follow the steps below to run the application locally.

### Prerequisites

Make sure you have the following installed on your machine:

- **.NET 8 SDK** or higher
- **SQL Server** for the database
- **Git** to clone the repository

### Installation

1. Clone the repository:
   ```shell
   git clone https://github.com/yourusername/NetBuy.git
   cd LaChozaComercial
   ```

2. Configure the database:
   - Open the `appsettings.json` file and adjust the connection string:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=NetBuyDB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

3. Apply database migrations:
   ```shell
   dotnet ef database update
   ```

4. Run the project:
   ```shell
   dotnet run
   ```

---


## Contributing

If you want to contribute to this project, follow the steps below:

1. Fork the repository:
   ```shell
   git fork https://github.com/blasestevez/NetBuy.git
   ```

2. Create a feature branch:
   ```shell
   git checkout -b feature/your-feature-name
   ```

3. Commit your changes:
   ```shell
   git commit -m "Add your detailed commit message here"
   ```

4. Push the branch:
   ```shell
   git push origin feature/your-feature-name
   ```

5. Open a pull request and describe your changes.
