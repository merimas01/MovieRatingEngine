import { Routes, Route } from 'react-router-dom';
import HomePage from '../components/Home';

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
    </Routes>
  );
}

export default AppRoutes;
