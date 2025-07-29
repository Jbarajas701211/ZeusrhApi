# Mi API en .NET Core

Esta es una API desarrollada con ASP.NET Core para gestionar como prueba para authenticar endpoints.

## Cómo usar

- Clona el repositorio
- Deberás ejecutar el archivo Scripst en sqlserver para crear la bd
- Restaura los paquetes NuGet
- Ejecuta la aplicación

## Endpoints principales

### Para Usuario

Para poder ejecutar el registro de un usuario requieres hacer la petición en el siguiente endpoint incluyendo el  json de entrada
- GET /api/Usuario/registro
  El json de entrada es el siguiente: 
  {
  "nombre": "string",
  "correo": "string",
  "clave": "string"
  }

  Lo que obtendrás como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiración:
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

Para realizar el login deberas hacer la petición al siguiente endpoint
- POST /api/Usuario/login
  
  El json de entrada que deberás enviar en la petición es el siguiente:

  {
     "correo": "string",
     "clave": "string"
  }

  Lo que obtendrás como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiración:

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