
# BHD Prueba tecnica - Walddry Gonzalez

**Descripción:**

Esta prueba es una API RESTful para gestionar usuarios.

**Tecnologías:**

* .NET 6
* ASP.NET Core
* Entity Framework Core
* SQL Server

### Requisitos
* Tener instalado Microsoft Sql Server, en caso de no tenerlo instalado se puede descargar del siguiente enlace: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

* Tener instalado el SDK de .Net 6, en caso de no tenerlo instalado, se puede descargar del siguiente enlace: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Configurando el proyecto

### Clona el repositorio:
   
```bash
git clone https://github.com/Wal1802/BHD_ApiUser.git
```



### Cambia la cadena de conexión

Antes de ejecutar la aplicación, debes de configurar la cadena de conexión a tu base de datos. Edita el archivo `appsettings.json` y reemplaza los valores de la cadena de conexión con los de tu base de datos.




### Ejecuta las migraciones

Para aplicar las migraciones a la base de datos, ejecuta el siguiente comando en la línea de comandos desde el directorio raíz del proyecto:

```bash
dotnet ef database update 
```

En caso de que el comando de error, debes instalar las tools de entity frameowrk, para ello ejecuta el siguiente comando, y luego procede con el comando anterior para crear la base de datos, de manera opcional puedes ejecutar manualmente el script que se encuentra dentro del proyecto BHD.ApiUser 

```bash
dotnet tool install --global dotnet-ef --version 6.0.35
```

### Pon a correr el proyecto

Para poner a correr el proyecto, ejecuta el siguiente comando en la línea de comandos desde el directorio raíz del proyecto:

```bash
dotnet run
```

### Pruebas con swagger

Para acceder al recurso de crear usuario debes tener un token valido, para generarlo debes utilizar el metodo HTTPPut en el controlador Authentication con los valores por defualt, una vez obtengas un token debes configurarlo de la siguiente manera 

`Bearer TU_TOKEN_AQUI`

Una vez lo configures ya podras acceder a los recursos protegidos, como por ejemplo el HTTPPost del controlador user, con el que podras crear los usuarios.

### Personaliza el Regex para contraseña (Opcional)

Puedes editar el archivo `appsettings.json` y reemplaza los valores del key `Validator:PasswordRegex` con la expresión regular que convenga.

### Poner a correr el proyecto desde visual studio (Opcional)

Abrir la solución, modificar el connectionString, aplicar las migraciones siguiendo los pasos anteriores y luego poner a correr el proyecto de API.




