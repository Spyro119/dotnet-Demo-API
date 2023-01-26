import React, {useEffect, useState} from 'react';
import { GiHamburgerMenu } from 'react-icons/gi';
import { MdOutlineRestaurantMenu } from 'react-icons/md';


import LogoSVG from '../LogoSVG/LogoSVG';

import './Navbar.css';

const Navbar = () => {

  const [toggleMenu, setToggleMenu] = useState<boolean>(false);
  const [hamburgerMenuColor, setHamburgerMenuColor] = useState<string>("#000");

  const handleDarkMode = () => {
    const favicon: any = document.querySelector('link[rel="icon"]');
    
    console.log(`darkMode ? ${window.matchMedia('(prefers-color-scheme: dark)').matches}`);

    if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
      favicon.href = '/favicon-dark.ico'
      setHamburgerMenuColor("#fff");
    } else {
      favicon.href = '/favicon-light.ico';
      setHamburgerMenuColor("#000");
    }
  }

  useEffect(() => {
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => handleDarkMode());
    handleDarkMode();
  })

  return (
    <nav className="app__navbar">
     <div className="app__navbar-logo flex__center">
        <LogoSVG style={{width: "12px"}} color="#DCCA87"  />
        <p className="p__opensans"> Sj Animal Care</p>
      </div>
        <ul className="app__navbar-links">
        <li className="p__opensans"><a href="/">Home</a></li>
        <li className="p__opensans"><a href="/#about">About</a></li>
        <li className="p__opensans"><a href="/#contact">Contact</a></li>
      </ul>
      <div className="app__navbar-login">
        {/* <a href="#login" className="p__opensans">Log in / Register</a> */}
        {/* <div className="separator"></div>  */} 
        {/* <a href="#book-appointment" className="p__opensans" >Schedule appointment</a> */}
      </div>
      <div className="app__navbar-smallscreen">
        <GiHamburgerMenu 
        color={hamburgerMenuColor}
        fontSize={27} 
        onClick={() => setToggleMenu(true)} />
        
        { toggleMenu && (
          <div className="app__navbar-smallscreen_overlay flex__center slide-bottom">
            <MdOutlineRestaurantMenu className="overlay__close"
            fontSize={27}
            onClick={() => setToggleMenu(false)}/>
          <ul className="app__navbar-smallscreen_links">
            <li className="p__opensans"><a href="/">Home</a></li>
            <li className="p__opensans"><a href="#about">About</a></li>
            <li className="p__opensans"><a href="#contact">Contact</a></li>
            {/* <li className="p__opensans"><a href="#book-appointment">Schedule appointment</a></li> */}
          </ul>
        </div>
      )}

      </div>
    </nav>
  )
};

export default Navbar;
