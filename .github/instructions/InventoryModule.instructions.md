---
applyTo: '**'
---
Coding standards, domain knowledge, and preferences that AI should follow.

# Inventory Module - .NET Web API Implementation Guide

This file provides instructions for implementing an Inventory module in a .NET Web API, focusing on entity structure, relationships, CRUD endpoints, and best practices for primary and foreign keys.

---

## 1. Entities & Relationships

### Inventory Entity

- InventoryId (int, Primary Key)
- Name (string)
- Price (decimal)
- CategoryId (int, Foreign Key to Category)
- AvailableCount (int)
- IsDeleted (bool)
- CreatedBy (string)
- CreatedDate (DateTime)
- ModifiedBy (string)
- ModifiedDate (DateTime)

### Category Entity

- CategoryId (int, Primary Key)
- CategoryName (string)
- IsDeleted (bool)
- CreatedBy (string)
- CreatedDate (DateTime)
- ModifiedBy (string)
- ModifiedDate (DateTime)

**Relationship:**  
- Inventory.CategoryId is a foreign key referencing Category.CategoryId (many-to-one).

---

## 2. Database Design

- Define Inventory and Category tables with the specified fields.
- Set InventoryId and CategoryId as primary keys.
- Create a foreign key constraint from Inventory.CategoryId to Category.CategoryId.
- Use IsDeleted for soft deletes.
- Track creation and modification audit fields.

---

## 3. API Endpoints

### Inventory

- GET    /api/inventory           — List all inventory items (excluding soft-deleted)
- GET    /api/inventory/{id}      — Retrieve one inventory item by ID
- POST   /api/inventory           — Create a new inventory item
- PUT    /api/inventory/{id}      — Update an existing inventory item by ID
- DELETE /api/inventory/{id}      — Soft delete an inventory item by ID

### Category

- GET    /api/category            — List all categories (excluding soft-deleted)
- GET    /api/category/{id}       — Retrieve one category by ID
- POST   /api/category            — Create a new category
- PUT    /api/category/{id}       — Update an existing category by ID
- DELETE /api/category/{id}       — Soft delete a category by ID

---

## 4. Implementation Requirements

- Use Entity Framework Core or equivalent for ORM.
- Always filter out records where IsDeleted is true.
- When creating or updating records, set CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate accordingly.
- Validate that CategoryId exists when adding or updating Inventory.
- Use navigation property in Inventory to reference Category.
- Implement soft delete by setting IsDeleted to true instead of removing records.

---

## 5. Best Practices

- Enforce referential integrity via database foreign key constraints.
- Use Include for eager loading of related Category data in Inventory queries.
- Use DTOs for API request/response models if needed.
- Apply authentication and authorization for create, update, and delete operations.
- Implement pagination and search for inventory listing if required.
- Log or audit changes to entities as appropriate for your application.

---

## 6. UI Integration

- Inventory list should display Name, Price, Category, Available Count, and Actions (Edit/Delete).
- Support adding, editing, viewing, and removing inventory items using the API.
- Display category names by joining Inventory and Category entities.