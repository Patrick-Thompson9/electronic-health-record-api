# Electronic Health Record API Documentation

See HTTP files in Requests folder for testing the following routes.

## Table of Contents

1. [Patients](#patients)
   [1.1 Create](#11-create-patient)
   [1.2 Read](#12-read-patients)
   [1.3 Update](#13-update-patient)
   [1.4 Delete](#14-delete-patient)
2. [Orders](#orders)
   [2.1 Create](#21-create-order)
   [2.2 Read](#12-read-patients)
   [2.3 Update](#13-update-patient)
   [2.4 Delete](#14-delete-patient)
3. [Tests](#tests)
   [3.1 Create](#31-create-test)
   [3.2 Read](#32-read-test)
   [3.3 Update](#33-update-test)
   [3.4 Delete](#34-delete-test)

# Patients

## 1.1 Create Patient

Note that associated objects can not be created during the creation of the parent object. For example, when creating a patient, it is not possible to create an associated order inside the same request.

### Post Request

```
POST /patients
Content-Type: application/json

{
    "FirstName": "John",
    "LastName": "Doe",
    "DateOfBirth": "2000-01-06"
}
```

### Post Response

```
201 Created
```

```
{
    "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "mrn": "0000000001",
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "2000-01-06
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "Orders": []
}
```

## 1.2 Read Patients

Note that all Get requests will cascade, meaning all associated objects will be returned alongside the requested data. For example: searching a patient by ID will return that patient, any associated orders and tests.

Patients are returned in descending order using their Medical Record Numbers (mrn).

## Get All Patients

Note this endpoint has an optional search parameter called `limit` which limits the number of returned results. The default value is `limit = 20`.

### Get Request

```
GET /patients
```

### Get Response

```
200 Ok
```

```
[
    {
        "id": "551668e4-8acb-43e2-9fbc-10b4bd90b48d",
        "mrn": "0000000002",
        "firstName": "Jane",
        "lastName": "Doe",
        "dateOfBirth": "2000-03-12
        "dateTimeCreated": "2025-08-19T12:00:00",
        "lastUpdated": "2025-08-19T12:00:00",
        "Orders": []
    },
    {
        "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
        "mrn": "0000000001",
        "firstName": "John",
        "lastName": "Doe",
        "dateOfBirth": "2000-01-06
        "dateTimeCreated": "2025-08-18T12:00:00",
        "lastUpdated": "2025-08-19T11:00:00",
        "Orders": []
    }
]
```

## Get Patient By ID

### Get Request

```
GET /patients/f5bd2784-029a-4c4f-905c-799fee96f8e0
```

### Get Response

```
200 Ok
```

```
{
    "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "mrn": "0000000001",
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "2000-01-06
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "Orders": []
}
```

## Get Patient By MRN

### Get Request

```
GET /patients/mrn/0000000001
```

### Get Response

```
200 Ok
```

```
{
    "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "mrn": "0000000001",
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "2000-01-06
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "Orders": []
}
```

## 1.3 Update Patient

Note that you can not update a patients ID, MRN, or Orders. To update a patients orders and associated tests you must use the endpoint specific to the object you want to edit.

If an update request contains information of a patient that doesn't exist, it will create it.

### Put Request

```
PUT /patients/f5bd2784-029a-4c4f-905c-799fee96f8e0
Content-Type: application/json

{
    "FirstName": "John",
    "LastName": "Doe",
    "DateOfBirth": "2000-01-06",
}
```

### Put Response

`200 Ok` or `201 Created`

```
{
    "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "mrn": "0000000001",
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "2000-01-06
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "Orders": []
}
```

## 1.4 Delete Patient

Note that all delete requests will cascade. Meaning if a patient is deleted, the associated orders and tests will also be deleted.

### Delete Request

```
DELETE /patients/f5bd2784-029a-4c4f-905c-799fee96f8e0
```

### Delete Response

```
204 No Content
```

# Orders

## 2.1 Create Order

Note that associated objects can not be created during the creation of the parent object. For example, when creating an order, it is not possible to create an associated test inside the same request.

### Post Request

```
POST /orders
Content-Type: application/json

{
    "PatientId": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "OrderType": "COVID",
    "Notes": "Sample note text."
}
```

### Post Response

```
201 Created
```

```
{
    "id": "95bd2784-s29a-fc4f-805c-76ddeerf89e0",
    "patientId": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "orderNumber": "0000000001",
    "orderType": "COVID",
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "notes" "Sample note text.":
    "Test": null
}
```

## 2.2 Read Order

Note that all Get requests will cascade, meaning all associated objects will be returned alongside the requested data. For example: searching an order by ID will return that order, an associated test.

Orders are returned in descending order using their order number.

## Get All Orders

Note this endpoint has an optional search parameter called `limit` which limits the number of returned results. The default value is `limit = 20`.

### Get Request

```
GET /orders
```

### Get Response

```
200 Ok
```

```
[
    {
        "id": "E5bd2674-z3la-41xf-9v5c-799fe796f8e0",
        "patientId": "00000000-0000-0000-000000002",
        "orderNumber": "0000000002",
        "orderType": "Flu",
        "dateTimeCreated": "2025-08-18T13:00:00"
        "lastUpdated": "2025-08-18T13:00:00"
        "notes":"Example order notes 2",
        "tests": null
    },
    {
        "id": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
        "patientId": "00000000-0000-0000-000000001",
        "orderNumber": "0000000001",
        "orderType": "COVID",
        "dateTimeCreated": "2025-08-18T13:00:00"
        "lastUpdated": "2025-08-18T13:00:00"
        "notes":"Example order notes",
        "tests": null
    }
]
```

## Get Order By ID

### Get Request

```
GET /orders/D5bd2784-039a-4c4f-9v5c-799fe796f8e0
```

### Get Response

```
200 Ok
```

```
{
    "id": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
    "patientId": "00000000-0000-0000-000000001",
    "orderNumber": "0000000001",
    "orderType": "COVID",
    "dateTimeCreated": "2025-08-18T13:00:00"
    "lastUpdated": "2025-08-18T13:00:00"
    "notes":"Example order notes",
    "tests": null
}
```

## Get Order By Order Number

### Get Request

```
GET /orders/orderNumber/0000000001
```

### Get Response

```
200 Ok
```

```
{
    "id": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
    "patientId": "00000000-0000-0000-000000001",
    "orderNumber": "0000000001",
    "orderType": "COVID",
    "dateTimeCreated": "2025-08-18T13:00:00"
    "lastUpdated": "2025-08-18T13:00:00"
    "notes":"Example order notes",
    "tests": null
}
```

## Get Orders By MRN

### Get Request

```
GET /patients/f5bd2784-029a-4c4f-905c-799fee96f8e0/orders
```

### Get Response

```
200 Ok
```

```
[
    {
        "id": "E5bd2674-z3la-41xf-9v5c-799fe796f8e0",
        "patientId": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
        "orderNumber": "0000000002",
        "orderType": "Flu",
        "dateTimeCreated": "2025-08-18T13:00:00"
        "lastUpdated": "2025-08-18T13:00:00"
        "notes":"Example order notes 2",
        "tests": null
    },
    {
        "id": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
        "patientId": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
        "orderNumber": "0000000001",
        "orderType": "COVID",
        "dateTimeCreated": "2025-08-18T13:00:00"
        "lastUpdated": "2025-08-18T13:00:00"
        "notes":"Example order notes",
        "tests": null
    }
]
```

## 2.3 Update Order

Note that you can not update an order's ID, order number, or test. To update an order's test you must use the endpoint specific to the object you want to edit (/tests).

If an update request contains information of an order that doesn't exist, it will create it.

### Put Request

```
PUT /orders/f5bd2784-029a-4c4f-905c-799fee96f8e0
Content-Type: application/json

{
    "PatientId": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
    "OrderType": "COVID",
    "Notes": "Example order notes."
}
```

### Put Response

`200 Ok` or `201 Created`

```
{
    "id": "f5bd2784-029a-4c4f-905c-799fee96f8e0",
    "patientId": "D5bd2784-039a-4c4f-9v5c-799fe796f8e0",
    "orderNumber": "0000000001",
    "orderType": "COVID",
    "dateTimeCreated": "2025-08-18T13:00:00"
    "lastUpdated": "2025-08-18T13:00:00"
    "notes":"Example order notes.",
    "tests": null
}
```

## 2.4 Delete Order

Note that all delete requests will cascade. Meaning if a patient is deleted, the associated orders and tests will also be deleted.

### Delete Request

```
DELETE /orders/f5bd2784-029a-4c4f-905c-799fee96f8e0
```

### Delete Response

```
204 No Content
```

# Tests

## 3.1 Create Test

Note that associated objects can not be created during the creation of the parent object. For example, when creating an order, it is not possible to create an associated test inside the same request.

Note that creating a test for an order that already contains a test, the existing test will be overwritten by the new test.

### Post Request

```
POST /tests
Content-Type: application/json

{
    "OrderId": "94a7bcb9-41ef-430c-bc95-6f139a503ebd",
    "Result": "Positive",
    "DateTimeOrdered": "2023-10-01T12:00:00"
}
```

### Post Response

```
201 Created
```

```
{
    "id": "3a7d4aeb-0a17-4bdf-86c8-e2bf1f4b07e9",
    "orderId": "94a7bcb9-41ef-430c-bc95-6f139a503ebd",
    "result": "Positive",
    "dateTimeCreated": "2025-08-23T13:47:16.7730237",
    "lastUpdated": "2025-08-23T13:47:16.7730238"
}
```

## 3.2 Read Test

## Get All Tests

Note this endpoint has an optional search parameter called `limit` which limits the number of returned results. The default value is `limit = 20`.

tests are returned in descending order using their date of creation.

### Get Request

```
GET /tests
```

### Get Response

```
200 Ok
```

```
[
    {
    "id": "2cc5e8e7-34de-4708-882f-cbd0939a418c",
    "orderId": "ca275ab8-0510-415e-9373-97e02f94ced9",
    "result": "Negative",
    "dateTimeCreated": "2025-08-23T13:51:23.1297723Z",
    "lastUpdated": "2025-08-23T13:51:23.1297723Z"
    },
    {
    "id": "3a7d4aeb-0a17-4bdf-86c8-e2bf1f4b07e9",
    "orderId": "94a7bcb9-41ef-430c-bc95-6f139a503ebd",
    "result": "Positive",
    "dateTimeCreated": "2025-08-22T13:47:16.7730237",
    "lastUpdated": "2025-08-22T13:47:16.7730238"
    }
]
```

## Get Test By ID

### Get Request

```
GET /tests/2cc5e8e7-34de-4708-882f-cbd0939a418c
```

### Get Response

```
200 Ok
```

```
{
  "id": "2cc5e8e7-34de-4708-882f-cbd0939a418c",
  "orderId": "ca275ab8-0510-415e-9373-97e02f94ced9",
  "result": "Negative",
  "dateTimeCreated": "2025-08-23T13:51:23.1297723",
  "lastUpdated": "2025-08-23T13:51:23.1297723"
}
```

## 3.3 Update Test

Note that you can not update an test's ID.

If an update request contains information of a test that doesn't exist, it will create it.

### Put Request

```
PUT /tests/2cc5e8e7-34de-4708-882f-cbd0939a418c
Content-Type: application/json

{
    "OrderId": "ca275ab8-0510-415e-9373-97e02f94ced9",
    "DateTimeOrdered": "2023-10-01T12:00:00",
    "Result": "Positive"
}
```

### Put Response

`200 Ok` or `201 Created`

```
{
  "id": "2cc5e8e7-34de-4708-882f-cbd0939a418c",
  "orderId": "ca275ab8-0510-415e-9373-97e02f94ced9",
  "result": "Positive",
  "dateTimeCreated": "2025-08-23T13:51:23.1297723",
  "lastUpdated": "2025-08-23T13:55:19.6281899Z"
}
```

## 3.4 Delete Test

### Delete Request

```
DELETE /tests/2cc5e8e7-34de-4708-882f-cbd0939a418c
```

### Delete Response

```
204 No Content
```
