# MedicalCatalog - Practica 2

## Integrantes
- Sebastian Rojas Vargas
- Nicolas
- Gabriel
- [Pendiente nombre cuarto integrante]

## Enlace del repositorio
- [Agregar URL del repositorio aqui]

## Arquitectura del proyecto
- `MedicalCatalog.Domain`: entidades del dominio (`Categoria`, `Producto`).
- `MedicalCatalog.Application`: interfaces y servicios de negocio (BLL), validaciones y reglas.
- `MedicalCatalog.Infrastructure`: acceso a datos (DAL), `AppDbContext`, repositorio generico con EF Core + SQLite.
- `MedicalCatalog.Web`: presentacion MVC (controladores, vistas Razor, middleware global de excepciones).

## Paquetes NuGet usados
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Design`

## Principios y patrones aplicados
- `Repository Pattern`: interfaz `IRepository<T>` e implementacion `Repository<T>`.
- `Dependency Injection`: registro de servicios, repositorios y DbContext en `Program.cs`.
- `Separation of Concerns`: separacion de capas Domain/Application/Infrastructure/Web.
- `SOLID`:
  - SRP: cada clase tiene responsabilidad enfocada.
  - OCP: se puede extender logica de negocio agregando nuevos servicios.
  - DIP: controladores dependen de interfaces de servicios, no de implementaciones concretas.

## Ejecucion
1. Restaurar paquetes:
   ```bash
   dotnet restore
   ```
2. Crear migracion inicial (si no existe):
   ```bash
   dotnet ef migrations add InitialCreate --project MedicalCatalog.Infrastructure --startup-project MedicalCatalog.Web
   ```
3. Aplicar migraciones a SQLite:
   ```bash
   dotnet ef database update --project MedicalCatalog.Infrastructure --startup-project MedicalCatalog.Web
   ```
4. Ejecutar aplicacion:
   ```bash
   dotnet run --project MedicalCatalog.Web
   ```
