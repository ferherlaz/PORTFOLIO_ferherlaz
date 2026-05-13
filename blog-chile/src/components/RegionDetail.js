import { useParams } from 'react-router-dom';

const regionData = {
  norte: {
    title: 'NORTE Y DESIERTO DE ATACAMA',
    intro: `Lejos de ser inerte, el Desierto de Atacama es una zona abundante en energía, gracias
      a sus acogedoras culturas ancestrales y a los oasis que son fuente de vida en el desierto
      mas árido del mundo. Asciende los más de 4.000 metros de altura del altiplano andino para descubrir pequeños
      poblados que mantienen intactas sus costumbres en medio de extremos escenarios que vale la
      pena fotografiar.
      <br><br>
      Visita San Pedro de Atacama y explora paisajes únicos en el mundo. Salares, géiseres y
      lagunas de un intenso color azul, son parte de los lugares que te asombraraán.
      Emprende un viaje por las estrellas. Aprende más sobre los astros y ls constelaciones en uno de los
      múltiples observatorios astronómicos de la zona norte del país.`,
    places: [
      {
        name: 'SAN PEDRO DE ATACAMA',
        image: './img/SanPedroAtacana.jpg',
        description: `Maravilloso, extravagante, completamente libre... así son los paisajes de la zona más árida del platena,
          donde abundan los salares, las formaciones rocosas cubiertas de arena, pueblos andinos, volcanes y lagunas cristalinas.
          <br><br>
          Para quienes buscan la aventura, se recomienda hacer sandboard en el Valle de la Luna, mountain bike por
          la Garganta del Diablo, ascensiones a volcanes y conocer EL Medanoso, la duna más alta
          de Atacama. Pero si buscas un tour panorámico te recomendamos visitar el Salar de Tara, Lagunas Altiplánicas
          y el Valle de la Luna en San Pedro de Atacama con una copa de vino tinto.
          <br><br>
          No te puedes perder los famosos Geiser del Tatio, uno de los campos heométricos más altos del mundo
          cuya fumorales superan los 7 metros de altura, y las Termas de Puritama, un
          paisaje inolvidable en el Desierto.`
      },
      {
        name: 'VALLE DEL ELQUI',
        image: './img/valle-del-elqui.jpg',
        description: `El Valle del Elquí es un destino mágico conocido por sus paisajes naturales,
          su cielo despejado perfecto para la observación astronómica y su rica herencia cultural.
          <br><br>
          Aquí puedes visitar el observatorio Mamalluca y otros centros astronómicos que ofrecen
          experiencias únicas bajo uno de los cielos más claros del mundo. El valle también es famoso
          por sus centros de espiritualidad y retiros de meditación.
          <br><br>
          No puedes dejar de visitar las oficinas de turismo en Vicuña y explorar las antiguas
          dependencias de la mina de oro que hoy son un museo al aire libre.`
      }
    ]
  },
  centro: {
    title: 'CENTRO, SANTIAGO Y VALPARAISO',
    intro: `Santiago, la capital de Chile, es una metrópolis moderna rodeada de montañas de los Andes.
      Ofrece una mezcla única de tradición y modernidad, con barrios históricos, gastronómicos y culturales.
      <br><br>
      Valparaíso, declarada Patrimonio de la Humanidad por la UNESCO, es una ciudad portuaria
      única conocida por sus cerros coloridos, sus ascensores históricos y su vibrante escena artística.
      El contraste entre ambas ciudades ofrece una experiencia turística enriquecedora.`,
    places: [
      {
        name: 'SANTIAGO',
        image: './img/santiago.jpg',
        description: `Santiago es el centro político, económico y cultural de Chile. Puedes visitar la
          Plaza de Armas, el Palacio de La Moneda, el Cerro San Cristóbal y la Cerro Santa Lucía.
          <br><br>
          La ciudad ofrece una amplia gastronomía con platos típicos como el pastel de choclo,
          la cazuela y el asado chileno. No puedes dejar de probar los vinos chilenos en alguna
          de las viñas cercanas a la ciudad.
          <br><br>
          Para los amantes de la historia, el Museo Nacional de Historia Natural y el Museo
          de la Memoria y los Derechos Humanos son visitas obligadas.`
      },
      {
        name: 'VALPARAÍSO',
        image: './img/valparaiso.jpg',
        description: `Valparaíso es una ciudad única en el mundo. Sus cerros cubiertos de casas colorful,
          sus escaleras mecánicas y sus ascensores históricos la convierten en un destino turístico imperdible.
          <br><br>
          El Cerro Alegre y el Cerro Concepción son los más turísticos, con galerías de arte,
          restaurantes y cafés con vistas spectacular. ElASCENSOR es un paseo obligatorio para
          subir a los cerros y disfrutar de vistas panorámicas de la bahía.
          <br><br>
          No puedes dejar de visitar La Sebastiana, la casa del premio Nobel Pablo Neruda, hoy
          convertida en museo.`
      }
    ]
  },
  rapaNui: {
    title: 'RAPA NUI (ISLA DE PASCUA)',
    intro: `Rapa Nui, más conocida como Isla de Pascua, es uno de los lugares más misteriosos y fascinantes
      del mundo. Ubicada en medio del Océano Pacífico, esta isla chilena es famosa por sus monumentales
      moais, figuras de piedra talladas por los antiguos habitantes polinesios.
      <br><br>
      La isla ofrece una combinación única de historia, cultura y naturaleza. Sus playas de arena blanca,
      sus volcanes extintos y sus paisajes vírgenes la convierten en un destino soñado para cualquier viajero.`,
    places: [
      {
        name: 'RANO RARAKU',
        image: './img/ranoKkau.jpg',
        description: `Rano Raraku es el crater de un volcano extinto que served como cantera principal
          para la construcción de los moais. Aquí puedes ver cientos de figuras en diferentes estados
          de elaboración, algunas aún parcialmente enterradas.
          <br><br>
          El paisaje lunar del crater, con su lago de agua dulce y las figuras de piedra desperdigadas,
          es una de las imágenes más emblemáticas de la isla. La mayoría de los moais fueron tallados
          aquí antes de ser transportados a través de la isla.`
      },
      {
        name: 'HANGAROA',
        image: './img/hangaRoa.jpg',
        description: `Hanga Roa es la capital y único pueblo de la Isla de Pascua. Aquí vive la mayor parte
          de la población y encontrarás la mayoría de los servicios turísticos, restaurantes y hoteles.
          <br><br>
          La ciudad tiene un ambiente tranquilo y acogedor, con influencias tanto chilenas como polinesias.
          No puedes dejar de visitar la iglesia católica construida con basalto volcánico y los numerosos
          restorantes que ofrecen gastronomía local basada en pescado fresco y productos locales.`
      },
      {
        name: 'PLAYA ANAKENA',
        image: './img/playaAnakena.jfif',
        description: `Playa Anakena es la playa más famosa de la Isla de Pascua, con arena blanca y palmeras
          que crean un paisaje tropical único en Chile.
          <br><br>
          Además de su belleza natural, la playa cuenta con varios moais restaurados en la orilla,
          incluyendo el famoso Ahu Ature Hiva. Es el lugar perfecto para nadar y relajarse después
          de un día de exploración.
          <br><br>
          La playa tiene servicios de estacionamiento, restaurantes y tiendas de recuerdos. Es
          recomendable llevar protector solar, ya que el sol es muy intenso durante todo el año.`
      }
    ]
  },
  sur: {
    title: 'SUR, LAGOS Y VOLCANES',
    intro: `La lluvia cae intensa para luego dar paso a un cielo azul intenso. Huele a bosque y a tierra húmeda.
      La gente es cálida y acogedora. ¡Bienvenido al sur de Chile! Descubre su belleza escénica y la herencia cultural de sus pueblos originarios.
      En esta zona la cultura mapuche es protagonista. ¡Comparte sus tradiciones durmiendo en una ruca o probando los platos típicos de la zona!
      <br><br>
      Si te gusta la nieve esquía en medio de bosques milenarios y aprovecha de relajar tu cuerpo en alguno de los centros termales que se ubican
      en medio de inspiradores paisajes naturales.
      También puedes practicar deportes náuticos en lagos y ríos, disfrutar de un día de pesca o explorar la variada flora y fauna en las reservas y parques
      nacionales.`,
    places: [
      {
        name: 'SALTO DEL LAJA',
        image: './img/salto del laja.jpg',
        description: `Saltos del Laja es reconocido por ser la puerta de entrada al turismo del sur del país. En total, posee cuatro saltos de agua,
          siendo el salto principal, que se ve desde la carretera y tiene una altura de 35 metros, es la postal típica de la región del Biobío
          y el más conocido. Además es uno de los íconos turísticos indiscutidos del país.
          <br><br>
          Está ubicado a 1 hora y 10 minutos desde Concepción y a 33 minutos desde Los Ángeles, la capital regional de la provincia del
          Biobío. En cuanto a gastronomía, no puedes dejar de probar sus platos típicos con sabrosos ingredientes como la trucha, ciervos
          y cervezas artesanales de la zona.
          <br><br>
          Es posible realizar paseos náuticos por los afluentes del río bajo la cascada, además de canopy, trekking y cabalgatas. Es un
          destino que es posible visitarlo durante todo el año.`
      },
      {
        name: 'PUERTO VARAS',
        image: './img/puertoVaras.jpg',
        description: `Puerto Varas, conocida como la "Ciudad de las Rosas", es una hermosa ciudad lacustre ubicada a orillas del Lago Llanquihue.
          Su arquitectura de estilo alemán, heredada de los inmigrantes europeos del siglo XIX, le da un encanto especial.
          <br><br>
          Desde aquí puedes visitar el Volcán Osorno, que ofrece vistas spectulares desde sus laderas.
          La ciudad también es conocida por su gastronomía basada en salmón, trucha y otros productos del mar.
          <br><br>
          No puedes dejar de visitar el Museo de la Cultura Werner Kraus y explorar la Costanera, donde encontrarás restaurantes,
          cafés y tiendas locales con vistas al lago y al volcán.`
      },
      {
        name: 'ARCHIPIÉLAGO DE CHILOÉ',
        image: './img/chiloe.jpg',
        description: `El Archipiélago de Chiloé es un lugar mágico declarado Patrimonio de la Humanidad por la UNESCO.
          Conocido por sus tradiciones únicas, sus iglesias de madera y su mito del Caleuche, la isla ofrece una experiencia cultural inigualable.
          <br><br>
          Las iglesias de Chiloé, construidas enteramente en madera, son ejemplos destacados de la arquitectura colonial religiosa.
          Dieciséis de ellas son Patrimonio de la Humanidad.
          <br><br>
          No puedes dejar de probar el curanto, un plato tradicional preparado en hoyos excavados en la tierra,
          y visitar el pueblo de Castro, capital de Chiloé, con sus iconiccas casas sobre palafitos.`
      }
    ]
  },
  patagonia: {
    title: 'PATAGONIA CHILENA',
    intro: `La Patagonia Chilena es uno de los lugares más salvajes y spectaculares del planeta.
      Glaciares monumentales, montañas que tocan el cielo, lagos de color turquesa y una naturaleza vírgen
      hacen de esta región un destino soñado para aventureros y amantes de la naturaleza.
      <br><br>
      Aquí encontrarás el Parque Nacional Torres del Paine, uno de los más spectaculares del mundo,
      además de glaciares como el Grey, el Jorge Montt y el Serrano. La región ofrece trekking, navegación
      por glaciares, avistamiento de fauna y experiencias únicas en contacto con la naturaleza.`,
    places: [
      {
        name: 'TORRES DEL PAINE',
        image: './img/torresPaine.jpg',
        description: `El Parque Nacional Torres del Paine es uno de los parques nacionales más spectaculares del mundo.
          Sus tres icónicas torres de granito, que se alzan a más de 2.500 metros sobre el nivel del mar, son el símbolo de la Patagonia.
          <br><br>
          El parque ofrece numerosas rutas de trekking, desde caminatas suaves hasta expediciones de varios días.
          El Circuit Trek es una ruta de aproximadamente 8-10 días que rodea completamente el macizo del Paine.
          <br><br>
          Además de las torres, puedes ver el Glacier Grey, el Valle del Frances y la fauna característica como guanacos,
          condores y zorros. Es recomendable planificar con anticipación y reservar campamentos y refugios.`
      },
      {
        name: 'CARRETERA AUSTRAL',
        image: './img/carreteraAustral.png',
        description: `La Carretera Austral es una de las rutas más espectaculares del mundo, atravesando más de 1.200 kilómetros
          de paisajes vírgenes desde Puerto Montt hasta Villa O'Higgins.
          <br><br>
          Esta ruta te llevará por fiordos, glaciares, bosques templados y montañas impresionante. Puedes hacer paradas
          en lugares como el Parque Nacional Queulat, el Glacier Ventisquero y las termas de Puyuhuapi.
          <br><br>
          Es una ruta perfecta para quienes buscan aventura y naturaleza en estado puro. Muchos tramos son de ripio,
          por lo que se recomienda vehículos con tracción integral. Es necesario planificar el viaje con anticipación.`
      },
      {
        name: 'ANTÁRTICA CHILENA',
        image: './img/antartica.jpg',
        description: `La Antártica Chilena representa el territorio chileno en el continente blanco. Aquí encontrarás
          bases científicas, glaciares colossales y una biodiversidad única adaptada a las condiciones más extremas del planeta.
          <br><br>
          Desde Punta Arenas puedes realizar expediciones a la Antártica, que incluyen navegación por el Estrecho de Drake,
          desembarco en bases científicas y avistamiento de pingüinos, focas y ballenas.
          <br><br>
          Esta experiencia única te permitirá conocer uno de los lugares más remotos y preservados del planeta,
          donde la naturaleza gobernAbsolute es la protagonista absoluta.`
      }
    ]
  }
};

function RegionDetail() {
  const { id } = useParams();
  const region = regionData[id];

  if (!region) {
    return (
      <main>
        <div className="contenido">
          <div className="introduccion">
            <div className="intro__title">
              <h2>Región no encontrada</h2>
            </div>
          </div>
        </div>
      </main>
    );
  }

  return (
    <main>
      <div className="contenido">
        <div className="introduccion">
          <div className="intro__title">
            <h2>{region.title}</h2>
          </div>
          <div className="intro__info">
            <p dangerouslySetInnerHTML={{ __html: region.intro }} />
          </div>
        </div>

        {region.places.map((place, index) => (
          <div className="descripcion" key={index}>
            <div className="desc__img">
              <img src={place.image} alt={place.name} />
              <div className="desc__nombre">
                {place.name}
              </div>
            </div>

            <div className="desc__info">
              <p dangerouslySetInnerHTML={{ __html: place.description }} />
            </div>
          </div>
        ))}
      </div>
    </main>
  );
}

export default RegionDetail;
