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
- La carpeta `.vs/` está excluida del control de versiones.
- Para subir libros por CSV, usa el endpoint `/api/books/upload` en Swagger.
