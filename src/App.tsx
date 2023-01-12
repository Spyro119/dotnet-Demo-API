import React from 'react';
import './App.css';

import { Home } from './pages';
import { Footer, Navbar } from './components';

function App() {
  return (
   <>
    <Navbar/>
    {/* <Browser Router> */}
      <Home/>
    {/* </Browser Router> */}
    <Footer/>
   </>
  );
}

export default App;
