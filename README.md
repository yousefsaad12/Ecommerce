

# E-Commerce API

Welcome to the E-Commerce API project! This API is built using ASP.NET and is designed to handle various e-commerce functionalities such as user authentication, product management, payment processing, and more.

## Features

- **ASP.NET API**: Robust and scalable backend development.
- **SQL & Entity Framework**: Efficient data management.
- **Identity**: Secure user authorization.
- **Pagination, Sorting, and Filters**: Enhance data retrieval.
- **Stripe Integration**: Seamless payment processing.
- **Redis**: Efficient caching.
- **Clean Architecture**: Maintainable code structure.
- **JWT Token & Refresh Token**: Secure authentication.
- **Dependency Injection**: Manage dependencies.
- **Repository Pattern**: Data access logic.

## Technologies Used

- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **Redis**
- **Stripe API**
- **JWT Tokens**
- **Clean Architecture**
- **Dependency Injection**
- **Repository Pattern**

## Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Redis](https://redis.io/download)
- [Stripe Account](https://stripe.com/)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yousefsaad12/Ecommerce.git
   cd Ecommerce
   ```

2. **Set up the database:**
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.
   - Run the following command to apply migrations:
     ```bash
     dotnet ef database update
     ```

3. **Set up Redis:**
   - Ensure Redis is installed and running on your machine.

4. **Set up Stripe:**
   - Add your Stripe API keys in `appsettings.json`.

5. **Run the application:**
   ```bash
   dotnet run
   ```

### Usage

- Use tools like [Postman](https://www.postman.com/) to interact with the API.
- Swagger UI is available at `/swagger` for API documentation and testing.

### API Endpoints

- **Authentication:**
  - `POST /api/auth/register` - Register a new user.
  - `POST /api/auth/login` - Login a user.
  - `POST /api/auth/refresh` - Refresh JWT token.

- **Products:**
  - `GET /api/products` - Get all products with pagination, sorting, and filters.
  - `GET /api/products/{id}` - Get a single product by ID.
  - `POST /api/products` - Create a new product.
  - `PUT /api/products/{id}` - Update a product by ID.
  - `DELETE /api/products/{id}` - Delete a product by ID.

- **Payments:**
  - `POST /api/payments/charge` - Process a payment using Stripe.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to the ASP.NET community for their continuous support and resources.
