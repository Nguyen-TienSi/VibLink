import React from 'react'
import LoginForm from '../components/Auth/LoginForm'
import { useNavigate } from 'react-router-dom'

const Login: React.FC = () => {
  const navigate = useNavigate()
  return (
    <div className='min-h-screen flex items-center justify-center bg-gray-50'>
      <LoginForm onSuccess={() => navigate('/')} />
    </div>
  )
}

export default Login
