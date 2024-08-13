# Simple Web App using MVC architecture using ASP.NET Core

## Overview
This project is designed to manage two types of users: Admin and Client. The system allows Admins to manage Client profiles, including creating, editing, and deleting profiles, as well as managing other Admin logins. Clients can only view their profiles.

## Functional Requirements

### User Types
- **Admin**: 
  - Can perform the following tasks:
    - **Create a Client Profile** with the following fields:
      - **First Name**: Required, accepts alphanumeric and special characters.
      - **Last Name**: Required, accepts alphanumeric and special characters.
      - **Phone Number**: Required, accepts numeric characters only.
      - **Email Address**: Required, accepts alphanumeric and special characters.
      - **Address**: Optional, accepts alphanumeric and special characters.
      - **Notes**: Optional, accepts alphanumeric and special characters (multiline).
      - **Birthday**: Optional, accepts date only.
    - **Edit a Client's Profile**: Modify any of the fields listed above.
    - **Delete a Client's Profile**: Mark a client's profile as inactive (soft delete).
    - **Manage Admin Accounts**: Create, edit, or delete other admin logins.

- **Client**:
  - Can perform the following task:
    - **View Profile**: View their own profile information.

## Important Notes
1. **Data Persistence**:
   - No data is permanently deleted from the database. Deleted records are only marked as inactive (soft delete).

2. **Input Validation**:
   - Implement input validation to ensure that:
     - All required fields are filled out.
     - Data is validated according to the specified requirements (e.g., numeric characters for phone numbers, date format for birthdays, etc.).

 
 
