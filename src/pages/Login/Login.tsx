import React, { useRef } from 'react'

const Login = () => {
  const usernameRef = useRef<HTMLInputElement | null>(null);
  const passwordRef = useRef<HTMLInputElement | null>(null);

  const handleSubmit = ((e : Event) : void => {
    e.preventDefault();
  })

  return (
    <div>Login</div>
  )

}

export default Login