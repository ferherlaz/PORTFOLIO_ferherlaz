function SobreChile() {
  return (
    <main>
      <div className="contenedor_info">
        <div className="info">
          <div className="info__content">
            <div className="info__title">
              <h2>¡BIENVENID@S!</h2>
            </div>
            <div className="info__description">
              <p>¡Bienvenido a Chile, la tierra de contrastes y maravillas!
                Ubicado en el extremo suroeste de América del Sur,
                Chile es un país que cautiva con su impresionante geografía
                que va desde la aridez del desierto de Atacama hasta los
                picos nevados de la cordillera de los Andes y los exuberantes bosques del sur.
                Descubre la magia de Chile, donde la naturaleza, la historia y la hospitalidad
                se fusionan para crear un destino fascinante.
                ¡Prepárate para sumergirte en la diversidad de Chile y explorar sus encantos inigualables!
              </p>
            </div>
          </div>
          <div className="info__img">
            <img src={"./img/Chile01.jpg"} alt="" />
          </div>
        </div>

        <div className="info">
          <div className="info__img">
            <img src={"./img/Moai02.jpg"} alt="" />
          </div>
          <div className="info__content">
            <div className="info__title">
              <h2>¿QUÉ VAS A ENCONTRAR AQUI?</h2>
            </div>
            <div className="info__description">
              <p>Sumérgete en un viaje virtual a través de las diversas zonas del país,
                comenzando en el norte y el desierto de Atacama, donde la aridez se
                encuentra con paisajes surrealistas. Luego, exploraremos el animado centro
                del país, con un enfoque especial en la bulliciosa metrópolis de Santiago y
                la pintoresca Valparaíso. Nos aventuraremos hasta Rapa Nui, la misteriosa isla
                de Pascua, antes de sumergirnos en los fascinantes lagos y volcanes del sur.
                Descubriremos la majestuosidad de la Patagonia, con sus glaciares y paisajes vírgenes,
                antes de llegar a la asombrosa Antártica. Únete a nosotros para una experiencia única,
                donde cada región de Chile cuenta su propia historia cautivadora.</p>
            </div>
          </div>
        </div>

        <div className="info">
          <div className="info__content">
            <div className="info__title">
              <h2>¿QUÉ DOCUMENTOS NECESITO?</h2>
            </div>
            <div className="info__description">
              <p>Para visitar Chile solo tienes que presentar tu documento de identificación (cédula o
                pasaporte), y dependiendo de tu país de origen, tu visa estampada. Si eres un ciudadano
                sudamericano, de la Unión Europea, Estados Unidos, Canadá y Australia no necesitas visa como
                turista. Sin embargo, algunos países deben pagar una tasa en efectivo (impuesto de reciprocidad)
                al llegar al aeropuerto de Chile.</p>
            </div>
          </div>
          <div className="info__img">
            <img src={"./img/Pasaporte.jpg"} alt="" />
          </div>
        </div>

        <div className="info">
          <div className="info__img">
            <img src={"./img/chile_te_cuida.jpg"} alt="" />
          </div>
          <div className="info__content">
            <div className="info__title">
              <h2>CHILE TE CUIDA :)</h2>
            </div>
            <div className="info__description">
              <p>Recomendaciones para un buen viaje por Chile:
                - Reserva servicios formales y oficiales. Busca en el Registro Nacional de Prestadores de Servicios Turísticos.
                - Para conocer el costo de su viaje en taxis oficiales y habilitados descarga la App VotChile.
                - Asegúrate de salir con lo preciso y en caso de tener objetos de valor, guárdalos en tu alojamiento.
                - Cambia dinero en casas de cambio formales o en bancos; nunca en las calles. Infórmate del valor previamente.
                - En caso de ser víctima de una emergencia o delito, contacta a Carabineros al 133 o acude a la estación de policía más cercana y/o contacta a tu Consulado en Chile.
              </p>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}

export default SobreChile;
