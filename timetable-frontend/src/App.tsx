//import { useState } from 'react'
import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import Home from './pages/HomePage';
import ProfessorPage from './pages/ProfessorPage';
import TimetablePage from './pages/TimetablePage';

function App() {
  //const [count, setCount] = useState(0)

  return (
    
      
      <Router>
      {/*   <NavigationBar/> */}
      <Routes>
        
        <Route path="/" element={<ProfessorPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/homePage" element={<Home />} />
        <Route path="/groupsPage" element={<TimetablePage />} />
        {/*<Route path="/account" element={<Account />} /> */}
       
      </Routes>
    </Router>

  )
}

export default App
