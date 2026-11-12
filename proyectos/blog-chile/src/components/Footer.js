function Footer() {
  return (
    <>
      <footer className="footer">
        <div className="main">
          <div className="footer__content">
            <h2>RECORRIENDO<span> CHILE</span></h2>
            <h5>En Recorriendo Chile, descubre la belleza única de Chile con nuestros tours personalizados.
              Aventura, cultura y sostenibilidad en un solo viaje. ¡Explora con nosotros!</h5>
            <h5>contacto@recorriendochile.es</h5>
            <h5><span>+56991234780</span></h5>
          </div>
          <div className="footer__socialnetwork">
            <ul>
              <li>
                <a href="#" className="twitter">
                  <i className="fab fa-twitter fa-2x"></i>
                </a>
              </li>
              <li>
                <a href="#" className="instagram">
                  <i className="fab fa-instagram fa-2x"></i>
                </a>
              </li>
              <li>
                <a href="#" className="youtube">
                  <i className="fab fa-youtube fa-2x"></i>
                </a>
              </li>
              <li>
                <a href="#" className="google">
                  <i className="fab fa-google fa-2x"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </footer>
      <div className="footer__bottom">
        <p>
          <span className="text-grey">© </span>
          <span className="text-cyan">2026 | Created with</span>
          <span className="text-red">❤️</span>
          <span className="text-grey"> by Fernando </span>
          <span className="text-cyan">Hernández Lázaro. All rights reserved.</span>
        </p>
      </div>
    </>
  );
}

export default Footer;