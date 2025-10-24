# 🎯 API PARCIAL 3 - Juego "Picas y Famas" (Modalidad Solitario)

API RESTful desarrollada para el juego "Picas y Famas" en modalidad solitario. Permite registrar jugadores, iniciar partidas, realizar intentos de adivinanza y visualizar métricas en Power BI.

---

## 📦 Paquetes NuGet requeridos

| Paquete                          | Propósito                                                                 | Comando de instalación                                                  |
|----------------------------------|---------------------------------------------------------------------------|-------------------------------------------------------------------------|
| `Microsoft.EntityFrameworkCore.SqlServer` | ORM para persistencia en base de datos SQL Server                | `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`           |
| `Microsoft.EntityFrameworkCore.Tools`     | Herramientas para migraciones y scaffolding                      | `dotnet add package Microsoft.EntityFrameworkCore.Tools`               |
| `ESCMB.GuessCore`                         | Lógica de validación de intentos (famas y picas)                 | `dotnet add package ESCMB.GuessCore`                                   |

> 💡 Todos los comandos deben ejecutarse desde la carpeta que contiene el archivo `.csproj` del proyecto.

---

## 🧱 Estructura del proyecto

API PARCIAL 3/ 
- `Controllers/`: Controladores API para manejar solicitudes HTTP.
- `Data/`: Contexto de base de datos y configuración de Entity Framework.
- `DataTransferObjects/`: DTOs para la transferencia de datos entre cliente y servidor.
- `Models/`: Modelos de datos que representan las entidades del juego.
- `Services/`: Lógica de negocio y servicios para manejar la funcionalidad del juego.
- `Migrations/`: Archivos de migración de Entity Framework para la base de datos.
- `Program.cs` y `Startup.cs`: Configuración de la aplicación y servicios.
- `appsettings.json`: Configuración de la aplicación, incluyendo cadenas de conexión.
