import { Routes, Route } from 'react-router-dom';
import HomePage from '../components/Home';
import Login from '../components/Login';

function AppRoutes() {
  return (
    <Routes>
      <Route path="/home/:uniqueUserInfo" element={<HomePage />} />
      <Route path="/home" element={<HomePage />} /> {/* if you dont use Google auth */}
      <Route path="/" element={<Login />} />
    </Routes>
  );
}

export default AppRoutes;
