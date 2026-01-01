# FinalExamination

## Descripción
Solución de ejemplo para la gestión de libros y autores con autenticación JWT, subida de archivos CSV y API documentada con Swagger.

## Estructura del proyecto
- `BookAuthorApi/` - Proyecto principal de la API
- `BookAuthorApi.Tests/` - Pruebas unitarias

## Características principales
- CRUD de libros y autores
- Autenticación y autorización JWT
- Subida de libros vía archivo CSV
- Documentación interactiva con Swagger

## Uso rápido
1. Clona el repositorio
2. Restaura los paquetes NuGet
3. Configura la cadena de conexión en `appsettings.json`
4. Ejecuta las migraciones de base de datos
5. Inicia la API y accede a `/swagger` para probar los endpoints

## Credenciales de prueba
- Usuario: `admin` / Contraseña: `Password123`
- Usuario: `user` / Contraseña: `Password123`

## Notas

## Script de Base de Datos
Para crear la base de datos y las tablas necesarias manualmente, ejecuta el script `script01012026.sql` en tu servidor SQL Server.

Ejemplo de uso:
1. Abre SQL Server Management Studio (SSMS) o Azure Data Studio.
2. Copia el contenido de `script01012026.sql`.
3. Ejecútalo sobre tu base de datos destino.

El script incluye:
- Creación de tablas `Authors`, `Books` y migraciones de EF Core.
- Inserción de datos de ejemplo.
- Relaciones y llaves foráneas.
