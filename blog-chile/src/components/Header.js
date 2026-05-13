import { Link, useLocation } from 'react-router-dom';

function Header() {
  const location = useLocation();
  const isActive = (path) => location.pathname === path ? 'active' : '';

  return (
    <header>
      <nav className="contenedor_nav">
        <div className="logo">
          <Link to="/" className="boton_navegacion">
            <img src={"./img/LogoChile.jpg"} alt="Icono Navegación" />
          </Link>
        </div>
        <ul className="menu">
          <li><Link to="/" className={isActive('/')}>SOBRE CHILE</Link></li>
          <li><Link to="/regiones" className={isActive('/regiones')}>REGIONES</Link></li>
          <li><Link to="/contacto" className={isActive('/contacto')}>CONTACTO</Link></li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
