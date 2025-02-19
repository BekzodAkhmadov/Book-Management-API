# Book Management API

## Overview

The Book Management API is a RESTful web service built with .NET 8 that allows users to manage a collection of books. The API supports CRUD operations (Create, Read, Update, Delete).The API also calculates the popularity score of a book based on the number of views and the age of the book.

## Features

- **CRUD Operations**: Create, read, update, and delete books.
- **Pagination**: Retrieve books with pagination support.
- **Soft Delete**: Soft delete books to keep them in the database but mark them as deleted.
- **Popularity Score**: Calculate the popularity score of a book based on views and age.

## Books

- **GET /api/books**: Retrieve a paginated list of books.
  - Query Parameters:
    - `pageNumber`: The page number (default: 1).
    - `pageSize`: The number of items per page (default: 10).

- **GET /api/books/{id}**: Retrieve the details of a specific book by its ID, including the popularity score.

- **POST /api/books**: Create a new book.
  - Request Body:
```csharp
{
  "title": "Book Title",
  "publicationYear": 2023,
  "authorName": "Author Name",
  "viewsCount": 0
}```

- **POST /api/books/bulk**: Create multiple books in bulk.
  - Request Body:
```csharp
[
  {
    "title": "Book Title 1",
    "publicationYear": 2023,
    "authorName": "Author Name 1",
    "viewsCount": 0
  },
  {
    "title": "Book Title 2",
    "publicationYear": 2022,
    "authorName": "Author Name 2",
    "viewsCount": 0
  }
]
```

- **PUT /api/books/{id}**: Update an existing book by its ID.
  - Request Body:
```csharp
{
  "id": 1,
  "title": "Updated Book Title",
  "publicationYear": 2023,
  "authorName": "Updated Author Name",
  "viewsCount": 10
}
```

- **DELETE /api/books/{id}**: Soft delete a specific book by its ID.

- **DELETE /api/books/bulk**: Soft delete multiple books in bulk.
  - Request Body:
```csharp
[1, 2, 3]
```

    