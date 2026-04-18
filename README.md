# Task Board Application

A mini To Do Task Board Management application

Live Demo : https://jam.dev/c/91ba64fd-560c-4171-8a2d-01ccf34c5904

## Tech Stack

* **.NET 9 Web API**
* **Angular 19**
* **Entity Framework Core**
* **SQL Server**

This project demonstrates clean architecture, proper api design, and frontend backend integration.

---

# Features

### Backend (.NET Core)

* Clean Architecture Layers: Strict separation between Controller, Application, Domain, and Infrastructure.
* CQRS Pattern: Uses MediatR for clean command/query separation.
* Domain-Driven Design: Task transitions (MarkComplete, ReopenTask) are encapsulated within the Domain entities.
* Global Exception Middleware: Structured JSON error responses for all API failures.
* EF core implementation
* Status-based Workflow:Managed lifecycle (Todo -> In Progress -> Completed).

* Task lifecycle:

  * Create Task
  * Update Task
  * Delete Task
  * Change Status
  * Reopen Task

---

### Frontend (Angular)

* Columns:

  * Todo
  * In Progress
  * Completed
    
* Create / Edit / Delete tasks
* Change task status with actions
* Reopen completed tasks
* API integration with HttpClient
* Basic user feedback (toast/messages)

---


#  Setup 

## Backend Setup

1. Navigate to backend:

cd backend

2. Update database connection  in `appsettings.json`

3. Apply migrations:

dotnet ef database update --project TaskBoard.Infrastructure --startup-project TaskBoard.Api

4. Run the Backend:

dotnet run --project TaskBoard.Api

or

Run the application with http


Backend will run at:

http://localhost:5186

---

## Frontend Setup

1. Navigate to frontend:

cd frontend

2. Install dependencies:

npm install

3. Run the Frontend:

ng serve

Frontend will run at:

http://localhost:4200

---

#  API Endpoints

### Tasks

| Method | Endpoint                 | Description   |
| ------ | ------------------------ | ------------- |
| GET    | `/api/tasks`             | Get all tasks |
| POST   | `/api/tasks`             | Create task   |
| PUT    | `/api/tasks/{id}`        | Edit task     |
| PUT    | `/api/tasks/{id}/status` | Update status |
| POST   | `/api/tasks/{id}/reopen` | Reopen task   |
| DELETE | `/api/tasks/{id}`        | Delete task   |

---

#  Database Design

### Task Entity

* `Id` (GUID, Primary Key)
* `Title` (Required)
* `Description`
* `Status` (Enum)
* `IsCompleted`
* `CreatedAt`
* `UpdatedAt`

---

# Backend Design Implementation

* Used Clean Architecture to separate concerns
* Business logic handled in Application layer 
* Domain model contains behavior (e.g., `MarkComplete`, `ReopenTask`)
* Used DTOs to avoid exposing domain entities
* Status-driven workflow instead of boolean flags
* Exception handling via middleware

---


#  Future Improvements

* Add JWT Authentication
* Drag & Drop feature
* Unit & Integration tests

