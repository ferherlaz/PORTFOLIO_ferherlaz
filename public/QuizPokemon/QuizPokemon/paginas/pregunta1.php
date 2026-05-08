<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="../css/style.css">
</head>

<!-- Pokemon asociado a elemento

Fuego -- Charmander
Planta -- Bulbasaur
Agua -- Squirtle
Electricidad --Pikachu
-->

<body>

    <!-- Pregunta 1 con  RadioButton-->

    <div class="presentacion">
        
        <div class="codigophp">
            <?php
                session_start();

                $nombreJugador ="";
                // Inicializar un array vacío para almacenar las respuestas del usuario si aún no existe
                if (!isset($_SESSION['respuestas'])) {
                $_SESSION['respuestas'] = array();
                
                }

                if(isset($_COOKIE["nombre"])){
                $nombreJugador = $_COOKIE["nombre"];
                }
            ?>

        </div>  
    
        <div class="pres__text">
            <h2>¡Bienvenid@ <?php echo $nombreJugador?>, al PHPQuiz!</h2>
            <p>Por favor, responde las siguientes preguntas:</p>
        </div>

        <div class="press_post">
            <form method="post" action="pregunta2.php">
                <label for="preguntaUno"><h4>"¿Qué elemento crees que te define entre fuego, agua, planta y eléctrico?"</h4></label><br>
                <input type="radio" id="opcion1" name="respuestaUno" value="C">
                <label for="opcion1"><span>A.</span> Fuego</label><br>
                <input type="radio" id="opcion2" name="respuestaUno" value="P">
                <label for="opcion2"><span>B.</span> Eléctrico</label><br>
                <input type="radio" id="opcion3" name="respuestaUno" value="S">
                <label for="opcion3"><span>C.</span> Agua</label><br>
                <input type="radio" id="opcion4" name="respuestaUno" value="B">
                <label for="opcion4"><span>D.</span> Planta</label><br><br>
                <input type="submit" name="enviarPreguntaUno"value="Siguiente"> 
            </form>
        </div>

    </div>

</body>

</html>