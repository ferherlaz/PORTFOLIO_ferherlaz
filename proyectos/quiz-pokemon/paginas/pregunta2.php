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

  <div class="presentacion">

    <!-- Pregunta 2 con  RadioButton-->
          
    <div class="codigophp">
      <?php
        session_start();
        
        $nombreJugador ="";

        $respuesta = $_POST['respuestaUno'];
        array_push($_SESSION['respuestas'],$respuesta); //añado la alternativa al array
      

        if(isset($_COOKIE["nombre"])){
        $nombreJugador = $_COOKIE["nombre"];
        }
      ?>

    </div>  
      
    <div class="pres__text">
      <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
      <p>Pregunta 2: </p>
    </div>

    <div class="press_post">
      <form method="post" action="pregunta3.php">
        <label for="preguntaDos"><h4>¿Cómo describirías tu personalidad?</h4></label><br>
        <input type="radio" id="opcion1" name="respuestaDos" value="C">
        <label for="opcion1"><span>A.</span> Apasionado y enérgico </label><br>
        <input type="radio" id="opcion2" name="respuestaDos" value="S">
        <label for="opcion2"><span>B.</span> Tranquilo y adaptable</label><br>
        <input type="radio" id="opcion3" name="respuestaDos" value="B">
        <label for="opcion3"><span>C.</span> Pacífico y conectado con la naturaleza</label><br>
        <input type="radio" id="opcion4" name="respuestaDos" value="P">
        <label for="opcion4"><span>D.</span> Activo y rápido</label><br><br>
        <input type="submit" value="Siguiente"> 
      </form>
    </div>

  </div>

</body>
</html>