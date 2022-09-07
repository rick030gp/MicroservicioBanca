# MicroservicioBanca
API desarrollada en .Net Core 5 para administración de clientes, cuentas bancarias y movimientos de cuentas (transacciones) utilizando principios SOLID y arquitectura DDD, además de utilizar patrones de diseño como MVC, Repository, Dependency Injection.

Utilización en docker:
- Cambiar el valor de la key "Banca" en el archivo appsettings.json ubicado en el proyecto MicroservicioBanca.WebApi, la cual debe apuntar a una base de datos que se vaya a utilizar. (No es necesario crear la base ni las tablas, se aplicarán las migraciones al correr el proyecto).
- El proyecto está configurado para despliegue de docker en Linux.
- Ejecutar los siguientes comandos docker desde powerShell en la ruta donde se descargue el proyecto [...ruta]\MicroservicioBanca>
  * docker build -f .\host\MicroservicioBanca.WebApi\Dockerfile -t banca .
  * docker run -it -p 80:80 -d banca
- Listo.!

NOTA: Dentro de este repositorio se encuentra un json (colección de los endpoints disponibles) para importar en Postman y poder utilizar las endpoints.
