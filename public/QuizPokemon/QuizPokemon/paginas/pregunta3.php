<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="../css/style.css">
</head>
<body>

<!-- Pregunta 3 con CheckBox -->

<div class="presentacion">
        
    <div class="codigophp">
        <?php
            session_start();
            $nombreJugador ="";

            $respuesta = $_POST['respuestaDos'];
            array_push($_SESSION['respuestas'],$respuesta);

            if(isset($_COOKIE["nombre"])){
            $nombreJugador = $_COOKIE["nombre"];
            }
        ?>

    </div>  
    
    <div class="pres__text">
        <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
        <p>Pregunta 3: </p>
    </div>

    <div class="press_post">
        <form  method="post" action="pregunta4.php">
            <label for="preguntaTres"><h4>¿Cuál es tu entorno natural ideal?</h4></label><br>
            <input type="checkbox" id="opcion1" name="respuestaTres" value="P">
            <label for="opcion1"><span>A.</span> Áreas con interacción tecnológica </label><br>
            <input type="checkbox" id="opcion2" name="respuestaTres" value="B">
            <label for="opcion2"><span>B.</span> Bosques y jardines</label><br>
            <input type="checkbox" id="opcion3" name="respuestaTres" value="S">
            <label for="opcion3"><span>C.</span> Espacios acuáticos</label><br>
            <input type="checkbox" id="opcion4" name="respuestaTres" value="C">
            <label for="opcion4"><span>D.</span> Lugares cálidos y soleados</label><br><br>
            <input type="submit" value="Siguiente"> 
        </form>

    </div>

</div>
    
</body>
</html>