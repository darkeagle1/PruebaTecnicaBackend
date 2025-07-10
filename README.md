# Tasks Backend (.NET 6 + EF Core + Onion Architecture)

## Estructura del Proyecto
- **Domain**: Entidades, enums, interfaces de repositorio
- **Application**: Servicios, DTOs, validaciones
- **Infrastructure**: Repositorios, DbContext
- **API**: Controllers, configuración, DI, Swagger, manejo de errores

## Requisitos
- .NET 6 SDK o superior

## Cómo levantar el backend
1. Abre una terminal en esta carpeta (`tasks-backend`).
2. Ejecuta:
   ```sh
   dotnet build
   dotnet run --project src/API/API.csproj
   ```
3. Accede a Swagger en: `http://localhost:5000/swagger`

> La base de datos es en memoria, no requiere configuración adicional.

---

## Notas
- Arquitectura Onion y Repository Pattern aplicados.
- Código limpio y principios SOLID.
- Manejo global de errores y validaciones.
- Documentación Swagger lista para probar los endpoints.
