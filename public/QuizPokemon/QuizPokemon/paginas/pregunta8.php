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

  <!-- Pregunta 8 con button -->
  
  <div class="presentacion">
        
    <div class="codigophp">
      <?php
        session_start();
        
        $nombreJugador ="";
  
        $respuesta = $_POST['respuestaSiete'];
        array_push($_SESSION['respuestas'],$respuesta);
  
        if(isset($_COOKIE["nombre"])){
            $nombreJugador = $_COOKIE["nombre"];
        }
      ?>
    </div>  
      
    <div class="pres__text">
      <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
      <p>Pregunta 8: </p>
    </div>
    
    <div class="press_post">
      <form method="post" action="pregunta9.php">
        <label for="preguntaOcho"><h4>¿Cómo te relacionas con los demás?</h4></label><br>
        <input type="radio" id="opcion1" name="respuestaOcho" value="C">
        <label for="opcion1"><span>A.</span> Con pasión y entusiasmo </label><br>
        <input type="radio" id="opcion2" name="respuestaOcho" value="S">
        <label for="opcion2"><span>B.</span> De manera relajada y amigable</label><br>
        <input type="radio" id="opcion3" name="respuestaOcho" value="B">
        <label for="opcion3"><span>C.</span> De forma calmada y compasiva</label><br>
        <input type="radio" id="opcion4" name="respuestaOcho" value="P">
        <label for="opcion4"><span>D.</span> Siempre con energía y vitalidad</label><br><br>
        <input type="submit" value="Siguiente">  
      </form>
    </div>
        
  </div>

</body>
</html>      