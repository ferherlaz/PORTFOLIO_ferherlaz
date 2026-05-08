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

<!-- Pregunta 6 con textArea -->

<div class="presentacion">
        
  <div class="codigophp">
    <?php
      session_start();
      
      $nombreJugador ="";

      $respuesta = $_POST['respuestaCinco'];
      $respuestaCamelCase = strtoupper($respuesta);//strtoupper convierte todos los caracteres de la cadena a Mayus

      $respuestaFinal;

    switch ($respuestaCamelCase) {
      case "A":
        $respuestaFinal = 'P';
        break;

      case "B":
        $respuestaFinal = 'S';
        break;

      case "C":
        $respuestaFinal = 'C';
        break;

      case "D":
        $respuestaFinal = 'B';
        break;
      }

    array_push($_SESSION['respuestas'],$respuestaFinal);

    if(isset($_COOKIE["nombre"])){
        $nombreJugador = $_COOKIE["nombre"];
    }
    ?>
  </div>  
  
  <div class="pres__text">
    <h2>¡Continuemos <?php echo $nombreJugador?>, en el PHPQuiz!</h2>
    <p>Pregunta 6: </p>
  </div>

  <div class="press_post">
    <form method="post" action="pregunta7.php">
      <label for="preguntaSeis"><h4>¿Cuál es tu comida favorita?</h4></label><br>
      <label for="opcion1"><span>A.</span> Asados y barbacoas </label><br>
      <label for="opcion2"><span>B.</span> Mariscos y sushi</label><br>
      <label for="opcion3"><span>C.</span> Ensaladas y vegetales</label><br>
      <label for="opcion4"><span>D.</span> Comida picante y extranjera</label><br><br>
      <textarea name="respuestaSeis"  id="respuestaEscogida"></textarea>
      <input type="submit" value="Siguiente"> 
    </form>

  </div>

</div>

</body>
</html>
