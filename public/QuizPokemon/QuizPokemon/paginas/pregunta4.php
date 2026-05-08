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

<!-- Pregunta 4 con CheckBox -->

<div class="presentacion">
        
  <div class="codigophp">
    <?php
      session_start();
      $nombreJugador ="";

      $respuesta = $_POST['respuestaTres'];
      array_push($_SESSION['respuestas'],$respuesta);

      if(isset($_COOKIE["nombre"])){
      $nombreJugador = $_COOKIE["nombre"];
      }
    ?>

  </div>  
    
  <div class="pres__text">
    <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
    <p>Pregunta 4: </p>
  </div>

  <div class="press_post">
    <form method="post" action="pregunta5.php">
      <label for="preguntaCuatro"><h4>¿Cuál es tu color favorito entre estos?</h4></label><br>
      <input type="checkbox" id="opcion1" name="respuestaCuatro" value="S">
      <label for="opcion1"><span>A.</span> Azul </label><br>
      <input type="checkbox" id="opcion2" name="respuestaCuatro" value="B">
      <label for="opcion2"><span>B.</span> Verde</label><br>
      <input type="checkbox" id="opcion3" name="respuestaCuatro" value="C">
      <label for="opcion3"><span>C.</span> Rojo</label><br>
      <input type="checkbox" id="opcion4" name="respuestaCuatro" value="P">
      <label for="opcion4"><span>D.</span> Amarillo</label><br><br>
      <input type="submit" name="enviarPreguntaCuatro"value="Siguiente"> 
    </form>

  </div>

</div>

</body>
</html>