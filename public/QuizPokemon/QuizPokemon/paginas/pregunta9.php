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

  <!-- Pregunta 9 con select despegable -->
  
  <div class="presentacion">
          
    <div class="codigophp">
      <?php
        session_start();
        $nombreJugador ="";
        $respuesta = $_POST['respuestaOcho'];
        array_push($_SESSION['respuestas'],$respuesta);
  
  
        if(isset($_COOKIE["nombre"])){
            $nombreJugador = $_COOKIE["nombre"];
        }
      ?>
    </div>  
            
    <div class="pres__text">
      <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
      <p>Pregunta 9: </p>
    </div>
          
    <div class="press_post">
      <form method="post" action="pregunta10.php">
        <label for="preguntaNueve"><h4>¿Qué tipo de película prefieres?</h4></label>
        <select name="respuestaNueve" id="pelicula">
        <option value="B"><span>A.</span> Documentales o romances</option>
        <option value="C"><span>B.</span> Acción y aventura</option>
        <option value="S"><span>C.</span> Drama y suspenso</option>
        <option value="P"><span>D.</span> Ciencia ficción o películas de superhéroes</option>
        </select>
        <input type="submit" value="Siguiente"> 
      </form>
  
    </div>
                    
  </div>

</body>
</html>