Club Deportivo — ASP.NET MVC (Proyecto)
Proyecto académico — Analista en Sistemas (2º año)

Aplicación web desarrollada en ASP.NET Core MVC (.NET 8) que permite la gestión de socios y actividades deportivas de un club, incluyendo altas, bajas, modificaciones, vinculación entre socios y actividades, y reportes comparativos.

Objetivo

Desarrollar una aplicación web cliente/servidor bajo el patrón Modelo–Vista–Controlador (MVC), con integración a base de datos mediante Entity Framework Core, validaciones, y manejo básico de relaciones entre entidades.

Funcionalidades principales
Módulo	Descripción
Socios	Alta, edición, baja, listado, búsqueda por DNI o nombre.
Actividades	Alta, edición, baja lógica, listado, control de cupo y horarios.
Vinculaciones	Asignación de actividades a socios (validando cupos y duplicados).
Reportes	Comparativo de cantidad de socios por actividad.
Validaciones	Reglas de negocio y restricciones de datos (campos requeridos, formato de email, cupos, etc.).
Estructura del proyecto
ClubDeportivo.Web/
│
├── Controllers/
│   ├── SociosController.cs
│   ├── ActividadesController.cs
│   ├── InscripcionesController.cs
│   └── ReportesController.cs
│
├── Models/
│   ├── socio.cs
│   ├── actividad.cs
│   ├── inscripcion.cs
│   ├── validations.cs
│   └── ViewModels/
│       └── inscripcion_create_vm.cs
│
├── Data/
│   ├── appdbcontext.cs
│   └── DesignTimeDbContextFactory.cs
│
├── Views/
│   ├── Socios/
│   ├── Actividades/
│   ├── Inscripciones/
│   ├── Reportes/
│   └── Shared/
│       └── _Layout.cshtml
│
├── appsettings.json
├── Program.cs
└── README.md

Modelo de datos (diagrama simplificado)
Socio (1..*) ─── (Inscripción) ─── (*..1) Actividad


Socio

SocioId, Dni, Nombre, Apellido, FechaNacimiento, Email, Teléfono, Dirección

Actividad

ActividadId, Nombre, Descripción, Días, HoraInicio, HoraFin, Cupo, Activo

Inscripción

InscripcionId, SocioId, ActividadId, FechaInscripcion

Tecnologías utilizadas

C# / .NET 8.0

ASP.NET Core MVC

Entity Framework Core (Code First)

SQL Server (localdb)

Bootstrap 5 / Razor Views

LINQ y Data Annotations

Scaffolding automático (CRUD)

 Instrucciones de ejecución
🔹 1. Clonar o descargar el proyecto
git clone https://github.com/usuario/ClubDeportivo.git
cd ClubDeportivo.Web

🔹 2. Restaurar dependencias
dotnet restore

🔹 3. Crear base de datos
dotnet ef database update

🔹 4. Ejecutar la aplicación
dotnet run

🔹 5. Navegar a:
http://localhost:5038

Características destacadas

Arquitectura MVC limpia y modular.
CRUD completos generados con scaffolding.
Validaciones automáticas en formularios.
Baja lógica en actividades (sin borrar registros).
Reporte comparativo de socios por actividad.
Datos de prueba automáticos (seed inicial).

Autores:
Francisco Domingo Capozzolo
Ricardo Muller
Facundo Guerra 


Analista de Sistemas – Proyecto ASP.NET MVC
Año: 2025