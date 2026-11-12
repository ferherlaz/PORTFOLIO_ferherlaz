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

  <!-- Pregunta 5 con textArea -->

  <div class="presentacion">
        
    <div class="codigophp">
      <?php
      session_start();
      $nombreJugador ="";

      $respuesta = $_POST['respuestaCuatro'];
      array_push($_SESSION['respuestas'],$respuesta);


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
      <form method="post" action="pregunta6.php">
        <label for="preguntaCinco"><h4>¿Qué habilidad te gustaría tener?</h4></label><br>            
        <label for="opcion1"><span>A.</span> Generar electricidad </label><br>
        <label for="opcion2"><span>B.</span> Manipular el agua</label><br>
        <label for="opcion3"><span>C.</span> Controlar el fuego</label><br>
        <label for="opcion4"><span>D.</span> Comunicarte con la naturaleza</label><br><br>
        <textarea name="respuestaCinco" id="respuestaEscogida"></textarea>
        <input type="submit" value="Siguiente"> 
      </form>

    </div>

  </div>

</body>
</html>