## 📋 Overview

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

---




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

- **✅ Fluent Validation:**  
  Ensures data integrity by rigorously validating inputs, resulting in user-friendly error messages.

- **🔑 Account Management:**  
  Offers robust features for user account control, including functionalities for password changes and resets.
  
- **🗃️ Data Seeding:**  
Automatically seeds essential data, including admin roles and users, to ensure the system starts with pre-configured data, simplifying the setup and initial use.

## 🔧 **Setup Instructions**

### **Prerequisites**

- **.NET 8.0 SDK**
- **SQL Server**
- **Visual Studio 2022** (or **VS Code**)

### **Installation**

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MohammedAwadEleash/AspDotNetMVCBookifyApplication
   ```
2. **Navigate to the project folder**:
   ```bash
   cd  Bookify

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
- **Serilog** is pre-configured to log data based on the settings in `appsettings.json`.
- **Hangfire Dashboard** is accessible at `/hangfire` (requires admin role).

---
