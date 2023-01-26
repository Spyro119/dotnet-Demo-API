import React from 'react';
import './App.css';
import { Route, Routes } from 'react-router-dom';

import { Home, Login, Profile, Register } from './pages';
import { Footer, Navbar } from './components';

function App() {
  return (
   <>
    <Navbar/>
    <Routes>
      <Route path="/" element={<Home/>}/>
      <Route path="/login" element={<Login/>}/>
      <Route path="/register" element={<Register/>}/>
      <Route path="/user/profile" element={<Profile/>}/>
      {/* <Route path="/*" element={<NotFound/>}/> */}
    </Routes>
    <Footer/>
   </>
  );
}

export default App;
