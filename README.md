# Usuarios API REST - ASP.NET Core

API REST desarrollada con **ASP.NET Core 8** y **Entity Framework Core** (Code First) para la gestión de usuarios con operaciones CRUD completas.

---

## Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8 (Code First)
- SQLite (base de datos local)
- Swagger / OpenAPI (documentación y pruebas)

---

## Estructura del proyecto

```
UsuariosAPI/
├── Controllers/
│   └── UsuariosController.cs   # Endpoints CRUD
├── Data/
│   └── AppDbContext.cs          # Contexto de Entity Framework
├── Migrations/
│   ├── 20240101000000_InitialCreate.cs
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   └── Usuario.cs               # Modelo de datos
├── appsettings.json             # Configuración y cadena de conexión
├── Program.cs                   # Configuración de la aplicación
└── UsuariosAPI.csproj
```

---

## Cómo ejecutar la API

### Prerrequisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado

### Pasos

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/UsuariosAPI.git
cd UsuariosAPI

# 2. Restaurar paquetes NuGet
dotnet restore

# 3. Aplicar migraciones (crea la base de datos SQLite automáticamente)
dotnet ef database update

# 4. Ejecutar la aplicación
dotnet run
```

La API estará disponible en:
- **Swagger UI:** `http://localhost:5000` (raíz)
- **API Base URL:** `http://localhost:5000/api/usuarios`

> **Nota:** Las migraciones también se aplican automáticamente al iniciar la aplicación (`db.Database.Migrate()` en `Program.cs`).

---

## Endpoints disponibles

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/usuarios` | Obtener todos los usuarios |
| GET | `/api/usuarios/{id}` | Obtener usuario por ID |
| POST | `/api/usuarios` | Crear nuevo usuario |
| PUT | `/api/usuarios/{id}` | Actualizar usuario existente |
| DELETE | `/api/usuarios/{id}` | Eliminar usuario por ID |

---

## Modelo de datos - Usuario

```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "correo": "juan@email.com",
  "fechaDeNacimiento": "1995-06-15T00:00:00"
}
```

---

## Probar con Postman

### 1. Crear usuario (POST)
- **URL:** `POST http://localhost:5000/api/usuarios`
- **Headers:** `Content-Type: application/json`
- **Body:**
```json
{
  "nombre": "Juan Pérez",
  "correo": "juan@email.com",
  "fechaDeNacimiento": "1995-06-15"
}
```
- **Respuesta exitosa (201):**
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "correo": "juan@email.com",
  "fechaDeNacimiento": "1995-06-15T00:00:00"
}
```
- **Error correo duplicado (400):**
```json
{
  "mensaje": "El correo 'juan@email.com' ya está en uso por otro usuario."
}
```

### 2. Obtener todos los usuarios (GET)
- **URL:** `GET http://localhost:5000/api/usuarios`
- **Respuesta (200):**
```json
[
  {
    "id": 1,
    "nombre": "Juan Pérez",
    "correo": "juan@email.com",
    "fechaDeNacimiento": "1995-06-15T00:00:00"
  }
]
```

### 3. Obtener usuario por ID (GET)
- **URL:** `GET http://localhost:5000/api/usuarios/1`
- **Respuesta (200):** objeto usuario
- **No encontrado (404):**
```json
{ "mensaje": "No se encontró el usuario con ID 1." }
```

### 4. Actualizar usuario (PUT)
- **URL:** `PUT http://localhost:5000/api/usuarios/1`
- **Headers:** `Content-Type: application/json`
- **Body:**
```json
{
  "nombre": "Juan Pérez Actualizado",
  "correo": "juan.nuevo@email.com",
  "fechaDeNacimiento": "1995-06-15"
}
```
- **Respuesta (200):** objeto usuario actualizado

### 5. Eliminar usuario (DELETE)
- **URL:** `DELETE http://localhost:5000/api/usuarios/1`
- **Respuesta (200):**
```json
{ "mensaje": "Usuario con ID 1 eliminado correctamente." }
```

---

## Probar con Swagger

Al ejecutar la aplicación, navega a `http://localhost:5000` para acceder a la interfaz de Swagger UI donde puedes probar todos los endpoints directamente desde el navegador.

---

## Manejo de errores

| Código | Situación |
|--------|-----------|
| 200 OK | Operación exitosa |
| 201 Created | Usuario creado correctamente |
| 400 Bad Request | Datos inválidos o correo duplicado |
| 404 Not Found | Usuario no encontrado |

---

## Capturas de pantalla

> *(Agregar capturas de Postman/Swagger aquí después de ejecutar la API)*

### Swagger UI - Lista de endpoints
![Swagger UI](screenshots/swagger-ui.png)

### POST - Crear usuario
![POST Usuario](screenshots/post-usuario.png)

### GET - Obtener todos los usuarios
![GET Usuarios](screenshots/get-usuarios.png)

### PUT - Actualizar usuario
![PUT Usuario](screenshots/put-usuario.png)

### DELETE - Eliminar usuario
![DELETE Usuario](screenshots/delete-usuario.png)

### Error - Correo duplicado (400)
![Error Duplicado](screenshots/error-duplicado.png)
