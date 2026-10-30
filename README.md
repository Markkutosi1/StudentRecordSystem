# Student Record Management System

## Project Overview

The Student Record Management System is a C# console application developed to manage student information using an SQLite database.

The system allows users to register, view, search, update, and delete student records. It stores information in a database so that records remain available even after the application is closed.

## Technologies Used

- **Programming Language:** C#
- **Framework:** .NET
- **Database:** SQLite
- **Database Library:** Microsoft.Data.Sqlite
- **Development Environment:** Visual Studio Code

## Main Features

1. Register new students.
2. View all registered students.
3. Search for a student using their student number.
4. Update student names and courses.
5. Delete student records with confirmation.
6. Prevent duplicate student numbers.
7. Store records permanently in an SQLite database.

## Database Structure

The application uses a `Students` table with the following fields:

| Field | Description |
|---|---|
| Id | Automatically generated student ID |
| StudentNumber | Unique student number |
| Name | Student's full name |
| Course | Student's course |

## How to Run the Project

1. Install the .NET SDK.
2. Open the project folder in Visual Studio Code.
3. Open the terminal.
4. Run:

   `dotnet restore`

5. Start the application:

   `dotnet run`

The application automatically creates the SQLite database and the Students table when it starts.

## Learning Outcomes

Through this project, I gained practical experience in:

- C# programming and console application development.
- Database creation and management using SQLite.
- SQL operations: INSERT, SELECT, UPDATE, and DELETE.
- Input validation and duplicate record prevention.
- Connecting a C# application to a relational database.

## Project Status

Completed and tested. All main database operations have been tested successfully. 