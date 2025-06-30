# 🎵 Gestión Escuelas de Música - Prueba Técnica Italika

**👤 Autor:** Joseth David Acosta Loayza  
**📅 Fecha:** 29 Junio 2025

---

## 📄 Descripción

Proyecto API REST para la gestión de escuelas de música, incluyendo administración de alumnos, profesores, escuelas y las relaciones entre ellos (asignaciones e inscripciones).  
Implementado con .NET 9, Entity Framework Core y Swagger para documentación y pruebas.

---

## 📂 Repositorio GitHub

El código fuente completo y actualizado de este proyecto está disponible en:
https://github.com/JosethAcostaLoayza/GestionEscuelasMusica

---

## 📂 Contenido del repositorio

- 📁 Código fuente del API (Controllers, Services, DTOs, Entities, Data Context)  
- 🗃️ Archivo SQL para crear la base de datos, tablas, procedimientos almacenados e insertar datos de ejemplo (`EscuelaMusica_DBScript.sql`)  
- 📑 Configuración para Swagger  
- 📝 README (este archivo)

---

## ⚙️ Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) instalado  
- 🖥️ SQL Server (local o remoto) para alojar la base de datos  
- 🛠️ Cliente para ejecutar scripts SQL (SQL Server Management Studio, Azure Data Studio, o similar)  

---

## 🚀 Configuración y despliegue

### 1️⃣ Crear la base de datos

Ejecuta el script `EscuelaMusica_DBScript.sql` en tu servidor SQL para crear la base de datos, tablas, procedimientos almacenados y registros iniciales.

---

### 2️⃣ Configurar la cadena de conexión y ejecutar la API

En el archivo `appsettings.json`, actualiza la cadena de conexión:

```json
{
  "ConnectionStrings": {
    "EscuelaMusicaConnection": "Server=TU_SERVIDOR;Database=Italika_GestionEscuelasMusica;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
Reemplaza TU_SERVIDOR por el nombre o IP de tu servidor SQL.
Luego, desde la terminal en la carpeta del proyecto o desde tu IDE favorito, ejecuta:
```bash
dotnet run
```
Esto levantará la API en http://localhost:5223 (puerto puede variar).

### 3️⃣ Probar la API con Swagger
Abre tu navegador y accede a:
```bash
http://localhost:5223/swagger/index.html
```
Desde ahí puedes ver toda la documentación y probar los endpoints interactivos.

## 🏗️ Estructura principal del API
- 👨‍🎓 Alumnos: CRUD para estudiantes
- 👩‍🏫 Profesores: CRUD para profesores
- 🎼 Escuelas: CRUD para escuelas
- 🔗 Asignaciones: Endpoints para asignar alumnos a profesores e inscribir alumnos en escuelas
- 📊 Consultas: Endpoints para obtener información combinada (ej. alumnos por profesor con escuela)


## ⚠️ Notas importantes
- 🔑 Los identificadores (Identificacion) para alumnos y profesores son únicos y no pueden actualizarse.
- 📦 Se usan DTOs para manejar las solicitudes y respuestas en la API.
- ✔️ El proyecto incluye validaciones básicas para evitar duplicados en asignaciones o inscripciones.

## 📬 Contacto

Para cualquier duda, sugerencia o comentario, puedes contactarme en:  

- ✉️ **Email:** joseth.d.acosta@example.com 
- 🔗 **LinkedIn:** www.linkedin.com/in/joseth-acosta

¡Estaré encantado de ayudarte!
