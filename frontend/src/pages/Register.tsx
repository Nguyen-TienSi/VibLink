import React from 'react'
import RegisterForm from '../components/Auth/RegisterForm'
import { useNavigate } from 'react-router-dom'

const Register: React.FC = () => {
  const navigate = useNavigate()

  return (
    <div style={{ maxWidth: 400, margin: '0 auto', padding: 24 }}>
      <RegisterForm onSuccess={() => navigate('/')} />
    </div>
  )
}

export default Register
