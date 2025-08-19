import { Routes, Route } from 'react-router-dom';
import HomePage from '../components/Home';
import Login from '../components/Login';

function AppRoutes() {
  return (
    <Routes>
      <Route path="/home/:uniqueUserInfo" element={<HomePage />} />
      <Route path="/" element={<HomePage />} /> {/* if you dont use Google auth */}
      <Route path="/login" element={<Login />} />
    </Routes>
  );
}

export default AppRoutes;
