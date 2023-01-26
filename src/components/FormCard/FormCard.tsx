import React from 'react';
import './FormCard.css';

const FormCard = (Props: any) => {
  return (
    <div className="form__card">
      <h1>{Props.title}</h1>
      {Props.children}
    </div>
  )
}

export default FormCard