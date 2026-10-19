# Blazor Server Product Management (CRUD) Application

A multi-layered Blazor Server CRUD application built with .NET 10, Npgsql, and a Supabase PostgreSQL backend database.

---

## Technical Architecture

* **Framework**: .NET 10 (Blazor Server with `@rendermode InteractiveServer`)
* **Database**: Supabase PostgreSQL (Managed Cloud Instance)
* **Data Access**: `Npgsql` using `NpgsqlDataSource` Singleton
* **Architecture Pattern**: 4-Layer Separation of Concerns:
  * `Model` — Core data structures (`ProductModel`)
  * `DataAccessLayer` — PostgreSQL queries & connection handling (`CRUD`, `DBHelper`)
  * `BusinessLogic` — Service layer (`IProductService`, `ProductService`)
  * `UserInterface` — Blazor components & UI pages (`Home.razor`)

---

## Prerequisites

Before setting up the project locally, ensure you have installed:

1. [.NET 10 SDK](https://dotnet.microsoft.com/download)
2. [Git](https://git-scm.com/downloads)
3. Access to a [Supabase Project](https://supabase.com/) with a PostgreSQL instance.

---

## Database Setup (Supabase)

1. Open your **Supabase Dashboard** $\rightarrow$ **SQL Editor**.
2. Run the following script to create the `Products` table and configure Row Level Security (RLS):

```sql
-- Create the Products table
CREATE TABLE IF NOT EXISTS public."Products" (
    "PId" INT8 PRIMARY KEY,
    "ProductName" VARCHAR(255) NOT NULL,
    "Price" INT8 NOT NULL
);

-- Option A: Disable RLS for testing/local development
ALTER TABLE public."Products" DISABLE ROW LEVEL SECURITY;

-- Option B (Alternative): Enable RLS with a permissive public policy
-- ALTER TABLE public."Products" ENABLE ROW LEVEL SECURITY;
-- CREATE POLICY "Allow full CRUD access" 
-- ON public."Products" FOR ALL 
-- USING (true) WITH CHECK (true);

Building and Running the Application
Restore dependencies and build the solution:

Bash
dotnet build
Run the UserInterface project:

Bash
dotnet run --project UserInterface/UserInterface.csproj
Open your browser and navigate to:

Plaintext
http://localhost:5215
