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


  <!-- Pregunta 10 con select despegable -->
  
  <div class="presentacion">
          
    <div class="codigophp">
      <?php
        session_start();
        $nombreJugador ="";
        $respuesta = $_POST['respuestaNueve'];
        array_push($_SESSION['respuestas'],$respuesta);

        if(isset($_COOKIE["nombre"])){
            $nombreJugador = $_COOKIE["nombre"];
        }
      ?>
    </div>  
            
    <div class="pres__text">
      <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
      <p>Pregunta 10: </p>
    </div>
          
    <div class="press_post">
      <form method="post" action="procesarRespuestas.php">
        <label for="preguntaNueve"><h4>¿Cuál es tu meta principal en la vida?</h4></label>
        <select name="respuestaDiez" id="vida">
        <option value="C"><span>A.</span> Destacar y ser reconocido</option>
        <option value="S"><span>B.</span> Encontrar paz y equilibrio</option>
        <option value="B"><span>C.</span> Vivir en armonía con la naturaleza</option>
        <option value="P"><span>D.</span> Experimentar la emoción y la acción constante</option>
        </select>
        <input type="submit" value="Siguiente"> 
      </form>
  
    </div>
                    
  </div>

</body>
</html>