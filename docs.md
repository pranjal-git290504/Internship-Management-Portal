# Internship Management System API Documentation

## Base URL
```
http://localhost:5056/api
```

## Authentication

### Login
- **Endpoint**: `/User/Login`
- **Method**: POST
- **Body**:
```json
{
    "username": "string",
    "password": "string"
}
```
- **Response**:
```json
{
    "token": "string",
    "user": {
        "id": "number",
        "username": "string",
        "email": "string",
        "role": "string"
    }
}
```

### Register
- **Endpoint**: `/User/Register`
- **Method**: POST
- **Body**:
```json
{
    "firstName": "string",
    "lastName": "string",
    "username": "string",
    "email": "string",
    "password": "string"
}
```

### Forgot Password
- **Endpoint**: `/User/ForgotPassword`
- **Method**: POSTWhat challenges did you face, and how did you overcome them
- **Body**:
```json
{
    "email": "string"
}
```

### Reset Password
- **Endpoint**: `/User/ResetPassword`
- **Method**: POST
- **Body**:
```json
{
    "token": "string",
    "newPassword": "string"
}
```

## Student Management

### Get All Students
- **Endpoint**: `/Student/GetAll`
- **Method**: GET
- **Query Parameters**:
  - pageNumber (number)
  - pageSize (number)
- **Authorization**: Required
- **Response**:
```json
{
    "data": [
        {
            "id": "number",
            "firstName": "string",
            "lastName": "string",
            "email": "string",
            "college": "string",
            "course": "string",
            "yearOfStudy": "number"
        }
    ],
    "totalCount": "number"
}
```

### Get Student by ID
- **Endpoint**: `/Student/{id}`
- **Method**: GET
- **Authorization**: Required

### Create Student
- **Endpoint**: `/Student`
- **Method**: POST
- **Authorization**: Required
- **Body**:
```json
{
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "collegeId": "number",
    "course": "string",
    "yearOfStudy": "number",
    "resumeBase64": "string"
}
```

### Update Student
- **Endpoint**: `/Student`
- **Method**: PUT
- **Authorization**: Required

### Delete Student
- **Endpoint**: `/Student/{id}`
- **Method**: DELETE
- **Authorization**: Required

## Internship Management

### Get All Internships
- **Endpoint**: `/Internship/GetAll`
- **Method**: GET
- **Query Parameters**:
  - pageNumber (number)
  - pageSize (number)
- **Authorization**: Required
- **Response**:
```json
{
    "data": [
        {
            "id": "number",
            "title": "string",
            "description": "string",
            "requirements": "string",
            "duration": "number",
            "startDate": "date",
            "endDate": "date"
        }
    ],
    "totalCount": "number"
}
```

### Get Internship by ID
- **Endpoint**: `/Internship/{id}`
- **Method**: GET
- **Authorization**: Required

### Create Internship
- **Endpoint**: `/Internship`
- **Method**: POST
- **Authorization**: Required (Admin only)
- **Body**:
```json
{
    "title": "string",
    "description": "string",
    "requirements": "string",
    "duration": "number",
    "startDate": "date",
    "endDate": "date"
}
```

### Update Internship
- **Endpoint**: `/Internship`
- **Method**: PUT
- **Authorization**: Required (Admin only)

### Delete Internship
- **Endpoint**: `/Internship/{id}`
- **Method**: DELETE
- **Authorization**: Required (Admin only)

## Application Management

### Get All Applications
- **Endpoint**: `/Application/GetAll`
- **Method**: GET
- **Query Parameters**:
  - pageNumber (number)
  - pageSize (number)
- **Authorization**: Required
- **Response**:
```json
{
    "data": [
        {
            "id": "number",
            "studentId": "number",
            "internshipId": "number",
            "status": "string",
            "createdAt": "date"
        }
    ],
    "totalCount": "number"
}
```

### Submit Application
- **Endpoint**: `/Application`
- **Method**: POST
- **Authorization**: Required
- **Body**:
```json
{
    "studentId": "number",
    "internshipId": "number"
}
```

### Update Application Status
- **Endpoint**: `/Application`
- **Method**: PUT
- **Authorization**: Required (Admin only)
- **Body**:
```json
{
    "id": "number",
    "statusId": "number"
}
```

## Authentication and Security

### JWT Token Format
- Bearer token required in Authorization header
- Token expiry: 60 minutes
- Token contains:
  - User ID
  - Username
  - Role
  - Expiration time

### Error Responses
```json
{
    "success": false,
    "message": "string",
    "errors": ["string"]
}
```

### HTTP Status Codes
- 200: Success
- 201: Created
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 500: Internal Server Error

## API Features

### Pagination
All list endpoints support pagination with:
- pageNumber: Page number (starts from 1)
- pageSize: Number of items per page
- Response includes totalCount for UI pagination

### CORS
- Configured to allow any origin in development
- Specific origins in production

### File Handling
- Resume upload supports Base64 encoded files
- Maximum file size: 5MB
- Supported formats: PDF, DOC, DOCX

### Data Validation
- Required fields validation
- Email format validation
- Date range validation
- File size and format validation

## Development Setup

### Backend (.NET)
1. Update connection string in appsettings.json
2. Run database migrations
3. Start the API server

### Frontend (Angular)
1. Update API URL in environment.ts
2. Install dependencies: `npm install`
3. Start development server: `ng serve`

## Testing
- Backend unit tests using xUnit
- Frontend unit tests using Jasmine
- API tests using Postman/Swagger 