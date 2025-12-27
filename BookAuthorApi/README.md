# BookAuthorApi

## Descripción
API RESTful para gestión de libros y autores con autenticación JWT, integración SOAP/REST, carga masiva por CSV y dockerización.

## Ejecución local
```bash
dotnet restore
dotnet run
```

## Docker
### Construir imagen
```bash
docker build -t bookauthorapi .
```
### Ejecutar contenedor
```bash
docker run -d -p 8080:80 bookauthorapi
```
Accede a la API en [http://localhost:8080/swagger](http://localhost:8080/swagger)

### Compatibilidad
- Linux, Mac, Windows 11 (Docker Desktop o nativo)

## Publicar en Azure
Puedes subir la imagen a Azure Container Registry y desplegar en Azure App Service o Azure Container Instance.

## Endpoints principales
- Autenticación: `/api/auth/login`
- Libros: `/api/books`, `/api/books/upload`
- Autores: `/api/authors`

## Swagger
La documentación interactiva está disponible en `/swagger`.
