<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>PHPQUIZ</title>
    <link rel="icon" type="image/png" href="./img/pokeball.png">
    <link rel="stylesheet" href="./css/style.css">
</head>
<body>
    
    <div class="contenido">

        <div class="presentacion">
            <div class="pres__text">
                <h2>¡Bienvenid@!, ahora descubriras que tipo de pokemon eres!</h2>
                <p>Ingresa tu nombre para comenzar :)</p>
            </div>

            <div class="press_post">
                <form method="post">
                    <label for="nombre">Nombre:</label><br>
                    <input type="text" id="nombre" name="nombre" required><br><br>
                    <input type="submit" value="Comenzar">
                </form>
            </div>
        </div>

        <div class="codigophp">
            <?php

                session_start(); //esto permite que paginas distintas puedan acceder a una variable en comun

                // Verificar si se ha enviado el nombre del usuario
                if(isset($_POST['nombre'])){
                // Guardar el nombre en una cookie
                setcookie("nombre", $_POST['nombre'], time() + 3600, "/"); 

                // Redirigir al usuario a la página del quiz
                header("Location:paginas/pregunta1.php");
                exit;
                }

            ?>

        </div>

    </div>    

</body>
</html>