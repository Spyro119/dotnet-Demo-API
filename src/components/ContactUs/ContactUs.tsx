import React from 'react';

import './ContactUs.css';

const ContactUs = () => (
  <div className="app__bg app__wrapper section__padding" id="contact">
    <div className="app__wrapper_info">
      {/* <SubHeading title="Contact" /> */}
      <h1 className="headtext__cormorant" style={{marginBottom: '3rem'}}>Find us</h1>
      <div className="app__wrapper-content">
        <p className="p__opensans">Lane Ends Bungalow, Whatcroft Hall Lane, Rudheath, CW9 7SG</p>
        <p className="p__cormorant" style={{color: '#dcca87', margin: '2rem 0'}}>Opening Hours</p>
        <p className="p__opensans">Mon - Fri: 6:00 am - 21:00 pm</p>
        <p className="p__opensans">Sat - Sun: 5:00 am - 18:00 pm</p>
      </div>
      <button type="button" className="custom__button" style={{marginTop: '2rem'}}>Visit Us</button>
    </div>
    <div className="app__wrapper_img">

      {/* Since gmaps-react is not compatible with react@18 yet, we'll use an iframe. */}
        <iframe
          title='gmaps'
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24176.15493363148!2d-74.00690904142954!3d40.761598773474915!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89c258fbd0b82edf%3A0x41af87d148d1a374!2s9%20W%2053rd%20St%2C%20New%20York%2C%20NY%2010019%2C%20%C3%89tats-Unis!5e0!3m2!1sfr!2sca!4v1673479993192!5m2!1sfr!2sca"
          allowFullScreen={false}
          loading="lazy"
        ></iframe>
      {/* <img src={images.findus} alt="find us"/> */}
    </div>
  </div>
);

export default ContactUs;
