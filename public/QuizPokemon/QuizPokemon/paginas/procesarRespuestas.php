<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="../css/style.css">
</head>
<body>

    <div class="presentacion">
        <div class="pres__text">
        <h2>Resultado</h2>
        </div>

        <div class="press_post">
        
            <?php

                // pruebas  
                session_start();
                $nombreJugador ="";
                $respuesta = $_POST['respuestaDiez'];
                array_push($_SESSION['respuestas'],$respuesta);
             

                $arrayRespuestas = $_SESSION['respuestas'];


                // print_r($arrayRespuestas); /para verificar si el array se me completaba con la alternativa o no


                //Crear un arreglo para contabilizar la frecuencia de cada letra
                $frecuenciaLetras = array( 
                    'C' => 0,
                    'P' => 0,
                    'S' => 0,
                    'B' => 0
                );


                for($i=0; $i <10; $i++ ){
                    $res = $arrayRespuestas[$i];
                    $frecuenciaLetras[$res]++; 

                }

                // print_r($frecuenciaLetras); me imprime cuantas letras se repiten


                // Encontrar la letra que más se repite
                $letraMasRepetida = ''; //variable
                $maxRepeticiones = 0;//variable para ver cual letra es la que mas se repite

                foreach ($frecuenciaLetras as $letra => $repeticiones) { 
                    if ($repeticiones > $maxRepeticiones) { //aqui yo verifico la letra actual y el num de repeticiones actuales, asi yo logro ver cual esta siendo mas repetidas y cuantas veces
                        $letraMasRepetida = $letra;
                        $maxRepeticiones = $repeticiones;
                    }
                }

                if(isset($_COOKIE["nombre"])){
                    $nombreJugador = $_COOKIE["nombre"];
                }

                switch ($letraMasRepetida) {
                    case "C":
                        echo $nombreJugador . ", eres Charmander";
                        echo "<img src='../img/CG.gif' alt=''>";
                        break;
                    case "P":
                        echo $nombreJugador . ",eres Pikachu";
                        echo "<img src='../img/PG.gif' alt=''>";
                        break;
                    case "S":
                        echo $nombreJugador . ",eres Squirtle";
                        echo "<img src='../img/SG.gif' alt=''>";
                        break;
                    case "B":
                        echo $nombreJugador . ",eres Bulbasaur";
                        echo "<img src='../img/BG.gif' alt=''>";
                        break;
                    default:
                        echo "Vuelve a responder";
                }




                session_unset();
                session_destroy();


            ?>
        </div>

    </div>
</body>
</html>