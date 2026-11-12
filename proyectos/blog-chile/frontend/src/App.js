import { HashRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import SobreChile from './components/SobreChile';
import Regiones from './components/Regiones';
import Contacto from './components/Contacto';
import RegionDetail from './components/RegionDetail';

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<SobreChile />} />
        <Route path="/regiones" element={<Regiones />} />
        <Route path="/contacto" element={<Contacto />} />
        <Route path="/region/:id" element={<RegionDetail />} />
      </Routes>
      <Footer />
    </Router>
  );
}

export default App;