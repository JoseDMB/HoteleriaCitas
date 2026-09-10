# HoteleriaCitas

Descripción
-----------
Proyecto web para gestión básica de reservas/citas en el contexto de hotelería. Contiene una solución .NET con proyectos web y de acceso a datos; la interfaz está construida con Razor Pages / MVC según módulos presentes.

Funcionalidades principales
---------------------------
- Gestión de entidades relacionadas a clientes y empleados.
- Registro y consulta de citas/reservas.
- APIs y componentes separados en proyectos (ej. API-Hotel, Models/AccessDB).

Estructura del repositorio
--------------------------
- Proyecto principal web: Proyecto1 (Razor Pages / MVC)
- API: API-Hotel
- Proyecto de modelos/acceso a datos: Models/AccessDB
- Contenido estático: Proyecto1/wwwroot/

Tecnologías y dependencias
--------------------------
- .NET 10
- ASP.NET Core (Razor Pages / MVC)
- Libraries cliente (Bootstrap, jQuery) incluidas en wwwroot/lib (pueden restaurarse con LibMan o npm)
- Acceso a datos en el proyecto Models/AccessDB (ver implementación en código)

Requisitos
---------
- .NET 10 SDK instalado
- Visual Studio 2022/2026 o VS Code

Instalación y ejecución
-----------------------
1. Restaurar paquetes y dependencias:
   dotnet restore
2. Compilar la solución:
   dotnet build
3. Ejecutar la aplicación (desde la carpeta del proyecto web):
   dotnet run --project Proyecto1/MVC.csproj

Alternativa (Visual Studio): abrir Proyecto1.slnx y ejecutar desde el depurador.

Notas sobre archivos estáticos
----------------------------
Las librerías cliente en wwwroot/lib están incluidas en el repositorio por conveniencia. Si prefieres restaurarlas en lugar de versionarlas, elimina la carpeta y restaura con LibMan o npm; el .gitignore actual ignora wwwroot/lib para permitir esa opción.

Buenas prácticas antes de publicar
--------------------------------
- Eliminar artefactos de compilación locales: bin/ y obj/ (git rm -r --cached si ya estaban versionados).
- Comprobar que no hay secretos en appsettings.json u otros ficheros. Usar secretos de usuario o variables de entorno para credenciales.

Contribuir
---------
1. Fork del repositorio
2. Crear una rama con la funcionalidad: git checkout -b feature/mi-cambio
3. Commit y push, abrir Pull Request

Licencia
--------
Incluye la licencia que desees antes de publicar (por ejemplo MIT). Si no hay una, añade un archivo LICENSE.

Contacto
-------
Para dudas o soporte en la subida a GitHub, configuración del CI/CD o limpieza del historial, pedir instrucciones concretas.
