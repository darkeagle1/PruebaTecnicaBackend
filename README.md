# Tasks Backend (.NET 6 + EF Core + Onion Architecture)

## Estructura del Proyecto
- **Domain**: Entidades, enums, interfaces de repositorio
- **Application**: Servicios, DTOs, validaciones
- **Infrastructure**: Repositorios, DbContext
- **API**: Controllers, configuración, DI, Swagger, manejo de errores

## Requisitos
- .NET 6 SDK o superior
- Visual Studio 2022 (recomendado)

## Cómo levantar el backend
1. Abre el archivo de solución .sln en Visual Studio.
2. Establece el proyecto API como proyecto de inicio.
3. Presiona F5 o haz clic en "Iniciar" para compilar y ejecutar.
4. Una vez iniciado, accede a la documentación Swagger en:
👉 http://localhost:5032/swagger

> La base de datos es en memoria, no requiere configuración adicional.

---

## Notas
✔️ Arquitectura basada en Onion + Repository Pattern.

✔️ Principios SOLID aplicados.

✔️ Validaciones robustas y manejo globalizado de excepciones.

✔️ Swagger habilitado para probar y explorar los endpoints REST.

✔️ Ideal para pruebas y extensiones futuras (migración a persistencia real, autenticación, etc).
