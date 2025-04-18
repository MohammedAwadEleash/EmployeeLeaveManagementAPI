
# 🧾 Leave Management System

## 📋 Business Description

The Leave Management System is designed to digitize and streamline how employee leaves are requested, tracked, and managed within an organization.

Employees can submit leave requests online, while administrators (Admins) are able to review, approve, or reject those requests based on availability and company policies.

---

## ✅ Core Functionalities

- **Submit Leave Request**  
  Employees can submit a leave request by providing:
  - Start Date
  - End Date
  - Reason for leave

- **View Leave Requests**  
  Admins can retrieve all leave requests with optional filters:
  - By status (`Pending`, `Approved`, `Rejected`)
  - By employee ID
  - By date range

- **Approve / Reject Requests**  
  Admins can take action on leave requests after reviewing each one.

---

## 📌 Business Rules

- Prevent overlapping leave requests for the same employee.
- Ensure end date is not earlier than the start date.
- Support pagination and filtering for efficient data handling.

---

## 👥 User Roles

- **Employee**  
  - Submit leave requests  
  - View the status of their requests

- **Admin**  
  - View all leave requests  
  - Approve or reject requests  
  - Filter and manage requests


- **Backend Framework:** ASP.NET Core 9.0
- **ORM:** Entity Framework Core
- **Database:** SQL Server
-  **LINQ:** Used for querying and manipulating data efficiently.
- ✅ **SOLID Principles:** some of SOLID Principles  such as the single responsibility principle , Dependency Inversion Principle

### Design Patterns & Architecture Pattern:

- ✅ **Service Layer Pattern**  
  The service layer encapsulates the core business logic, invoked by controllers (API) for performing operations. This separation improves maintainability, reusability, and testability, while allowing controllers to focus solely on handling HTTP requests and responses.

- ✅ **Dependency Injection (DI)**  
  DI is leveraged to manage system dependencies, promoting loose coupling between components, improving maintainability, and allowing easier extensibility of the system.
- **SOLID Principles:** some of SOLID Principles  such as the single responsibility principle , Dependency Inversion Principle.

- ✅ **Repository Pattern**  
  Abstracts the data access logic and provides a centralized place for CRUD operations, enhancing separation of concerns and testability of the data layer.
- ✅ **Unit of Work Pattern**  
  Coordinates the work of multiple repositories by ensuring a single transaction for multiple operations. This pattern ensures data consistency and simplifies commit/rollback logic.
- **Authentication & Authorization:** ASP.NET Core Identity

  

## 🚀 Key Highlights

- **🔒 User & Role Management:**  
  Utilizes JWT Token for robust authentication and authorization, ensuring smooth and secure access control.

- **⚠️  Exception Handling:**  
  Provides unified error management to handle exceptions gracefully, significantly enhancing the user experience.

- **🛠️ Structured Error Handling:**  
  Adopts the result pattern to deliver clear, actionable feedback for error management.

- **🔄 Mapping: Mapster :**  
  Employs efficient object mapping between models to improve data handling and reduce boilerplate code.
- **🧠 Fluent API Configuration:**  
  Uses Fluent API in Entity Framework Core to configure entity relationships, constraints, and behaviors directly in the code, ensuring database schema precision and flexibility.
  
- **✅ Fluent Validation:**  
  Ensures data integrity by rigorously validating inputs, resulting in user-friendly error messages.

- **🔑 Account Management:**  
  Offers robust features for user account control, including functionalities for password changes and resets.
  
- **🗃️ Data Seeding:**  
Automatically seeds essential data, including admin roles and users, to ensure the system starts with pre-configured data, simplifying the setup and initial use.

## 🔧 **Setup Instructions**

### **Prerequisites**

- **.NET 0.0 SDK**
- **SQL Server**
- **Visual Studio 2022** (or **VS Code**)

### **Installation**

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MohammedAwadEleash/EmployeeLeaveManagementAPI.git
   ```
2. **Navigate to the project folder**:
   ```bash
   cd  EmployeeLeaveManagementAPI

   ```
3. **Restore project dependencies**:
   ```bash
   dotnet restore
   ```
4. **Update the connection string** in `appsettings.json` to match your database settings.
5. **Apply migrations to create the database**:
   ```bash
   dotnet ef database update
   ```
6. **Run the application**:
   ```bash
   dotnet run
   ```

### **Configuration**

- All necessary configuration settings, such as connection strings and API keys, are stored in `appsettings.json`.

---
## 📖 API Documentation (Swagger UI):

![Swagger UI - Google Chrome 4_19_2025 12_26_35 AM](https://github.com/user-attachments/assets/cd2fd84d-b315-4bbe-bb2c-c214f3573ff7)

![Swagger UI - Google Chrome 4_19_2025 12_14_07 AM](https://github.com/user-attachments/assets/d3707edf-1ea4-4e1d-abb1-854a6de9fb21)

![Swagger UI - Google Chrome 4_19_2025 12_14_07 AM](https://github.com/user-attachments/assets/d3707edf-1ea4-4e1d-abb1-854a6de9fb21)


![Swagger UI - Google Chrome 4_19_2025 12_13_48 AM](https://github.com/user-attachments/assets/9e98613c-a4ae-4e34-8e8a-d3614ebaf5b5)

![Swagger UI - Google Chrome 4_19_2025 12_14_14 AM](https://github.com/user-attachments/assets/6355d108-1bb1-4825-a422-c9e1cbf2e030)

