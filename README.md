# DISLAMS – Governed Attendance & Correction Service

## Overview
Backend-only system implementing governed attendance with immutable history,
controlled state transitions, role-based actions, and full audit logging.

## Tech Stack
- .NET 7 Web API
- Entity Framework Core
- SQL Server / LocalDB

## How to Run
1. Update connection string in `appsettings.json`
2. Run migrations:
3. Run the API:

## Key Features
- Draft → Submit → Approve → Publish → Lock lifecycle
- No silent edits
- Corrections via versioning
- Append-only audit logs

## Assumptions & Scope
- Authentication is mocked
- Student identities are opaque
- No UI by design

## Author
Chanderkant Sharma
