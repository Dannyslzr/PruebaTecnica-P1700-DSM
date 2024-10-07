Elaborado por Danny Salazar Méndez
Proyecto de prueba técnica para Grupo Prides.

**Arrancar solución.**
La solución cuenta con 2 proyectos que debe de arrancar cuando se vaya a ejecutar.
![image](https://github.com/user-attachments/assets/7e1ff079-52e4-41c2-aecc-46d921497db9)
 
Por lo cual se debe de configurar el arranque de ambos, para ello debe de dar click derecho sobre el nombre de la solución, click en propiedades.
 ![image](https://github.com/user-attachments/assets/ace007c6-94b1-4e6e-ae14-44f184c3b78b)

Acá deberá activar el arranque de múltiples proyectos. 
![image](https://github.com/user-attachments/assets/8083f221-9a1a-4c67-9e55-476894dee6ef)

**Configurar cadena de conexión:**
La cadena de conexión a la base de datos se encontrara en el archivo “appsettings.json”, del proyecto “Api”.
![image](https://github.com/user-attachments/assets/1f03e56e-ecd3-4e82-b1ba-d0a84c5116ef)


Debe de configurar los datos de la cadena de conexión a su ambiente.
![image](https://github.com/user-attachments/assets/de386a5d-829b-47b9-90a7-41e8d78b5b31)


**URL de arranque del Api:**
He configurado la URL del Api para que se ejecute en “https://localhost:7788”, esto desde el archivo “Program.cs”, línea 28 del proyecto “Api”.
En dado caso que otra aplicación este utilizando el url/puerto, puede eliminar esta línea.

Pero deberá de cambiar el URL que le haya cargado en el archivo “appsetting.json” del proyecto “Web”.
![image](https://github.com/user-attachments/assets/824c5a46-2933-458f-a869-b29679656b48)
![image](https://github.com/user-attachments/assets/5515848d-15fb-4035-bf1d-ce7942a897c9)


**Datos de inicio de sesión en la Web.**

Correo: danny_slzr@gmail.com
Contraseña: 123
De igual manera podrá crear usuarios en la app.
