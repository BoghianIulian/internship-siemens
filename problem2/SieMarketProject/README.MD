# SieMarket - Internship Project 2026

## Overview
This project is a C# implementation of the **SieMarket** order management and analytics system. The solution handles order processing for an online electronics store, including a dynamic discount policy and sales data analysis.

## Project Structure
The solution follows a layered architecture to ensure clean, maintainable, and scalable code:

### Models
Contains the core entities of the system:

- **Customer**: Represents the client with a unique ID and name.
- **OrderItem**: Captures product details, quantity, and unit price at the time of purchase.
- **Order**: Manages a collection of items and includes the logic for the **10% discount** applied to orders exceeding €500.

### Data (Repository)
- **OrderRepository**: Acts as an in-memory database. It uses private lists to store Customers, Orders, and OrderItems, providing methods to add and retrieve data independently.

### Services
- **MarketService**: Contains the business logic for analytics. It processes raw data from the repository to generate insights like the top-spending customer and product popularity.

### Program.cs
The entry point where components are initialized, test data is populated, and analytics results are displayed in the console.

## Implementation Details

### 1. Advanced Data Modeling
Customers are separated from Orders using unique IDs. This reflects a real-world relational database structure, ensuring data integrity (e.g., if a customer's name changes, their order history remains linked via `CustomerId`).

### 2. LINQ for High-Performance Analytics
LINQ (Language Integrated Query) is used to handle data processing efficiently:

- **Top Spender**: Grouped by `CustomerId`, calculated total spent for each group (including discounts), and used `OrderByDescending()` to find the highest spender.
- **Popularity Ranking**: Flattened all items from all orders using `SelectMany()`, grouped by `ProductName`, and sorted by total quantity sold.

### 3. Financial Precision
All currency-related fields (`UnitPrice`, `TotalAmount`) use the `decimal` data type. This avoids rounding errors associated with `float` or `double` in financial applications.

## How to Run
1. Ensure you have the **.NET SDK** installed.
2. Navigate to the project folder in your terminal.
3. Run the following command:

```bash
dotnet run