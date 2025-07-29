# Mi API en .NET Core

Esta es una API desarrollada con ASP.NET Core para gestionar como prueba para authenticar endpoints.

## C�mo usar

- Clona el repositorio
- Deber�s ejecutar el archivo Scripst en sqlserver para crear la bd
- Restaura los paquetes NuGet
- Ejecuta la aplicaci�n

## Endpoints principales

### Para Usuario

Para poder ejecutar el registro de un usuario requieres hacer la petici�n en el siguiente endpoint incluyendo el  json de entrada
- GET /api/Usuario/registro
  El json de entrada es el siguiente: 
  {
  "nombre": "string",
  "correo": "string",
  "clave": "string"
  }

  Lo que obtendr�s como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiraci�n:
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

Para realizar el login deberas hacer la petici�n al siguiente endpoint
- POST /api/Usuario/login
  
  El json de entrada que deber�s enviar en la petici�n es el siguiente:

  {
     "correo": "string",
     "clave": "string"
  }

  Lo que obtendr�s como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiraci�n:

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

### Para Productos

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