# Documentación de la API
Este documento proporciona una visión general de los endpoints de la API para gestionar usuarios en la aplicación.

Si bien el desarrollo fue realizado en ingles, para esta documentacion considere mas apropiado realizarla en español.

La api utiliza Mapper para el serializado de objetos y SeriLog en consola para excepciones no controladas.

La base de datos se encuentra configurada para crearse localmente al comenzar las operaciones y utiliza el patron Options por lo que puede configurarse en el archivo app.settings.json :

![image](https://github.com/user-attachments/assets/a7c04a0c-44a4-441c-bd0b-b34090f59432)

La solucion del proyecto se encuentra dentro de la carpeta ApiDevBP.

Aclaracion:

Opte por crear un endpoint para consultar el ID de un usuario mediante su nombre y apellido para casos en los que el usuario que realice las peticiones(PUT,DELETE) no conozca este ID.

Considere que el ID es mas eficiente al momento de realizar esta operaciones.

## Endpoints

### Get Users

- **URL:** `/users`
- **Metodo:** `GET`
- **Descripcion:** Devuelve una lista de todos los usuarios.
- **Respuesta:**
  - **200 OK:** Devuelve una lista de todos los usuarios.
    ```json
    [
      {
        "Name": "usuarioNombre1",
        "Lastname": "usuarioApellido1"
      },
      {
        "Name": "usuarioNombre2",
        "Lastname": "usuarioApellido2"
      }
    ]
    
### Get Users

- **URL:** `/users/{name}/{lastname}`
- **Metodo:** `GET`
- **Descripcion:** Devuelve una lista de todos los usuarios que coincidan con la busqueda, junto con sus Id.
- **Respuesta:**
  - **200 OK:** Devuelve una lista de todos los usuarios y sus Id.
    ```json
    [
      {
        "Id": 1,
        "Name": "usuarioNombre1",
        "Lastname": "usuarioApellido1"
      },
      {
        "Id": 2
        "Name": "usuarioNombre2",
        "Lastname": "usuarioApellido2"
      }
    ]

### Create User

- **URL:** `/users`
- **Metodo:** `POST`
- **Descripcion:** Crea un nuevo usuario.
- **Peticion JSON:**
  ```json
  {
    "Name": "usuarioNombre1",
    "Lastname": "UsuarioApellido1"
  }

### Update User

- **URL:** `/users/{id}`
- **Metodo:** `PUT`
- **Descripcion:** Modifica los datos de un usuario existente.
- **Peticion JSON:**
  ```json
  {
    "Name": "usuarioNombre1Modificado",
    "Lastname": "UsuarioApellido1Modificado"
  }

### 2. Delete User

- **URL:** `/users/{id}`
- **Metodo:** `DELETE`
- **Descripcion:** Elimina un usuario existente.
 
