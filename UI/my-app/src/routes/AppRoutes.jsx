import { Routes, Route } from 'react-router-dom';
import HomePage from '../components/Home';
import Login from '../components/Login'

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/login" element={<Login />} />
       <Route path="/home" element={<HomePage />} />
    </Routes>
  );
}

export default AppRoutes;
