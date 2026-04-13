# POS System – Project

## Overview

This project is a **Point of Sale (POS) system** developed to manage retail sales in real time. It provides a complete workflow from product selection to payment processing, invoice generation, and inventory updates.

The system is designed with a **clean architecture approach** (Service + Repository pattern), ensuring maintainability, scalability, and clear separation of concerns.

---

## Technologies Used

* **C# (.NET WinForms)** – Desktop user interface
* **PostgreSQL** – Relational database
* **Npgsql** – PostgreSQL driver for .NET
* **ADO.NET** – Database communication and transactions
* **Custom Helpers & Infrastructure Components** – For better usability and security

---

## Project Structure

### Models/

Contains classes representing database entities:

* `ProductModel`
* `SaleModel`
* `SaleDetailModel`
* `PaymentExecutionModel`
* `GiftCardModel`

---

### Repositories/

Handles direct database access (CRUD operations):

* ProductRepository
* SaleRepository
* PaymentRepository
* GiftCardRepository

---

### Services/

Contains business logic and core application workflows:

* ProductService (stock validation, product retrieval)
* SaleService (saving sales and updating stock)
* PaymentService (payment handling and currency conversion)
* GiftCardService (issuing and managing gift cards)

---

### Views/ (Forms)

User interface layer (WinForms):

* `FrmPOS` – Main POS screen
* `FrmPagesa` – Payment processing
* `FrmFaturimi` – Invoice generation
* `FrmGiftCard` – Gift card lookup and transactions
* Other management forms (products, users, reports)

---

### Infrastructure/

Core system utilities and shared state:

* **Database connection (Npgsql)**
* **Session management** (logged-in user, role, shift)
* Global configurations

---

### Helpers/

Reusable utility classes:

* **AutoClosingMessageBox** – Displays temporary messages that close automatically
* **PasswordHelper** – Handles password hashing and validation for secure authentication
* Other helper utilities for formatting and validation

---

## Database Structure (Examples)

The system uses PostgreSQL tables such as:

* **Artikujt (Products)** – product details and stock
* **Shitjet (Sales)** – sale header (invoice)
* **ShitjetDetale (Sale Details)** – individual items per sale
* **EkzekutimiPageses (Payments)** – payment transactions
* **MenyratPageses (Payment Methods)** – available payment types
* **GiftCards** – issued gift cards
* **GiftCardTransactions** – usage and balance history

---

## Core POS Functionalities

### Sales Workflow

1. Product is scanned or selected
2. Added to cart with quantity and price
3. System validates stock availability
4. Discounts can be applied (per item or invoice)
5. Total is calculated automatically

---

### Payment Processing

* Supports **single and split payments**
* Supports **multiple currencies**
* Automatic **currency conversion**
* Handles **cash and non-cash payments**
* Calculates **change (kusuri)** for cash transactions
* Validates sufficient cash in register before returning change

---

### Invoice Generation

* Generates unique invoice number
* Saves:

  * Sale header
  * Sale details
  * Payment records
* Displays and prints invoice
* Includes VAT (TVSH) calculation

---

### Gift Card System

* Gift cards are treated as **products**
* When purchased:

  1. Sale is saved
  2. `ShitjaId` is generated
  3. Gift card is created automatically
  4. Linked via `ShitjaIdIssued`
* Supports:

  * Balance tracking
  * Transaction history
  * Search by code

---

### Inventory Management

* Real-time stock updates after each sale
* Prevents selling beyond available stock
* Notifications for low stock

---

### Returns (Refunds)

* Allows returning items from previous sales
* Updates stock accordingly
* Handles refund payments

---

### Notifications System

* Low stock alerts
* Invalid operations (e.g., empty cart)
* Important system events
* Displayed via notification form

---

### Security & User Management

* User authentication system
* Password hashing using `PasswordHelper`
* Role-based access (Admin / Cashier)
* Session-based authorization

---

## How to Run the Project

1. Configure the PostgreSQL connection string
2. Ensure database tables are created
3. Open the project in **Visual Studio**
4. Build and run the application (`F5`)

---

