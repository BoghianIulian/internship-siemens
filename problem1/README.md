# Coffee Shop Digitalization - Sarah's Cafe

This project represents the design phase for a coffee shop management system, focusing on scalability, clear object-oriented structure, and data integrity.

## 1. Class Diagram (Problem 1.1)

The class diagram below outlines the core entities of the system, their properties, methods, and the relationships between them.

```mermaid
classDiagram
    class CoffeeShop {
        +int id
        +String locationName
        +String address
    }
    class BeveragePriceCatalog {
        +int beverageTypeId
        +String beverageName
        +double priceSmall
        +double priceMedium
        +double priceLarge
    }

    class Beverage {
        +int id
        +int beverageTypeId
        +Size size
        +double getFinalPrice()
    }

    class Extra {
        +int id
        +String name
        +double price
    }

    class Customer {
        +int id
        +String name
        +int loyaltyPoints
        +CustomerType type
        +redeemPoints()
    }

    class Order {
        +int id
        +DateTime timestamp
        +double totalPrice
        +calculateTotal()
    }

    class Barista {
        +int id
        +int shopId
        +String name
    }

    class Size {
        <<enumeration>>
        SMALL
        MEDIUM
        LARGE
    }

    class CustomerType {
        <<enumeration>>
        REGULAR
        GOLD
    }


    CoffeeShop "1" *-- "*" Barista : employs
    CoffeeShop "1" *-- "*" Order : hosts
    Order "1" *-- "*" Beverage : contains
    Beverage "1" *-- "*" Extra : has extras

    
    Beverage "*" --> "1" BeveragePriceCatalog : references prices
    Order "*" --> "0..1" Customer : placed by (optional)
    Order "*" --> "1" Barista : prepared by
    
    
    Beverage --> Size
    Customer --> CustomerType
```

# Design Justification

The system design was carefully crafted to address key business requirements for a coffee shop chain, ensuring scalability, customization, and accountability across multiple locations.

## 1. Multi-Location Scalability (Chain Support)
- Introduced the **CoffeeShop** class to manage the business as a chain.
- Each **Barista** is assigned to a specific shop, and every **Order** is linked to a location, allowing for per-shop revenue tracking and inventory management.

## 2. Pricing Scalability
- Used the **BeveragePriceCatalog** to decouple the global menu from individual orders.
- This centralizes prices for all sizes (**Small, Medium, Large**) across the entire chain, allowing for menu updates without affecting historical order data.

## 3. Guest & Member Flexibility
- The relationship between `Order` and `Customer` is **optional (0..1)**.
- This allows the system to process "Guest" transactions for walk-in customers while still providing full loyalty features for registered **Regular** or **Gold** members.

## 4. Customization & Instance Logic
- Implemented **composition** between `Beverage` and `Extra`. 
- While the catalog stores the "recipe," the `Beverage` class represents the **physical cup** prepared for a customer. If an order is cancelled, the specific cup and its added extras are removed, while the catalog remains intact.

## 5. Loyalty System
- Integrated `CustomerType` to manage dynamic point earning:
  - **Regular members:** 1 point per euro spent.
  - **Gold members:** 2 points per euro spent.
- The `redeemPoints()` method facilitates the "free drink" reward logic as per business requirements.

## 6. Accountability
- Every `Order` tracks the specific **Barista** and the **CoffeeShop** location.
- Automatic **timestamping** and total price calculation ensure business transparency and staff performance monitoring.

# Database Diagram (Problem 1.2)

The following **Entity Relationship Diagram (ERD)** represents the data persistence layer for the class model defined in section 1.1.  

It translates the object-oriented relationships—such as the composition between **Orders** and **Beverages**—into a **relational database schema**, ensuring that all business rules, including **multi-location tracking** and **optional customer membership**, are enforced at the data level.

```mermaid
erDiagram
    COFFEE_SHOPS ||--o{ BARISTAS : "employs"
    COFFEE_SHOPS ||--o{ ORDERS : "hosts"
    BARISTAS ||--o{ ORDERS : "prepares"
    CUSTOMERS |o--o{ ORDERS : "places (optional)"
    ORDERS ||--|{ ORDERED_BEVERAGES : "contains"
    BEVERAGE_CATALOG ||--o{ ORDERED_BEVERAGES : "defines"
    ORDERED_BEVERAGES ||--o{ BEVERAGE_EXTRAS : "customized with"
    EXTRAS ||--o{ BEVERAGE_EXTRAS : "is used in"

    COFFEE_SHOPS {
        int id PK
        string location_name
        string address
    }

    BARISTAS {
        int id PK
        int shop_id FK "References COFFEE_SHOPS"
        string name
    }

    CUSTOMERS {
        int id PK
        string name
        int loyalty_points
        string member_type
    }

    ORDERS {
        int id PK
        int shop_id FK "References COFFEE_SHOPS"
        int barista_id FK "References BARISTAS"
        int customer_id FK "Nullable for Guest users"
        datetime timestamp
        double total_price
    }

    BEVERAGE_CATALOG {
        int id PK
        string name
        double price_small
        double price_medium
        double price_large
    }

    ORDERED_BEVERAGES {
        int id PK
        int order_id FK "Part of Order composition"
        int beverage_type_id FK "References Catalog"
        string size
    }

    EXTRAS {
        int id PK
        string name
        double price
    }

    BEVERAGE_EXTRAS {
        int ordered_beverage_id FK "References specific drink"
        int extra_id FK "References Extra type"
    }
```  

# Database Logic & Key Constraints

## One-to-Many (1:N)
A **CoffeeShop** hosts many **Orders**, and a **Barista** prepares many **Orders**.  
This is enforced via **Foreign Keys** (`shop_id`, `barista_id`) in the `ORDERS` table to ensure full accountability.

## Many-to-Many (M:N) Resolution
To allow a single drink to have multiple extras (e.g., vanilla syrup and an extra shot), we introduced the **BEVERAGE_EXTRAS** junction table.  
This links `ORDERED_BEVERAGES` and `EXTRAS` without data redundancy.

## Nullability for Guests
The `customer_id` in the `ORDERS` table is **nullable**.  
This matches the business requirement that membership is optional.

## Data Normalization
By storing **sizes** and **prices** in the `BEVERAGE_CATALOG` and referencing them in `ORDERED_BEVERAGES`,  
we protect historical sales data from future menu price fluctuations.