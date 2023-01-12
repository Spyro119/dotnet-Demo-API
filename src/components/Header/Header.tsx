import React from 'react';

import { images } from '../../constants';

import './Header.css';

const Header = () => (
  <>
    <div className="app__image" id="home">
    <div className="app__above-image-text_smallscreen">
          <h1 className="headtext__cormorant" style={{marginBottom: '1rem'}}>Your dog don't behave? </h1>
          <p className="p__opensans">Talk to an expert at JS Animal Care and learn how <em>YOU</em> can train better your dog.</p>
        </div>
      <div className="app__Image-text">
          <h1 className="headtext__cormorant" style={{marginBottom: '1rem', marginTop: '5rem'}}>Your dog don't <br/> behave? </h1>
          <p className="p__opensans" style={{marginRight:"50%"}}>Talk to an expert at JS Animal Care and learn how <em>YOU</em> can train better your dog.</p>
        </div>
        <div className="app__image-container">
          <img
          src={images.healthyDog}/>
        </div>
      </div>
    <div className="app__header app__wrapper section__padding app__bg">
      <div className="app__wrapper_info">
        {/* <SubHeading title='Chase the new flavor'/> */}
        <h1 className="headtext__cormorant"> Home </h1>
        <p className="p__opensans" style={{ margin: '2rem 0' }}>Sit tellus lobortis sed senectus vivamus molestie. Condimentum volutpat morbi facilisis quam scelerisque sapien. Et, penatibus aliquam amet tellus </p>
        <button type="button" className="custom__button">Book an appointment now</button>
      </div>

      <div className="app__wrapper_img">
      <img src={images.dogTraining} alt="header img"/>
      </div>
    </div>
  </>
);

export default Header;