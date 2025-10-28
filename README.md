# 🎯 API PARCIAL 3 - Juego "Picas y Famas" (Modalidad Solitario)

API RESTful desarrollada para el juego "Picas y Famas" en modalidad solitario. Permite registrar jugadores, iniciar partidas, realizar intentos de adivinanza y visualizar métricas en Power BI.

---

## 📦 Paquetes NuGet requeridos

| Paquete                          | Propósito                                                                 | Comando de instalación                                                  |
|----------------------------------|---------------------------------------------------------------------------|-------------------------------------------------------------------------|
| `Microsoft.EntityFrameworkCore.SqlServer` | ORM para persistencia en base de datos SQL Server                | `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`           |
| `Microsoft.EntityFrameworkCore.Tools`     | Herramientas para migraciones y scaffolding                      | `dotnet add package Microsoft.EntityFrameworkCore.Tools`               |
| `ESCMB.GameCore`                         | Lógica de validación de intentos (famas y picas)                 | `dotnet add package ESCMB.GameCore`                                   |

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

- ---
## 🚀 Endpoints principales

- `POST /api/player/register`: Registra un nuevo jugador.
- `POST /api/game/start`: Inicia una nueva partida para un jugador.
- `POST /api/game/guess`: Realiza un intento de adivinanza en una partida activa.

## 🛠️ Ejemplo de uso

	- Registrar jugador
curl -X POST https://localhost:5001/api/player/register -H "Content-Type: application/json" -d '{"firstName":"Leonel","lastName":"Messi","age":38}'
	
	- - Iniciar partida
curl -X POST https://localhost:5001/api/game/start -H "Content-Type: application/json" -d '{"playerId":1}'
	
	- - Realizar intento de adivinanza
curl -X POST https://localhost:5001/api/game/guess -H "Content-Type: application/json" -d '{"gameId":1,"attemptedNumber":"1234"}'

- ---
## 📊 Integración con Power BI
- Configura Power BI para conectarse a la base de datos SQL Server utilizada por la API.
- Crea informes y dashboards para visualizar métricas como número de partidas jugadas, intentos realizados, y estadísticas de jugadores.
- Utiliza las visualizaciones de Power BI para analizar el rendimiento y la participación de los jugadores en el juego "Picas y Famas".
- ---
## 📝 Notas técnicas

- Requisitos de .NET 8.
- Variables de entorno necesarias (por ejemplo, cadena de conexión).
- Cómo ejecutar migraciones: `dotnet ef migrations add InitialCreate` y `dotnet ef database update`.
