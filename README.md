# Mi API en .NET Core

Esta es una API desarrollada con ASP.NET Core para gestionar como prueba para authenticar endpoints.

## Cómo usar

- Clona el repositorio
- Deberás ejecutar el archivo Scripst en sqlserver para crear la bd
- Restaura los paquetes NuGet
- Ejecuta la aplicación

## Endpoints principales

# Para Usuario

- GET /api/Usuario/registro
  El json de entrada es el siguiente: 
  {
  "nombre": "string",
  "correo": "string",
  "clave": "string"
  }

  La salida es la siguiente:
  {
  "success": true,
  "data": {
    "token": "string",
    "expiracion": "2025-07-29T20:34:12.695Z"
  },
  "errors": [
    "string"
  ]
  }


- POST /api/Usuario/login
  
  El json de entrada es el siguiente:

  {
     "correo": "string",
     "clave": "string"
  }

  El json de salida es el siguiente:

  {
  "success": true,
  "data": {
    "token": "string",
    "expiracion": "2025-07-29T20:37:44.484Z"
  },
  "errors": [
    "string"
  ]
}

# Para Productos

- GET /api/Producto

  El json de entrada es el siguiente:

  {
        "idProducto" : 1
  }

  El json de salida es el siguiente:

  {
     "success": true,
     "data": {
       "nombre": "string",
       "marcas": "string",
       "precio": 0
     },
     "errors": [
       "string"
       ]
  }

- POST /api/Producto

El json de entrada es el siguiente:

  {
     "nombre": "string",
     "marcas": "string",
     "precio": 0
  }

  El json de salida es el siguiente:

  {
     "success": true,
     "data": {
       "nombre": "string",
       "marcas": "string",
       "precio": 0
     },
     "errors": [
       "string"
       ]
  }