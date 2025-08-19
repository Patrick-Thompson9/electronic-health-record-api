# Electronic Health Record API Documentation

## Table of Contents

1. Patients
   1.1 Create
   1.2 Read
   1.3 Update
   1.4 Delete
2. Orders
   2.1 Create
   2.2 Read
   2.3 Update
   2.4 Delete
3. Tests
   3.1 Create
   3.2 Read
   3.3 Update
   3.4 Delete

# Patients

## 1.2 Read Patient

### Get Request

```
POST /patients/
```

### Get Response

```
200 Ok
```

```
{
    "id": "00000000-0000-0000-000000000",
    "mrn": "012346789",
    "firstName": "John",
    "lastName": "Doe",
    "dateTimeCreated": "2025-08-18T12:00:00",
    "lastUpdated": "2025-08-19T12:00:00",
    "orders" :
        [
            "00000000-0000-0000-000000001",
            "00000000-0000-0000-000000002"
        ]
}
```

# Orders

## 2.2 Read Order

### Get Request

```
POST /orders/
```

### Get Response

```
200 Ok
```

```
{
    "id": "00000000-0000-0000-000000000",
    "patientId": "00000000-0000-0000-000000001",
    "notes":"Example order notes",
    "dateTimeCreated": "2025-08-18T13:00:00"
    "lastUpdated": "2025-08-18T13:00:00"
    "tests":
        [

        ]
}
```

# Tests

## 3.2 Read Test

### Get Request

```
POST /tests/
```

### Get Response

```
200 Ok
```

```
{
    "id": "00000000-0000-0000-000000000",
    "orderId": "00000000-0000-0000-000000001",
    "dateTimeOrdered": "2025-08-18T13:00:00"
    "dateTimeCreated": "2025-08-19T12:00:00"
    "result": "Negative",
}
```
