# Gestor de Tickets de Soporte Técnico (Entrenamiento N-Capas)

## Objetivo Principal
Desarrollar un proyecto de práctica en un plazo de 2 semanas para dominar el flujo de trabajo en una arquitectura clásica de N-Capas en el ecosistema .NET, facilitando la adaptación a los estándares de desarrollo de una nueva empresa.

## Stack Tecnológico y Arquitectura
- **Base de Datos**: SQL Server (Uso estricto de Procedimientos Almacenados para todas las operaciones).
- **Acceso a Datos**: C# / .NET (Recomendado: Dapper o ADO.NET para mapear los resultados de los SPs).
- **Backend / Lógica**: .NET Web API RESTful.
- **Frontend / Presentación**: ASP.NET Core con Vistas Razor (Renderizado del lado del servidor).
- **Arquitectura de la Solución**: 3 proyectos separados (`SupportManager.Data`, `SupportManager.Api`, `SupportManager.Web`).

## Requerimientos Funcionales
El sistema debe permitir gestionar incidencias (tickets) que incluyan evidencia en imagen y ubicación geográfica.

### Módulo de Tickets (CRUD)
- Crear, leer y actualizar el estatus de tickets de soporte.

### Módulo de Archivos (Evidencia)
- Subir imágenes adjuntas desde un formulario HTML (`multipart/form-data`) en Razor.
- El frontend Razor debe capturar el archivo y enviarlo mediante una petición HTTP (usando `HttpClient` y `MultipartFormDataContent`) hacia un endpoint de la Web API.
- La API debe recibir el archivo con `IFormFile`, guardarlo físicamente y registrar la ruta en base de datos.

### Módulo de Geolocalización (Google Maps API)
- **Creación**: Un mapa interactivo en el formulario donde el usuario haga clic para definir la ubicación del problema (Latitud y Longitud guardadas en inputs ocultos para enviarse al backend).
- **Visualización**: El listado principal de tickets debe pintar un mapa con múltiples pines mostrando la ubicación de todos los reportes abiertos, requiriendo pasar datos serializados desde el backend C# hacia el JavaScript de la vista.

## Objetivos de Aprendizaje (Foco del Asistente)
- **Flujo estricto Bottom-Up**: Construir desde la persistencia (SQL) hasta la interfaz (Razor) sin mezclar responsabilidades.
- **Dominio de Formularios Complejos**: Transporte de archivos e información binaria entre proyectos .NET (Razor -> Web API).
- **Interoperabilidad Razor - JS**: Conectar el estado renderizado en el servidor (C#) con scripts del lado del cliente (Google Maps JS API).
- **Clean Code**: Mantener los controladores ligeros y centralizar la lógica de negocio y llamadas a base de datos en repositorios/servicios.
