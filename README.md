# Simple Web App using MVC Architecture with ASP.NET Core

## Overview
Welcome to the repository for our Simple Web App using ASP.NET Core MVC. This project demonstrates a robust web application designed to manage user profiles with different functionalities for Admins and Clients.

## Features
- **User Management**:
  - **Admin**:
    - **Create Client Profiles**: Add new clients with fields including First Name, Last Name, Phone Number, Email Address, Address, Notes, and Birthday.
    - **Edit Client Profiles**: Update existing client profiles with modified information.
    - **Delete Client Profiles**: Mark client profiles as inactive (soft delete).
    - **Admin Account Management**: Create, edit, or delete other admin logins.
  - **Client**:
    - **View Profile**: Clients can view their own profile information.

- **Data Persistence**:
  - Records are not permanently deleted but are marked as inactive to maintain data integrity.

- **Input Validation**:
  - Ensures that all required fields are filled and that data conforms to specified formats (e.g., numeric characters for phone numbers, date formats for birthdays).

## Setup
Ensure you have the following prerequisites installed:

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
