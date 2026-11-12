import { Link } from 'react-router-dom';

function Regiones() {
  const regions = [
    {
      id: 'norte',
      image: './img/Desierto 01.webp',
      title: 'NORTE Y DESIERTO',
      description: 'Región desértica y árida, el norte de Chile destaca por sus paisajes, recursos minerales y rica cultura ancestral.'
    },
    {
      id: 'centro',
      image: './img/Valparaiso01.jpg',
      title: 'CENTRO, SANTIAGO Y VALPARAISO',
      description: 'Santiago, capital cosmopolita, contrasta con Valparaíso, ciudad portuaria pintoresca, ambas con encanto cultural, historia vibrante y paisajes distintivos.'
    },
    {
      id: 'rapaNui',
      image: './img/RapaNui01.jpg',
      title: 'RAPA NUI (ISLA DE PASCUA)',
      description: 'Isla mágica y remota, famosa por sus moais monumentales y su misteriosa cultura ancestral que te transportará a otro mundo.'
    },
    {
      id: 'sur',
      image: './img/puertoVaras.jpg',
      title: 'SUR Y LAGOS',
      description: 'Región de lagos cristalinos, volcanes nevados y bosques milenarios. Descubre la magia del sur de Chile.'
    },
    {
      id: 'patagonia',
      image: './img/TorresDelPaine01.jpg',
      title: 'PATAGONIA',
      description: 'El fin del mundo te espera. Glaciares, montañas monumentales y paisajes vírgenes que quitan el aliento.'
    }
  ];

  return (
    <main>
      <div className="card_group">
        {regions.map((region) => (
          <div className="card" key={region.id}>
            <img src={region.image} alt={region.title} />
            <div className="layer"></div>
            <div className="card-info">
              <h1>{region.title}</h1>
              <p className="card-description">{region.description}</p>
              <Link to={`/region/${region.id}`}>
                <button>Explorar</button>
              </Link>
            </div>
          </div>
        ))}
      </div>
    </main>
  );
}

export default Regiones;
