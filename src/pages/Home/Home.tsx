import React from 'react'

import './Home.css';

import { AboutUs, Header, ContactUs, BookAppointment } from '../../components';

const Home = () => {
  return (
    <div>
      <Header />
      <AboutUs />
      <ContactUs />
      <BookAppointment />
    </div>
  )
}

export default Home