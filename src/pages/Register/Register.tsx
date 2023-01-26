import { FormEvent, useRef } from 'react';
// import { IRegister } from '../../interfaces';
import './Register.css';
import { FormCard } from '../../components';

const Register = () => {
  const emailRef = useRef<HTMLInputElement | null>(null);
  const passwordRef = useRef<HTMLInputElement | null>(null);
  const passwordConfirmationRef = useRef<HTMLInputElement | null>(null);
  const firstNameRef = useRef<HTMLInputElement | null>(null);
  const lastNameRef = useRef<HTMLInputElement | null>(null);

  const handleSubmit = (e : FormEvent) => {
    e.preventDefault();
    console.log(emailRef.current?.value);
    setTimeout(() => { }, 10000)
  }

  return (
    <div className="app__bg flex__center">
      <FormCard title="Register">
      <form className="app__register-form">
      <input placeholder="Entrez votre prénom" ref={firstNameRef}/>
      <input placeholder="Entrez votre nom." ref={lastNameRef}/>
      {/* <label id="icon"><i className="fas fa-envelope"></i></label> */}
      <input placeholder="Entrez votre email" autoComplete="email" ref={emailRef}/>
      {/* <label id="icon"><i className="fas fa-user"></i></label> */}
      <input placeholder="Type your password" type="password" autoComplete="new-password" ref={passwordRef}/>
      <input placeholder="Confirm password" type="password" autoComplete="new-password" ref={passwordConfirmationRef}/>
      <button className="custom__button round-btn" onClick={handleSubmit}>Register</button>
      </form>
      </FormCard>
    </div>
  )
}

export default Register