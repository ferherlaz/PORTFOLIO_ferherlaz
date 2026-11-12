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

  <!-- Pregunta 7 con buttoN RADIO  -->

  <div class="presentacion">
        
    <div class="codigophp">
      <?php
        session_start();
        
        $nombreJugador ="";

        $respuesta = $_POST['respuestaSeis'];
        $respuestaCamelCase = strtoupper($respuesta);

        $respuestaFinal;

        switch($respuestaCamelCase) {
          case "A":
            $respuestaFinal = 'C';
            break;

          case "B":
            $respuestaFinal = 'S';
            break;

          case "C":
            $respuestaFinal = 'B';
            break;

          case "D":
            $respuestaFinal = 'P';
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
      <p>Pregunta 7: </p>
    </div>
    
    <div class="press_post">
      <form method="post" action="pregunta8.php">
        <label for="preguntaSiete"><h4>¿Cómo reaccionas bajo presión?</h4></label><br>
        <input type="radio" id="opcion1" name="respuestaSiete" value="P">
        <label for="opcion1"><span>A.</span> Actuando rápidamente e impulsivamente</label><br>
        <input type="radio" id="opcion2" name="respuestaSiete" value="S">
        <label for="opcion2"><span>B.</span> Manteniendo la calma y analizando la situación</label><br>
        <input type="radio" id="opcion3" name="respuestaSiete" value="C">
        <label for="opcion3"><span>C.</span> Con determinación y valentía</label><br>
        <input type="radio" id="opcion4" name="respuestaSiete" value="B">
        <label for="opcion4"><span>D.</span> Buscando soluciones pacíficas</label><br><br>
        <input type="submit" value="Siguiente"> 
      </form>

    </div>

  </div>
  
  </body>
  </html>


