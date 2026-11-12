function Contacto() {
  return (
    <main>
      <div className="contenedor_contacto">
        <div className="contacto">
          <div className="contacto__vid">
            <video autoPlay loop muted playsInline src={"./img/Video01.mp4"}></video>
          </div>

          <div className="contacto__form">
            <h4>¿Alguna duda? :)</h4>
            <form className="form">
              <input type="text" placeholder="Nombre" />
              <input type="text" placeholder="Apellidos" />
              <input type="email" placeholder="E-mail" />
              <input type="tel" placeholder="Teléfono" />
              <textarea placeholder="Escribe tu consulta!">
              </textarea>
              <button type="button" className="contacto__button">
                ¡Enviar!
              </button>
            </form>
          </div>
        </div>
      </div>
    </main>
  );
}

export default Contacto;
